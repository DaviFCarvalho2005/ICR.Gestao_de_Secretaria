using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICR.Domain.Model.MemberAggregate
{
    [Table("members")]
    public class Member
    {
        public long Id { get; set; }
        [ForeignKey("families")]
        public long FamilyId { get; set; }
        [ForeignKey("churchs")]
        public long ChurchId { get; set; }
        public string Name { get; set; } = null!;
        public GenderType Gender { get; set; } // 'M' | 'F'
        [ForeignKey("cells")]
        public string? Role { get; set; }
        public DateTime BirthDate { get; set; }
        // Classe SEMPRE preenchida pelo sistema
        public string? CellPhone { get; set; }
        public ClassType Class { get; set; }

        public Member() { }

        public Member(long id, long familyId, long churchId, string name, GenderType gender, long cellId, string? role, DateTime birthDate, string? cellPhone, ClassType memberClass)
        {
            Id = id;
            FamilyId = familyId;
            ChurchId = churchId;
            Name = name;
            Gender = gender;
            Role = role;
            BirthDate = birthDate;
            CellPhone = cellPhone;
            Class = memberClass;

        }
        public enum GenderType
        {
            HOMEM = 1,
            MULHER = 2
        }
        public enum ClassType
        {
            BEBE = 0,
            CRIANCA = 1,
            JUNIORES = 2,
            JUVENIS = 3,
            JOVENS = 4,
            HOMENS = 5,
            MULHERES = 6,
        }
        public void setFamilyId(long familyId)
        {
            FamilyId = familyId;
        }
        public void SetChurchId(long churchId)
        {
            ChurchId = churchId;
        }
        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty", nameof(newName));
            Name = newName;
        }
        public void setGender(GenderType newGender)
        {
            Gender = newGender;
        }
        public void SetRole(string? newRole)
        {
            Role = newRole;
        }
        public void SetBirthDate(DateTime newBirthDate)
        {
            BirthDate = newBirthDate;
        }
        public void SetCellPhone(string? newCellPhone)
        {
            CellPhone = newCellPhone;
        }
        public void SetClass(ClassType newClass)
        {
            Class = newClass;
        }
    }
}

