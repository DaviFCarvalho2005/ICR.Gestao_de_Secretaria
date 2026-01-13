using ICR.Application.Services;
using ICR.Domain.DTOs;
using ICR.Domain.Model.ChurchAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICR.Infra.Data.Repositories
{
    public class ChurchRepository : IChurchRepository
    {
        private readonly ConnectionContext _context;
        private readonly IdSequenceService _idSequenceService;

        public ChurchRepository(ConnectionContext context)
        {
            _context = context;
            _idSequenceService = new IdSequenceService(context);
        }

        public async Task AddAsync(Church church)
        {
            var newId = await _idSequenceService.GetNextIdAsync<Church>();
            church.Id = newId;

            await _context.Churches.AddAsync(church);
        }

        public async Task<ChurchResponseDto?> GetByIdAsync(long id)
        {
            return await _context.Churches
                .Include(c => c.Federation)
                .Include(c => c.Minister)
                .Where(c => c.Id == id)
                .Select(c => new ChurchResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    FederationId = c.FederationId,
                    FederationName = c.Federation != null ? c.Federation.Name : null,
                    MinisterId = c.MinisterId,
                    MinisterName = c.Minister != null ? c.Minister.Member.Name : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ChurchResponseDto>> GetAllChurchsAsync(int pageNumber, int pageQuantity)
        {
            return await _context.Churches
                .Include(c => c.Federation)
                .Include(c => c.Minister)
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Select(c => new ChurchResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    FederationId = c.FederationId,
                    FederationName = c.Federation != null ? c.Federation.Name : null,
                    MinisterId = c.MinisterId,
                    MinisterName = c.Minister != null ? c.Minister.Member.Name : null
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<ChurchResponseDto>> GetChurchsbyFederationId(long id)
        {
            return await _context.Churches
                .Include(c => c.Federation)
                .Include(c => c.Minister)
                .Where(c => c.FederationId == id)
                .Select(c => new ChurchResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    FederationId = c.FederationId,
                    FederationName = c.Federation != null ? c.Federation.Name : null,
                    MinisterId = c.MinisterId,
                    MinisterName = c.Minister != null ? c.Minister.Member.Name : null
                })
                .ToListAsync();
        }


        public async Task<bool> UpdateAsync(long id, Church updatedChurch)
        {
            var church = await _context.Churches
                .FirstOrDefaultAsync(c => c.Id == id);

            if (church == null)
                return false;

            // Atualiza tudo aqui dentro, como você pediu
            church.SetName(updatedChurch.Name);
            church.SetFederationId(updatedChurch.FederationId);
            church.SetAddress(updatedChurch.Address);
            church.SetMinisterId(updatedChurch.MinisterId);

            _context.Churches.Update(church);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(long id)
        {
            var church = await _context.Churches
                .FirstOrDefaultAsync(c => c.Id == id);

            if (church == null)
                return false;

            _context.Churches.Remove(church);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
