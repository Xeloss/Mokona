namespace Mokona.FrontEnd.Utils
{
    using Mokona.Core.IoC;
    using Mokona.Core.Services;
    using Mokona.Entities;

    public static class SecurityHelper
    {
        public static bool IsCurrentUserAuthorized<T>(params Permissions[] requiredPermissions) where T : Entity
        {
            var securityService = ContainerAccessor.Resolve<ISecurityService>();

            var aUser = securityService.GetCurrentUserPrincipal();
            if (aUser == null)
                return false;

            foreach (var permission in requiredPermissions)
            {
                if (!securityService.IsAuthorized<T>(aUser.UserId, aUser.CompanyId, permission))
                    return false;
            }

            return true;
        }
    }
}
