using PrescriptionService.Domaine.Entities;
using PrescriptionService.Domaine.Ports;

namespace PrescriptionService.Infrastructure.Persistance
{
    public class InMemoryPrescriptionRepository : IPrescriptionRepository
    {
        private readonly List<Prescription> _prescriptions = new();

        public Task<IEnumerable<Prescription>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Prescription>>(_prescriptions);
        }
        public Task<IEnumerable<Prescription>?> GetByConsultationIdAsync(Guid consultationId)
        {
            var prescriptions = _prescriptions
                .Where(c => c.ConsultationId == consultationId)
                .AsEnumerable();

            return Task.FromResult(prescriptions);
        }

        public Task<Prescription?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_prescriptions.FirstOrDefault(p => p.Id == id));
        }

        public Task<Prescription> CreateAsync(Prescription prescription)
        {
            prescription.Id = Guid.NewGuid();
            _prescriptions.Add(prescription);
            return Task.FromResult(prescription);
        }

        public Task<Prescription?> UpdateAsync(Guid id, Prescription prescription)
        {
            var existing = _prescriptions.FirstOrDefault(p => p.Id == id);
            if (existing == null) return Task.FromResult<Prescription?>(null);

            return Task.FromResult<Prescription?>(existing);
        }
    }
}
