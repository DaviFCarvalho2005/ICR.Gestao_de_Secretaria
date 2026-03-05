using ICR.Domain.DTOs;
using ICR.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICR.Infra.Data.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ConnectionContext _context;

        public DashboardRepository(ConnectionContext context) => _context = context;

        public async Task<DashboardStatsDTO> GetTotalStatsAsync() => new DashboardStatsDTO
        {
            TotalMembers = await _context.Members.CountAsync(),
            TotalChurches = await _context.Churches.CountAsync(),
            TotalCells = await _context.Cells.CountAsync(),
            TotalFamilies = await _context.Families.CountAsync(),
            TotalMinisters = await _context.Ministers.CountAsync()
        };
    }
}
    