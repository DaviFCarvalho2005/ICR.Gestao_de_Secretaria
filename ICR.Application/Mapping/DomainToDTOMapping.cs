using AutoMapper;
using ICRManagement.Domain.DTOs;
using ICR.Domain.Model.FederationAggregate;

namespace ICR.Application.Mapping
{
    public class DomainToDTOMapping: Profile 
    {
        public DomainToDTOMapping()
        {
            CreateMap<Federation, FederationDTO>();
        }
    }
}
