namespace Mokona.Core.DataAccess.Repositories
{
    using Mokona.Core.Security;
    using Mokona.Entities;

    public interface ISecurityRepository : IRepository<User>
    {
        User GetUserWith(string aLoginName, string aCompanyDomain);

        int? LoadRuleEntityId<T>(AuthorizationRule<T> rule, T entity) where T : Entity;
    }
}
