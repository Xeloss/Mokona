namespace Mokona.Core.Exceptions
{
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System.Net;

    public class UnauthorizedException : HttpApplicationException
    {
        public string UserLoginName { get; private set; }

        public Permissions RequestedAction { get; private set; }

        public EntityType EntityType { get; private set; }

        public UnauthorizedException(string userLoginName, Permissions requestedAction, EntityType entityType)
            : base(HttpStatusCode.Unauthorized)
        {
            this.UserLoginName = userLoginName;
            this.RequestedAction = requestedAction;
            this.EntityType = entityType;
        }

        public override string ToString()
        {
            return "Action {0} on entity of type {1} is not allowed to the user {2}".ApplyFormat(RequestedAction, EntityType, UserLoginName);
        }
    }
}
