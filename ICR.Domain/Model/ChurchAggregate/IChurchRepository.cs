using ICR.Domain.DTOs;
using ICR.Domain.Model.CellAggregate;
using ICR.Domain.Model.ChurchAggregate;
using ICR.Domain.Model.FederationAggregate;
using ICR.Domain.Model.MemberAggregate;
using ICR.Domain.Model.MinisterAggregate;
using System.Collections.Generic;

namespace ICR.Domain.Model.ChurchAggregate
{
    public interface IChurchRepository
    {
        Task AddAsync(Church church);
        Task<IEnumerable<ChurchResponseDto>> GetAllChurchsAsync(int pageNumber, int pageQuantity);
        Task<ChurchResponseDto?> GetByIdAsync(long id);
        Task<IEnumerable<ChurchResponseDto>> GetChurchsbyFederationId(long federationId);
        Task<bool> DeleteAsync(long id);
        Task<bool> UpdateAsync(long id, Church updatedChurch);
        Task SaveAsync();
    }
}
