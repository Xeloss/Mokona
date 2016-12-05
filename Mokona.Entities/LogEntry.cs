namespace Mokona.Entities
{
    using System;

    public class LogEntry : Entity
    {
        public LogEntry()
        {
            this.TimeStamp = DateTime.UtcNow;
        }

        public DateTime TimeStamp { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", TimeStamp, UserId, Description.Substring(0, 30));
        }
    }
}
