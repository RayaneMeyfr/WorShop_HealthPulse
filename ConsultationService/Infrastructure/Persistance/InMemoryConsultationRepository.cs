using ConsultationService.Domaine.Entities;
using ConsultationService.Domaine.Ports;

namespace ConsultationService.Infrastructure.Persistance
{
    public class InMemoryConsultationRepository : IConsultationRepository
    {
        private readonly List<Consultation> _consultations = new();

        public Task<IEnumerable<Consultation>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Consultation>>(_consultations);
        }
         public Task<IEnumerable<Consultation>?> GetByIdPatientAsync(Guid patientId)
        {
            var consultations = _consultations
                .Where(c => c.PatientId == patientId)
                .AsEnumerable();

            return Task.FromResult(consultations);
        }

        public Task<Consultation?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_consultations.FirstOrDefault(p => p.Id == id));
        }

        public Task<Consultation> CreateAsync(Consultation consultation)
        {
            consultation.Id = Guid.NewGuid();
            consultation.DateConsultation = DateTime.UtcNow;
            _consultations.Add(consultation);
            return Task.FromResult(consultation);
        }

        public Task<Consultation?> UpdateAsync(Guid id, Consultation consultation)
        {
            var existing = _consultations.FirstOrDefault(p => p.Id == id);
            if (existing == null) return Task.FromResult<Consultation?>(null);

            return Task.FromResult<Consultation?>(existing);
        }

        
    }
}
