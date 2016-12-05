namespace Mokona.Core.DataAccess
{
    using Mokona.Core.DataAccess.Context;
    using Mokona.Core.Exceptions;
    using Mokona.Core.Security;
    using Mokona.Core.Services;
    using Mokona.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class AuthorizedEntitiesOnlyContext : IDBContext
    {
        private IDBContext context;
        private ISecurityService securityService;

        private static MethodInfo Contains;
        private static PropertyInfo HasValue;

        static AuthorizedEntitiesOnlyContext()
        {
            Contains = typeof(Enumerable).GetMethods()
                                         .FirstOrDefault(m => m.Name.Equals("Contains", StringComparison.CurrentCultureIgnoreCase)
                                                 && m.GetParameters().Length == 2)
                                         .MakeGenericMethod(typeof(int?));
            HasValue = typeof(int?).GetProperties()
                                   .FirstOrDefault(m => m.Name.Equals("HasValue", StringComparison.CurrentCultureIgnoreCase));
        }
        public AuthorizedEntitiesOnlyContext(IDBContext context, ISecurityService securityService)
        {
            this.context = context;
            this.securityService = securityService;
        }

        public IQueryable<Entity> Relation(Type entityType)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Relation<T>() where T : Entity
        {
            var result = this.context.Relation<T>();

            var policy = AuthorizationPolicies.GetPolicyFor<T>(Permissions.Read);
            if (policy == null)
                return result;

            //TODO: Si el usuario no esta loggeado y existe algun tipo de policy para hacer lecturas, va a tirar unauthorized. 
            //Ver como resolver las politicas de seguridad para usuarios anonimos
            var user = securityService.GetCurrentUserPrincipal();
            if (user == null)
                throw new UnauthorizedException(string.Empty, Permissions.Read, EntityTypes.Parse(typeof(T).Name));

            foreach (var rule in policy.Rules)
            {
                var permissions = securityService.GetPermissionsOf(user.UserId, user.CompanyId, rule.EntityType, rule.Permission)
                                                 .OrderByDescending(p => p.IsDenied)
                                                 .ThenBy(p => p.EntityId);

                var all = permissions.FirstOrDefault(p => p.EntityId == null);

                var allowedIds = permissions.Where(p => !p.IsDenied && p.EntityId != null)
                                            .Select(p => p.EntityId)
                                            .ToList();

                var deniedIds = permissions.Where(p => p.IsDenied && p.EntityId != null)
                                           .Select(p => p.EntityId)
                                           .ToList();

                Expression allowed = this.BuildAlwaysTrueExpression();
                Expression denied = this.BuildAlwaysTrueExpression();
                Expression isNull = this.BuildIsNullExpression(rule.EntityIdExpression); 

                if (all != null)
                {
                    if (all.IsDenied)
                    {
                        allowed = this.BuildExpresionFor(allowedIds, rule.EntityIdExpression);
                        isNull = this.BuildAlwaysFalseExpression();
                    }   
                    else
                    {
                        denied = Expression.Not(this.BuildExpresionFor(deniedIds, rule.EntityIdExpression));
                    }
                }
                else
                {
                    denied = Expression.Not(this.BuildExpresionFor(deniedIds, rule.EntityIdExpression));
                    allowed = this.BuildExpresionFor(allowedIds, rule.EntityIdExpression);
                }

                if (!allowedIds.Any())
                    allowed = this.BuildAlwaysTrueExpression();

                if(!deniedIds.Any())
                    denied = this.BuildAlwaysTrueExpression();

                var filter = Expression.AndAlso(allowed, denied);
                filter = Expression.OrElse(isNull, filter);

                result = result.Where(Expression.Lambda<Func<T, bool>>(filter, rule.EntityIdExpression.Parameters));
            }

            return result;
        }

        public T Save<T>(T anEntity) where T : Entity, new()
        {
            var requiredPermission = anEntity.Id == 0 ? Permissions.Create : Permissions.Update;

            var user = securityService.GetCurrentUserPrincipal();
            if (user == null)
                throw new UnauthorizedException(string.Empty, requiredPermission, EntityTypes.Parse(typeof(T).Name));

            var isAuthorized = this.securityService.IsAuthorized(user.UserId, user.CompanyId, anEntity, requiredPermission);
            if (!isAuthorized)
                throw new UnauthorizedException(user.LoginName, requiredPermission, EntityTypes.Parse(typeof(T).Name));

            return this.context.Save(anEntity);
        }

        public T Delete<T>(T anEntity) where T : Entity
        {
            var user = securityService.GetCurrentUserPrincipal();
            if (user == null)
                throw new UnauthorizedException(string.Empty, Permissions.Delete, EntityTypes.Parse(typeof(T).Name));

            var isAuthorized = this.securityService.IsAuthorized(user.UserId, user.CompanyId, anEntity, Permissions.Delete);
            if (!isAuthorized)
                throw new UnauthorizedException(user.LoginName, Permissions.Delete, EntityTypes.Parse(typeof(T).Name));

            return this.context.Delete(anEntity);
        }

        private Expression BuildExpresionFor<T>(IEnumerable<int?> collection, Expression<Func<T, int?>> entityId)
            where T : Entity
        {
            var constant = Expression.Constant(collection);

            return Expression.Call(Contains, constant, entityId.Body);
        }

        private Expression BuildAlwaysTrueExpression()
        {
            var constant = Expression.Constant(1);

            return Expression.Equal(constant, constant);
        }
        private Expression BuildAlwaysFalseExpression()
        {
            var True = Expression.Constant(0);
            var False = Expression.Constant(1);

            return Expression.Equal(True, False);
        }

        private Expression BuildIsNullExpression<T>(Expression<Func<T, int?>> entityId)
        {
            return Expression.Not(Expression.Property(entityId.Body, HasValue));
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public ITransaction BeginTransaction()
        {
            return this.context.BeginTransaction();
        }
    }
}
