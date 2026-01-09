using ICR.Domain.Model.CellAggregate;
using ICR.Domain.Model.MemberAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ICR.Infra.Data.Repositories
{
    public class CellRepository : ICellRepository
    {
        private readonly ConnectionContext _context;

        public CellRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Cell cell)
        {
            _context.Cells.Add(cell);
        }

        public Cell? GetById(long id)
        {
            return _context.Cells
                .FirstOrDefault(c => c.Id == id);
        }


        public List<Cell> Get(int pageNumber, int pageQuantity)
        {
            return _context.Cells
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public List<Cell> GetByChurchId(Member leader)
        {
            return _context.Cells
                .Where(c => c.ResponsibleId == leader.Id)
                .ToList();
        }

        public void Delete(long id)
        {
            var cell = GetById(id);
            if (cell != null)
                _context.Cells.Remove(cell);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
