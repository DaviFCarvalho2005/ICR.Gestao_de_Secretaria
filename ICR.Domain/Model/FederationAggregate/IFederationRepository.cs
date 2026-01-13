using ICR.Domain.Model.ChurchAggregate;
using ICR.Domain.Model.MemberAggregate;
using System.Collections.Generic;

namespace ICR.Domain.Model.FederationAggregate
{
    public interface IFederationRepository
    {
        Task<IEnumerable<Federation>> AddAsync(Federation federation);
        Task<Federation?> GetByIdAsync(long id);
        Task<IEnumerable<Federation>> GetAllFederationsAsync();
        void UpdateAsync(Federation federation);
        void DeleteAsync(Federation federation);

    }
}
