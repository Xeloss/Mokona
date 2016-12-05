namespace Mokona.Entities
{
    using System;

    public class SoftDeletableAttribute : Attribute
    {
        public string ColumnName { get; set; }

        public SoftDeletableAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}
