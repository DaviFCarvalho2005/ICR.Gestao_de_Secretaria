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
        [ForeignKey(nameof(CellId))]
        public long CellId { get; set; }
        public CellAggregate.Cell Cell { get; set; }
        [ForeignKey(nameof(ChurchId))]
        public long ChurchId { get; set; }
        public ChurchAggregate.Church Church { get; set; }

        [ForeignKey(nameof(ManId))]
        public long? ManId { get; set; }
        public MemberAggregate.Member? Man { get; set; }
        [ForeignKey(nameof(WomanId))]
        public long? WomanId { get; set; }
        public MemberAggregate.Member? Woman { get; set; }

        public DateTime? WeddingDate { get; set; }


        public Family(){ }
        public Family(long id,long cellId, long? manId, long? womanId, DateTime? weddingDate)
        {
            Id = id;
            CellId = cellId;
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

