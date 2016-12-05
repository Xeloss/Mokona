namespace Mokona.Core.IoC
{
    using System;

    /// <summary>
    /// Configura un metodo o una clase para que guarde automaticamente los cambios hechos a entidades
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class AutomaticSaveChangesAttribute : Attribute
    {
        public readonly bool Enabled;

        public AutomaticSaveChangesAttribute(bool enable)
        {
            this.Enabled = enable;
        }
    }
}
