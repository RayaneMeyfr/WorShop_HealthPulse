using ConsultationService.Application.DTOs;
using ConsultationService.Domaine.Entities;
using ConsultationService.Domaine.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConsultationService.Application.Mappers
{
    public static class ConsultationMapper
    {
        public static ConsultationResponseDto ToDto(Consultation consultation)
        {
            return new ConsultationResponseDto
            {
                Id = consultation.Id,
                PatientId = consultation.PatientId,
                Motif = consultation.Motif.ToString(),
                DateConsultation = consultation.DateConsultation,
                DureeMinutes = consultation.DureeMinutes,
                Tarif = consultation.Tarif
            };
        }

        public static Consultation ToEntity(ConsultationRequestDto dto)
        {
            return new Consultation
            {

                PatientId = dto.PatientId,
                Motif = Enum.Parse<MotifConsultation>(dto.Motif),
                DateConsultation = dto.DateConsultation,
                DureeMinutes = dto.DureeMinutes,
                Tarif = dto.Tarif
            };
        }

        public static IEnumerable<ConsultationResponseDto> ToDtoList(IEnumerable<Consultation> consultations)
        {
            return consultations.Select(ToDto);
        }
    }
}
