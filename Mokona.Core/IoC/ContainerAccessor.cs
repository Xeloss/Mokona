namespace Mokona.Core.IoC
{
    using Autofac;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    public static class ContainerAccessor
    {
        public static Func<IComponentContext> Container { get; set; }

        public static T Resolve<T>()
        {
            return ContainerAccessor.Container().Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return ContainerAccessor.Container().Resolve(type);
        }

        public static T Resolve<T>(ExpandoObject values)
        {
            return ContainerAccessor.Container().Resolve<T>(ContainerAccessor.GetParameters(values));
        }

        public static T Resolve<T>(string name)
        {
            return ContainerAccessor.Container().ResolveNamed<T>(name);
        }

        public static T Resolve<T>(object values)
        {
            return ContainerAccessor.Container().Resolve<T>(ContainerAccessor.GetParameters(values));
        }

        public static T Resolve<T>(string name, ExpandoObject values)
        {
            return ContainerAccessor.Container().ResolveNamed<T>(name, ContainerAccessor.GetParameters(values));
        }

        public static T Resolve<T>(string name, object values)
        {
            return ContainerAccessor.Container().Resolve<T>(ContainerAccessor.GetParameters(values));
        }

        private static NamedParameter[] GetParameters(ExpandoObject values)
        {
            IList<NamedParameter> parameters = new List<NamedParameter>();

            foreach (var param in values)
            {
                parameters.Add(new NamedParameter(param.Key, param.Value));
            }

            return parameters.ToArray();
        }

        private static NamedParameter[] GetParameters(object values)
        {
            var result = new List<NamedParameter>();

            foreach (var prop in values.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                result.Add(new NamedParameter(prop.Name, prop.GetValue(values, null)));
            }

            return result.ToArray();
        }
    }
}
