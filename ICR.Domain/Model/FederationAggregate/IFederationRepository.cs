using ICR.Domain.Model.ChurchAggregate;
using System.Collections.Generic;

namespace ICR.Domain.Model.FederationAggregate
{
    public interface IFederationRepository
    {
        void Add(Federation federation);
        Federation? GetbyId(long id);
        List<Federation> Get(int pageNumber, int pageQuantity);
        void Delete(long id);
        void Save();

        List<Church> GetChurchesByFederationId(long federationId);
    }
}
