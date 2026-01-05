using PatientService.Domaine.Entities;
using PatientService.Domaine.Entities.Enums;
using PatientService.Domaine.Ports;

namespace PatientService.Infrastructure.Persistance
{
    public class InMemoryPatientRepository : IPatientRepository
    {
        private readonly List<Patient> _patients = new();

        public Task<IEnumerable<Patient>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Patient>>(_patients);
        }

        public Task<Patient?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_patients.FirstOrDefault(p => p.Id == id));
        }

        public Task<Patient> CreateAsync(Patient patient)
        {
            patient.Id = Guid.NewGuid();
            patient.DateInscription = DateTime.UtcNow;
            _patients.Add(patient);
            return Task.FromResult(patient);
        }

        public Task<Patient?> UpdateAsync(Guid id, Patient patient)
        {
            var existing = _patients.FirstOrDefault(p => p.Id == id);
            if (existing == null) return Task.FromResult<Patient?>(null);

            existing.Nom = patient.Nom;
            existing.DateNaissance = patient.DateNaissance;
            existing.GroupeSanguin = patient.GroupeSanguin;

            return Task.FromResult<Patient?>(existing);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var patient = _patients.FirstOrDefault(p => p.Id == id);
            if (patient == null) return Task.FromResult(false);

            _patients.Remove(patient);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Patient>> GetByGroupeSanguinAsync(GroupeSanguin groupe)
        {
            return Task.FromResult<IEnumerable<Patient>>(
                _patients.Where(p => p.GroupeSanguin == groupe));
        }
    }
}
