namespace HealthDashboardService.Application.DTOs
{
    public class DashboardResponseDto
    {
        public int TotalPatients { get; set; }
        public Dictionary<string, int> ConsultationsParType { get; set; } = new();
        public decimal ChiffreAffairesTotal { get; set; }
        public int TotalPrescriptions { get; set; }
        public Dictionary<string, int> PatientsParGroupeSanguin { get; set; } = new();
        public DateTime GenereLe { get; set; } = DateTime.UtcNow;
    }

    public class PatientHistoriqueResponseDto
    {
        public PatientDto Patient { get; set; } = null!;
        public List<ConsultationDto> Consultations { get; set; } = new();
        public List<PrescriptionDto> Prescriptions { get; set; } = new();
    }
}
