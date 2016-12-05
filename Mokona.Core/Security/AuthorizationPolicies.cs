namespace Mokona.Core.Security
{
    using Mokona.Entities;
    using Mokona.Utils.Extensions;
    using System.Collections.Concurrent;

    public static class AuthorizationPolicies
    {
        private static ConcurrentDictionary<string, object> policies;

        static AuthorizationPolicies()
        {
            policies = new ConcurrentDictionary<string, object>();
        }

        public static AuthorizationPolicy<T> GetPolicyFor<T>(Permissions Action)
            where T : Entity
        {
            var type = typeof(T);
            var key = "{0}_{1}".ApplyFormat(type.Name, Action.ToString());

            if (!policies.ContainsKey(key))
                return null;

            return (AuthorizationPolicy<T>)policies[key];
        }

        public static void Register<T>(this AuthorizationPolicy<T> item) where T : Entity
        {
            var key = "{0}_{1}".ApplyFormat(typeof(T).Name, item.Action.ToString());
            policies.GetOrAdd(key, item);
        }
    }
}
