using ICR.Domain.Model.MemberAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICR.Domain.DTOs
{
    public class MemberDTO
    {
        public long FamilyId { get; set; }
        public string Name { get; set; } = null!;
        public GenderType Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool HasBeenMarried { get; private set; }
        public string? Role { get; set; }
        public long? CellPhone { get; set; }


        



    }
}
