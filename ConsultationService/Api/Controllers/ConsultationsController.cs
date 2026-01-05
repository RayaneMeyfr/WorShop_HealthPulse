using ConsultationService.Application.DTOs;
using ConsultationService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultationService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultationsController : ControllerBase
    {
        private readonly IConsultationAppService _service;

        public ConsultationsController(IConsultationAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationResponseDto>>> GetAll()
        {
            var consultations = await _service.GetAllAsync();
            return Ok(consultations);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ConsultationResponseDto>> GetById(Guid id)
        {
            var consultation = await _service.GetByIdAsync(id);
            if (consultation == null)
                return NotFound(new { message = $"Consultation avec l'ID {id} non trouvé" });

            return Ok(consultation);
        }

        [HttpGet("patient/{patientId:guid}")]
        public async Task<ActionResult<ConsultationResponseDto>> GetByIdPatients(Guid patientId)
        {
            var consultations = await _service.GetByIdPatientAsync(patientId);
            if (consultations == null)
                return NotFound(new { message = $"Consultation avec l'ID {patientId} non trouvé" });

            return Ok(consultations);
        }

        [HttpGet("{id}/cout-horaire")]
        public async Task<IActionResult> GetCoutHoraire(Guid id)
        {
            var result = await _service.GetCoutHoraireAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ConsultationResponseDto>> Create([FromBody] ConsultationRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ConsultationResponseDto>> Update(Guid id, [FromBody] ConsultationRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = $"Consultation avec l'ID {id} non trouvé" });

            return Ok(updated);
        }

    }
}
