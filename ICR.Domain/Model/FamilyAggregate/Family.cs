using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICR.Domain.Model.FamilyAggregate
{
    [Table("families")]
    public class Family
    {
        public long Id { get; set; }
        [ForeignKey("FatherId")]
        public long? ManId { get; set; }
        [ForeignKey("MotherId")]
        public long? WomanId { get; set; }
        public DateTime? WeddingDate { get; set; }


        public Family(){ }
        public Family(long id, long? manId, long? womanId, DateTime? weddingDate)
        {
            Id = id;
            ManId = manId;
            WomanId = womanId;
            WeddingDate = weddingDate;
        }
        public void SetFatherId(long? manId)
        {
            ManId = manId;
        }
        public void SetMotherId(long? womanId)
        {
            WomanId = womanId;
        }
        public void SetWeddingDate(DateTime? weddingDate)
        {
            WeddingDate = weddingDate;
        }
    }
}

