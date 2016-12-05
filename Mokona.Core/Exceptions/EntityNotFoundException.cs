namespace Mokona.Core.Exceptions
{
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System.Net;

    public class EntityNotFoundException : HttpApplicationException
    {
        public object RequestedId { get; set; }

        public EntityType EntityType { get; private set; }

        public EntityNotFoundException(EntityType entityType, object requestedId)
            : base(HttpStatusCode.NotFound)
        {
            this.EntityType = entityType;
            this.RequestedId = requestedId;
        }

        public override string ToString()
        {
            return "Resource '{0}' of type {1} was not found.".ApplyFormat(RequestedId, EntityType);
        }
    }
}
