namespace Mokona.Core.Security
{
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System;
    using System.Linq.Expressions;

    public class AuthorizationRule<T> where T : Entity
    {
        public AuthorizationRule(Expression<Func<T, int?>> entityId, EntityType entityType, Permissions permission)
        {
            this.EntityIdExpression = entityId;
            this.RootEntityIdExpression = entityId;

            this.EntityType = entityType;
            this.Permission = permission;

            if (this.EntityIdExpression != null)
            {
                this.CompiledEntityIdExpression = this.EntityIdExpression.Compile();
                this.CompiledRootEntityIdExpression = this.CompiledEntityIdExpression;
            }
        }

        public AuthorizationRule(Expression<Func<T, int?>> entityId, Expression<Func<T, int?>> rootEntityId, EntityType entityType, Permissions permission)
            : this(entityId, entityType, permission)
        {
            this.RootEntityIdExpression = rootEntityId;
            if (this.RootEntityIdExpression != null)
                this.CompiledRootEntityIdExpression = this.RootEntityIdExpression.Compile();
        }

        public Permissions Permission { get; set; }

        public EntityType EntityType { get; set; }

        public Expression<Func<T, int?>> EntityIdExpression { get; private set; }

        public Expression<Func<T, int?>> RootEntityIdExpression { get; private set; }

        protected Func<T, int?> CompiledEntityIdExpression { get; set; }

        protected Func<T, int?> CompiledRootEntityIdExpression { get; set; }

        public int? GetEntityIdOf(T anEntity)
        {
            try
            {
                if (this.CompiledEntityIdExpression == null)
                    return null;

                return this.CompiledEntityIdExpression(anEntity);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public int? GetRootEntityIdOf(T anEntity)
        {
            try
            {
                if (this.CompiledRootEntityIdExpression == null)
                    return null;

                return this.CompiledRootEntityIdExpression(anEntity);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public override string ToString()
        {
            return "{0} - {1}".ApplyFormat(EntityType, Permission);
        }
    }
}
