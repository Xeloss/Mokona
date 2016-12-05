namespace Mokona.FrontEnd
{
    using System.Collections.Generic;
    using Mokona.Core.Security;
    using Mokona.Entities;

    public static class AuthorizationPoliciesConfig
    {
        public static void RegisterPolicies()
        {
            #region Company Policies

            new AuthorizationPolicy<Company>(Permissions.Admin)
            {
                Rules = new List<AuthorizationRule<Company>>() {
                    new AuthorizationRule<Company>(null, EntityTypes.Company, Permissions.Admin),
                }
            }.Register();

            new AuthorizationPolicy<Company>(Permissions.Create)
            {
                Rules = new List<AuthorizationRule<Company>>() {
                    new AuthorizationRule<Company>(null, EntityTypes.Company, Permissions.Create),
                }
            }.Register();

            new AuthorizationPolicy<Company>(Permissions.Delete)
            {
                Rules = new List<AuthorizationRule<Company>>() {
                    new AuthorizationRule<Company>(c => c.Id, EntityTypes.Company, Permissions.Delete),
                }
            }.Register();

            new AuthorizationPolicy<Company>(Permissions.Update)
            {
                Rules = new List<AuthorizationRule<Company>>() {
                    new AuthorizationRule<Company>(c => c.Id, EntityTypes.Company, Permissions.Update),
                }
            }.Register();

            new AuthorizationPolicy<Company>(Permissions.Read)
            {
                Rules = new List<AuthorizationRule<Company>>() {
                    new AuthorizationRule<Company>(c => c.Id, EntityTypes.Company, Permissions.Read),
                }
            }.Register();

            #endregion

            #region User Policies
            new AuthorizationPolicy<User>(Permissions.Admin)
            {
                Rules = new List<AuthorizationRule<User>>() {
                    new AuthorizationRule<User>(null, EntityTypes.User, Permissions.Admin),
                }
            }.Register();


            new AuthorizationPolicy<User>(Permissions.Create)
            {
                Rules = new List<AuthorizationRule<User>>() {
                    new AuthorizationRule<User>(null, EntityTypes.User, Permissions.Create),
                    new AuthorizationRule<User>(u => u.CompanyId, EntityTypes.Company, Permissions.Update)
                }
            }.Register();

            new AuthorizationPolicy<User>(Permissions.Delete)
            {
                Rules = new List<AuthorizationRule<User>>() {
                    new AuthorizationRule<User>(u => u.Id, EntityTypes.User, Permissions.Delete),
                    new AuthorizationRule<User>(u => u.CompanyId, EntityTypes.Company, Permissions.Update)
                }
            }.Register();

            new AuthorizationPolicy<User>(Permissions.Update)
            {
                Rules = new List<AuthorizationRule<User>>() {
                    new AuthorizationRule<User>(u => u.Id, EntityTypes.User, Permissions.Update),
                    new AuthorizationRule<User>(u => u.CompanyId, EntityTypes.Company, Permissions.Update)
                }
            }.Register();

            new AuthorizationPolicy<User>(Permissions.Read)
            {
                Rules = new List<AuthorizationRule<User>>() {
                    new AuthorizationRule<User>(u => u.Id, EntityTypes.User, Permissions.Read),
                    new AuthorizationRule<User>(u => u.CompanyId, EntityTypes.Company, Permissions.Read)
                }
            }.Register();

            new AuthorizationPolicy<User>(Permissions.Approve)
            {
                Rules = new List<AuthorizationRule<User>>() {
                    new AuthorizationRule<User>(u => u.Id, EntityTypes.User, Permissions.Approve),
                    new AuthorizationRule<User>(u => u.CompanyId, EntityTypes.Company, Permissions.Read),
                }
            }.Register();
            #endregion

            #region Role Policies
            new AuthorizationPolicy<Role>(Permissions.Admin)
            {
                Rules = new List<AuthorizationRule<Role>>() {
                    new AuthorizationRule<Role>(null, EntityTypes.Role, Permissions.Admin),
                    new AuthorizationRule<Role>(r => r.CompanyId, EntityTypes.Company, Permissions.Read)
                }
            }.Register();

            new AuthorizationPolicy<Role>(Permissions.Create)
            {
                Rules = new List<AuthorizationRule<Role>>() {
                    new AuthorizationRule<Role>(null, EntityTypes.Role, Permissions.Create),
                    new AuthorizationRule<Role>(r => r.CompanyId, EntityTypes.Company, Permissions.Read)
                }
            }.Register();

            new AuthorizationPolicy<Role>(Permissions.Delete)
            {
                Rules = new List<AuthorizationRule<Role>>() {
                    new AuthorizationRule<Role>(r => r.Id, EntityTypes.Role, Permissions.Delete),
                    new AuthorizationRule<Role>(r => r.CompanyId, EntityTypes.Company, Permissions.Read)
                }
            }.Register();

            new AuthorizationPolicy<Role>(Permissions.Update)
            {
                Rules = new List<AuthorizationRule<Role>>() {
                    new AuthorizationRule<Role>(r => r.Id, EntityTypes.Role, Permissions.Update),
                    new AuthorizationRule<Role>(r => r.CompanyId, EntityTypes.Company, Permissions.Read)
                }
            }.Register();

            new AuthorizationPolicy<Role>(Permissions.Read)
            {
                Rules = new List<AuthorizationRule<Role>>() {
                    new AuthorizationRule<Role>(r => r.Id, EntityTypes.Role, Permissions.Read),
                    new AuthorizationRule<Role>(r => r.CompanyId, EntityTypes.Company, Permissions.Read)
                }
            }.Register();
            #endregion
        }
    }
}
