namespace ICR.Domain.DTOs
{
    public class DashboardStatsDTO
    {
        public int TotalMembers { get; set; }
        public int TotalChurches { get; set; }
        public int TotalCells { get; set; }
        public int TotalFamilies { get; set; }
        public int TotalMinisters { get; set; }
        public string ResultMessage { get; set; } = "Sucesso";
    }
}