using System.Collections.Generic;

namespace Mokona.Entities
{
    public class Company : Entity
    {
        public Company()
        { }

        public string Name { get; set; }

        public string Annotations { get; set; }

        public string Domain { get; set; }
    }
}
