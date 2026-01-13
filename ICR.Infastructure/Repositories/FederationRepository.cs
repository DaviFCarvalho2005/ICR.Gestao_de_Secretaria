using ICR.Application.Services;
using ICR.Application.ViewModel;
using ICR.Domain.Model.FederationAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICR.Infra.Repositories
{
    public class FederationRepository : IFederationRepository
    {
        private readonly ConnectionContext _context;
        private readonly IdSequenceService _idSequenceService;

        public FederationRepository(ConnectionContext context)
        {
            _context = context;
            _idSequenceService = new IdSequenceService(_context);
        }

        // Adiciona uma nova federação
        public async Task<IEnumerable<Federation>> AddAsync(Federation federation)
        {
            // Gerando novo ID corretamente
            var newId = await _idSequenceService.GetNextIdAsync<Federation>();

            federation.Id = newId;

            await _context.Federations.AddAsync(federation);
            await _context.SaveChangesAsync();

            return await _context.Federations.ToListAsync();
        }

        // Pega uma federação por ID
        public async Task<Federation?> GetByIdAsync(long id)
        {
            return await _context.Federations
                                 .FirstOrDefaultAsync(f => f.Id == id);
        }

        // Retorna todas as federações
        public async Task<IEnumerable<Federation>> GetAllFederationsAsync()
        {
            return await _context.Federations.ToListAsync();
        }

        // Atualiza federação existente
        

        public async void UpdateAsync(Federation federation)
        {
            _context.Federations.Update(federation);
            await _context.SaveChangesAsync();
        }

        public async void DeleteAsync(Federation federation)
        {
            _context.Federations.Remove(federation);
            await _context.SaveChangesAsync();
        }
    }
}
