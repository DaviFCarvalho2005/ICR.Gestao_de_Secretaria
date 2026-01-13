using ICR.Domain.Model.ChurchAggregate;
using ICR.Domain.Model.MemberAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ICR.Domain.DTOs
{
    public class CellDTO
    {
        public string Name { get; private set; }
        public long ChurchId { get; private set; }
        public long? ResponsibleId { get; private set; }

    }

    public class CellResponseDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public long ChurchId { get; private set; }
        public string ChurchName { get; private set; }
        public long? ResponsibleId { get; private set; }
        public string? ResponsibleName { get; private set; }
        public CellResponseDTO(
        long id,
        string name,
        long churchId,
        string churchName,
        long? responsibleId,
        string? responsibleName)
        {
            Id = id;
            Name = name;
            ChurchId = churchId;
            ChurchName = churchName;
            ResponsibleId = responsibleId;
            ResponsibleName = responsibleName;
        }

    }
}
