using PrescriptionService.Application.DTOs;

namespace PrescriptionService.Application.Services
{
    public interface IPrescriptionAppService
    {
        Task<IEnumerable<PrescriptionResponseDto>> GetAllAsync();
        Task<PrescriptionResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<PrescriptionResponseDto>?> GetByIdConsultationAsync(Guid id);
        Task<PrescriptionResponseDto> CreateAsync(PrescriptionRequestDto dto);
        Task<PrescriptionResponseDto?> UpdateAsync(Guid id, PrescriptionRequestDto dto);
        Task<TotalPrisesResponseDto?> GetTotalPriseAsync(Guid prescriptionId);
    }
}
