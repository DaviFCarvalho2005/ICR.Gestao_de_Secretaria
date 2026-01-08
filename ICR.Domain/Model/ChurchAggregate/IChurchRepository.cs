using ICR.Domain.Model.CellAggregate;
using ICR.Domain.Model.ChurchAggregate;
using ICR.Domain.Model.MinisterAggregate;
using ICR.Domain.Model.FederationAggregate;
using System.Collections.Generic;

namespace ICR.Domain.Model.ChurchAggregate
{
    public interface IChurchRepository
    {
        void Add(Church federation);
        Church? GetById(long id);
        List<Church> Get(int pageNumber, int pageQuantity);
        void Delete(long id);
        void Save();
        List<Cell> GetCellsByChurchId(long churchId);
        List<Minister> GetAuxiliarMinisterByChurchID(long churchId);
    }
}
