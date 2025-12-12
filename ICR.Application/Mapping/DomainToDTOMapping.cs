using AutoMapper;
using ICRManagement.Domain.DTOs;
using ICRManagement.Domain.Model.FederationAggregate;

namespace ICRManagement.Application.Mapping
{
    public class DomainToDTOMapping: Profile 
    {
        public DomainToDTOMapping()
        {
            CreateMap<Federation, FederationDTO>();
        }
    }
}
