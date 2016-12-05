namespace Mokona.Core.Services
{
    using Mokona.Entities;
    using Mokona.Utils.Interfaces;
    using System.Collections.Generic;

    public interface ICacheService<T> : IService<T> where T : Entity
    { }
}
