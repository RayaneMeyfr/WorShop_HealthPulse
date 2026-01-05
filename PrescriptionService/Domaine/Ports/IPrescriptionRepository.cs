using PrescriptionService.Domaine.Entities;

namespace PrescriptionService.Domaine.Ports
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task<Prescription?> GetByIdAsync(Guid id);
        Task<IEnumerable<Prescription>> GetByConsultationIdAsync(Guid consultationId);
        Task<Prescription> CreateAsync(Prescription prescription);
        Task<Prescription?> UpdateAsync(Guid id, Prescription prescription);
    }
}
