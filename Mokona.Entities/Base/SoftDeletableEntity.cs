namespace Mokona.Entities
{
    using System;

    [SoftDeletable("Deleted")]
    public abstract class SoftDeletableEntity : Entity
    {
        public bool Deleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
