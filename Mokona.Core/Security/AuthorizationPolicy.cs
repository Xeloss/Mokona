namespace Mokona.Core.Security
{
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System.Collections.Generic;

    public class AuthorizationPolicy<T> where T : Entity
    {
        public AuthorizationPolicy(Permissions action)
        {
            this.Action = action;
        }

        public Permissions Action { get; set; }

        public List<AuthorizationRule<T>> Rules { get; set; }

        public override string ToString()
        {
            return "{0} - {1}".ApplyFormat(typeof(T).Name, Action);
        }
    }
}
