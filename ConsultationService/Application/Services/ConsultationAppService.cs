using ConsultationService.Application.DTOs;
using ConsultationService.Application.Mappers;
using ConsultationService.Domaine.Ports;

namespace ConsultationService.Application.Services
{
    public class ConsultationAppService : IConsultationAppService
    {
        private readonly IConsultationRepository _repository;

        public ConsultationAppService(IConsultationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConsultationResponseDto>> GetAllAsync()
        {
            var consultations = await _repository.GetAllAsync();
            return ConsultationMapper.ToDtoList(consultations);
        }

        public async Task<IEnumerable<ConsultationResponseDto>?> GetByIdPatientAsync(Guid id)
        {
            var consultation = await _repository.GetByIdPatientAsync(id);
            return consultation != null ? ConsultationMapper.ToDtoList(consultation) : null;
        }

        public async Task<ConsultationResponseDto?> GetByIdAsync(Guid id)
        {
            var consultation = await _repository.GetByIdAsync(id);
            return consultation != null ? ConsultationMapper.ToDto(consultation) : null;
        }

        public async Task<CoutHoraireResponseDto?> GetCoutHoraireAsync(Guid consultationId)
        {
            var consultation = await _repository.GetByIdAsync(consultationId);
            if (consultation == null) return null;

            return new CoutHoraireResponseDto
            {
                ConsultationId = consultation.Id,
                Tarif = consultation.Tarif,
                DureeMinutes = consultation.DureeMinutes,
                CoutHoraire = consultation.CalculerCoutHoraire()
            };
        }

        public async Task<ConsultationResponseDto> CreateAsync(ConsultationRequestDto dto)
        {
            var consultation = ConsultationMapper.ToEntity(dto);
            var created = await _repository.CreateAsync(consultation);
            return ConsultationMapper.ToDto(created);
        }

        public async Task<ConsultationResponseDto?> UpdateAsync(Guid id, ConsultationRequestDto dto)
        {
            var consultation = ConsultationMapper.ToEntity(dto);
            var updated = await _repository.UpdateAsync(id, consultation);
            return updated != null ? ConsultationMapper.ToDto(updated) : null;
        }

    }
}
