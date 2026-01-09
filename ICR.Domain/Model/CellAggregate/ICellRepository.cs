using ICR.Domain.Model.MemberAggregate;

using System.Collections.Generic;

namespace ICR.Domain.Model.CellAggregate
{
    public interface ICellRepository
    {
        void Add(Cell cell);
        Cell? GetById(long id);
        List<Cell> Get(int pageNumber, int pageQuantity);
        List<Cell> GetByChurchId(Member leader);

        void Delete(long id);
        void Save();

    }
}