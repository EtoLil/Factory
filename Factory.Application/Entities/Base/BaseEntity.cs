using Factory.Core.Interfaces;

namespace Factory.Core.Entities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
