using System.Collections.Generic;

namespace ICR.Domain.Model.MinisterAggregate
{
    public interface IMinisterRepository
    {
        void Add(Minister minister);
        Minister? GetById(long id);
        List<Minister> Get(int pageNumber, int pageQuantity);
        void Delete(long id);
        void Save();

        // Domain-specific queries
        List<Minister> GetByMemberId(long memberId);
        List<Minister> GetByFamilyId(long familyId);
        Minister? GetByCpf(long cpf);
        List<Minister> GetByChurchId(long churchId);
    }
}