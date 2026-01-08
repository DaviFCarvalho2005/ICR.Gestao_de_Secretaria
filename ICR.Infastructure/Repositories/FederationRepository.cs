using ICR.Domain.Model.ChurchAggregate;
using ICR.Application.ViewModel;
using ICR.Domain.Model.FederationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICR.Infra.Repositories
{
    public class FederationRepository : IFederationRepository
    {
        private readonly ConnectionContext _context;

        public FederationRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Federation federation)
        {
            _context.Federations.Add(federation);
            _context.SaveChanges();
        }       

        // GET by id
        public Federation? GetbyId(long id)
        {
            return _context.Federations.Find(id);
        }

        public List<Federation> Get(int pageNumber, int pageQuantity)
        {
            return _context.Federations
                .OrderBy(f => f.Name)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public void Delete(long id)
        {
            var federation = GetbyId(id);
            if (federation == null)
                throw new InvalidOperationException("Comissão Federada não encontrada");

            _context.Federations.Remove(federation);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Church> GetChurchesByFederationId(long federationId)
        {
            return _context.Churches
                .Where(c => c.FederationId == federationId)
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
