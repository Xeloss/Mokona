namespace Mokona.Core.Services
{
    using Mokona.Core.Security;
    using Mokona.Entities;
    using System.Collections.Generic;

    public interface ISecurityService
    {
        bool AreCredentialsValid(string aLoginName, string aCompanyDomain, string aPassword);

        bool IsAuthorized<T>(int userId, int companyid, Permissions requiredPermission) where T : Entity;

        bool IsAuthorized<T>(int userId, int companyid, T entity, Permissions requiredPermission) where T : Entity;

        IEnumerable<RolePermission> GetPermissionsOf(int userId, int companyid, EntityType entityType, Permissions permission);

        UserPrincipal GetCurrentUserPrincipal();

        UserPrincipal CreatePrincipalFor(string aLoginName, string aCompanyDomain);
        UserPrincipal CreatePrincipalFor(User user);
    }
}
