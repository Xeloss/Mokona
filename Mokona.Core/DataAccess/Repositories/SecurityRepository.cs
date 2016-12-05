namespace Mokona.Core.DataAccess.Repositories
{
    using Mokona.Core.DataAccess;
    using Mokona.Core.DataAccess.Context;
    using Mokona.Core.Exceptions;
    using Mokona.Core.IoC;
    using Mokona.Core.Security;
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System.Collections.Generic;
    using System.Linq;

    public class SecurityRepository : BasicRepository<User>, ISecurityRepository
    {
        public SecurityRepository()
            : base(ContainerAccessor.Resolve<IDBContext>())
        { }

        protected SecurityRepository(IDBContext context)
        {
            this.context = context;
        }

        public User GetUserWith(string aLoginName, string aCompanyDomain)
        {
            return this.AsUntracked()
                       .ListAll()
                       .FirstOrDefault(u => u.LoginName == aLoginName
                                         && u.Company.Domain == aCompanyDomain);
        }

        public int? LoadRuleEntityId<T>(AuthorizationRule<T> rule, T entity) where T : Entity
        {
            var rootId = rule.GetRootEntityIdOf(entity);
            if (rootId == null)
                return null;

            var rootEntity = this.context.Relation(rule.EntityType.Type)
                                         .FirstOrDefault(u => u.Id == rootId);

            if (rootEntity != null)
                return rootEntity.Id;

            throw new TechnicalException("Root entity '{0}' not found for entity '{1}'".ApplyFormat(rootId, entity));
        }

        public override User Save(User anEntity)
        {
            throw new TechnicalException("SecurityRepository is not intended to persist entities");
        }

        public override IEnumerable<User> Save(IEnumerable<User> entities)
        {
            throw new TechnicalException("SecurityRepository is not intended to persist entities");
        }

        public override void Delete(User aEntity)
        {
            throw new TechnicalException("SecurityRepository is not intended to persist entities");
        }

        public override void Delete(IEnumerable<User> entities)
        {
            throw new TechnicalException("SecurityRepository is not intended to persist entities");
        }

        public override void DeleteBy(int Id)
        {
            throw new TechnicalException("SecurityRepository is not intended to persist entities");
        }

        public override IRepository<User> AsUntracked()
        {
            return new SecurityRepository(new UntrackedContext(this.context));
        }
    }
}
