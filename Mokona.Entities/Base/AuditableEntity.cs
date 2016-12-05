namespace Mokona.Entities
{
    using System;

    public abstract class AuditableEntity : SoftDeletableEntity
    {
        public int CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public int LastUpdaterId { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }
}
