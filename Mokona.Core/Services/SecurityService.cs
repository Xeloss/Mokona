namespace Mokona.Core.Services
{
    using Mokona.Core.DataAccess.Repositories;
    using Mokona.Core.IoC;
    using Mokona.Core.Security;
    using Mokona.Core.Utils;
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System;

    [AutomaticSaveChanges(false)]
    public class SecurityService : ISecurityService
    {
        //TODO: Hacer que la cache de permisos caduque en algun momento
        private ISecurityRepository securityRepository;
        private static ConcurrentDictionary<string, CacheItem<IEnumerable<RolePermission>>> permissionsCache;

        static SecurityService()
        {
            permissionsCache = new ConcurrentDictionary<string, CacheItem<IEnumerable<RolePermission>>>();
        }

        public SecurityService(ISecurityRepository securityRepository)
        {
            this.securityRepository = securityRepository;
        }

        public bool AreCredentialsValid(string aLoginName, string aCompanyDomain, string aPassword)
        {
            if (aLoginName.IsNullOrEmpty() || aPassword.IsNullOrEmpty() || aCompanyDomain.IsNullOrEmpty())
                return false;

            var user = this.securityRepository.GetUserWith(aLoginName, aCompanyDomain);
            if (user == null)
                return false;

            var cypher = EncryptionHelper.Encrypt(aPassword, aPassword);

            return user.Password == cypher;
        }

        public bool IsAuthorized<T>(int userId, int companyId, Permissions requiredPermission) where T : Entity
        {
            var policy = AuthorizationPolicies.GetPolicyFor<T>(requiredPermission);
            if (policy == null)
                return true;

            var authorized = true;
            foreach (var rule in policy.Rules)
            {
                var permission = this.GetPermissionsOf(userId, companyId, rule.EntityType, rule.Permission)
                                     .OrderByDescending(p => p.IsDenied)
                                     .FirstOrDefault();

                if (permission == null)
                    return false;

                authorized = authorized && !permission.IsDenied;
            }

            return authorized;
        }
        public bool IsAuthorized<T>(int userId, int companyId, T entity, Permissions requiredPermission) where T : Entity
        {
            var policy = AuthorizationPolicies.GetPolicyFor<T>(requiredPermission);
            if (policy == null)
                return true;

            var authorized = true;
            foreach (var rule in policy.Rules)
            {
                var id = rule.GetEntityIdOf(entity);
                if (id == null)
                    id = securityRepository.LoadRuleEntityId(rule, entity);

                var permission = this.GetPermissionsOf(userId, companyId, rule.EntityType, rule.Permission)
                                     .OrderByDescending(p => p.IsDenied)
                                     .FirstOrDefault(p => p.EntityId == null || p.EntityId == id);

                if (permission == null)
                    return false;

                authorized = authorized && !permission.IsDenied;
            }

            return authorized;
        }

        public UserPrincipal GetCurrentUserPrincipal()
        {
            return HttpContext.Current.User as UserPrincipal;
        }

        public UserPrincipal CreatePrincipalFor(string aLoginName, string aCompanyDomain)
        {
            var user = this.securityRepository.GetUserWith(aLoginName, aCompanyDomain);

            return this.CreatePrincipalFor(user);
        }
        public UserPrincipal CreatePrincipalFor(User user)
        {
            return new UserPrincipal(user);
        }

        public IEnumerable<RolePermission> GetPermissionsOf(int userId, int companyid, EntityType entityType, Permissions permission)
        {
            return this.GetPermissionsOf(userId, companyid)
                       .Where(p => (p.Permission == permission || p.Permission == Permissions.Admin)
                                &&  p.EntityType == entityType.Id)
                       .ToList();
        }

        private IEnumerable<RolePermission> GetPermissionsOf(int userId, int companyId)
        {
            CacheItem<IEnumerable<RolePermission>> result = null;
            var cacheKey = $"{userId}_{companyId}";

            if (!permissionsCache.TryGetValue(cacheKey, out result))
            {
                var permissions = this.securityRepository.AsUntracked()
                                                         .ListAll()
                                                         .Where(u => u.Id == userId 
                                                                  && u.CompanyId == companyId)
                                                         .SelectMany(u => u.Roles)
                                                         .SelectMany(r => r.Permissions)
                                                         .ToList();
                if (permissions != null)
                {
                    result = new CacheItem<IEnumerable<RolePermission>>()
                    {
                        CreationDate = DateTime.Now,
                        Item = permissions
                    };

                    permissionsCache.GetOrAdd(cacheKey, result);
                }
            }

            if (result != null && result.HasExpired())
            {
                permissionsCache.TryRemove(cacheKey, out result);
                return this.GetPermissionsOf(userId, companyId);
            }

            return result.Item;
        }

        private class CacheItem<T>
            where T : class
        {
            public T Item { get; set; }

            public DateTime CreationDate { get; set; }

            public DateTime ExpirationDate {
                get
                {
                    double interval = 5 * 60 * 1000;
                    return this.CreationDate.AddMilliseconds(interval);
                }
            }

            public bool HasExpired()
            {
                return this.ExpirationDate < DateTime.Now;
            }
        }
    }
}
