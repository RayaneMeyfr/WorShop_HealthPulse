using ConsultationService.Domaine.Entities;

namespace ConsultationService.Domaine.Ports
{
    public interface IConsultationRepository
    {
        Task<IEnumerable<Consultation>> GetAllAsync();
        Task<IEnumerable<Consultation>?> GetByIdPatientAsync(Guid id);
        Task<Consultation?> GetByIdAsync(Guid id);
        Task<Consultation> CreateAsync(Consultation consultation);
        Task<Consultation?> UpdateAsync(Guid id, Consultation consultation);
    }
}
