using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICR.Domain.Model.CellAggregate
{
    [Table("cells")]
    public class Cell
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        [ForeignKey("ChurchId")]
        public long ChurchId { get; private set; }
        [ForeignKey("ResponsibleId")]
        public long? ResponsibleId { get; private set; }
        protected Cell() { }
        public Cell(long id, string name, long churchId)
        {
            Id = id;
            Name = name;
            ChurchId = churchId;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
        public void AssignResponsible(long responsibleId)
        {
            ResponsibleId = responsibleId;
        }

    }
}
