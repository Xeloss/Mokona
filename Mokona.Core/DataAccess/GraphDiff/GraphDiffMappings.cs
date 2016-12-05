namespace Mokona.Core.DataAccess.GraphDiff
{
    using Mokona.Entities;
    using RefactorThis.GraphDiff;
    using System;
    using System.Collections.Concurrent;
    using System.Linq.Expressions;

    public class GraphDiffMappings
    {
        private static ConcurrentDictionary<Type, IGraphDiffSetting> mappings;

        static GraphDiffMappings()
        {
            mappings = new ConcurrentDictionary<Type, IGraphDiffSetting>();

            mappings[typeof(Company)] = new CompanyGraphDiffSettings();
        }

        public static Expression<Func<IUpdateConfiguration<T>, object>> GetOf<T>(GraphDiffAction action) where T : Entity
        {
            var key = typeof(T);

            if (!mappings.ContainsKey(key))
                return (map) => null;

            var settings = mappings[key];

            return settings.GetConfiguration(action) as Expression<Func<IUpdateConfiguration<T>, object>>;
        }
    }
}
