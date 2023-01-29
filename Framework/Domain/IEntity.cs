using System;

namespace Framework.Domain
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }

    public interface IEntity : IEntity<Guid>
    {

    }
}
