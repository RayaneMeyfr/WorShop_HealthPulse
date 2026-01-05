using PrescriptionService.Application.DTOs;
using PrescriptionService.Application.Mappers;
using PrescriptionService.Domaine.Ports;

namespace PrescriptionService.Application.Services
{
    public class PrescriptionAppService : IPrescriptionAppService
    {
        private readonly IPrescriptionRepository _repository;

        public PrescriptionAppService(IPrescriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PrescriptionResponseDto>> GetAllAsync()
        {
            var prescriptions = await _repository.GetAllAsync();
            return PrescriptionMapper.ToDtoList(prescriptions);
        }

        public async Task<IEnumerable<PrescriptionResponseDto>?> GetByIdConsultationAsync(Guid id)
        {
            var prescription = await _repository.GetByConsultationIdAsync(id);
            return prescription != null ? PrescriptionMapper.ToDtoList(prescription) : null;
        }

        public async Task<PrescriptionResponseDto?> GetByIdAsync(Guid id)
        {
            var prescription = await _repository.GetByIdAsync(id);
            return prescription != null ? PrescriptionMapper.ToDto(prescription) : null;
        }

        public async Task<TotalPrisesResponseDto?> GetTotalPriseAsync(Guid prescriptionId)
        {
            var prescription = await _repository.GetByIdAsync(prescriptionId);
            if (prescription == null) return null;

            return new TotalPrisesResponseDto
            {
                PrescriptionId = prescription.Id,
                Medicament = prescription.Medicament,
                Frequence = prescription.Frequence,
                DureeJours = prescription.DureeJours,
                TotalPrises = prescription.CalculerTotalPrises()
            };
        }

        public async Task<PrescriptionResponseDto> CreateAsync(PrescriptionRequestDto dto)
        {
            var prescription = PrescriptionMapper.ToEntity(dto);
            var created = await _repository.CreateAsync(prescription);
            return PrescriptionMapper.ToDto(created);
        }

        public async Task<PrescriptionResponseDto?> UpdateAsync(Guid id, PrescriptionRequestDto dto)
        {
            var prescription = PrescriptionMapper.ToEntity(dto);
            var updated = await _repository.UpdateAsync(id, prescription);
            return updated != null ? PrescriptionMapper.ToDto(updated) : null;
        }

    }
}
