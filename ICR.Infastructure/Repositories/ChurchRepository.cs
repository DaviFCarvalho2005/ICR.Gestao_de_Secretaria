using ICR.Domain.Model.CellAggregate;
using ICR.Domain.Model.ChurchAggregate;
using ICR.Domain.Model.MinisterAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICR.Infra.Repositories
{
    internal class ChurchRepository : IChurchRepository
    {
        private readonly ConnectionContext _context;

        public ChurchRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Church federation)
        {
            _context.Churches.Add(federation);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var church = GetById(id);
            if (church == null)
                throw new InvalidOperationException("Igreja não encontrada");

            _context.Churches.Remove(church);
            _context.SaveChanges();

        }

        public List<Church> Get(int pageNumber, int pageQuantity)
        {
            return _context.Churches
                .OrderBy(f => f.Id)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public Church? GetById(long id)
        {
            return _context.Churches.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
