using ConsultationService.Application.DTOs;

namespace ConsultationService.Application.Services
{
    public interface IConsultationAppService
    {
        Task<IEnumerable<ConsultationResponseDto>> GetAllAsync();
        Task<ConsultationResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ConsultationResponseDto>?> GetByIdPatientAsync(Guid id);
        Task<ConsultationResponseDto> CreateAsync(ConsultationRequestDto dto);
        Task<ConsultationResponseDto?> UpdateAsync(Guid id, ConsultationRequestDto dto);
        Task<CoutHoraireResponseDto?> GetCoutHoraireAsync(Guid consultationId);
    }
}
