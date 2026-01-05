using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrescriptionService.Application.DTOs;
using PrescriptionService.Application.Services;

namespace PrescriptionService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionAppService _service;

        public PrescriptionsController(IPrescriptionAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescriptionResponseDto>>> GetAll()
        {
            var prescriptions = await _service.GetAllAsync();
            return Ok(prescriptions);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PrescriptionResponseDto>> GetById(Guid id)
        {
            var prescription = await _service.GetByIdAsync(id);
            if (prescription == null)
                return NotFound(new { message = $"Prescription avec l'ID {id} non trouvé" });

            return Ok(prescription);
        }

        [HttpGet("consultation/{consultationId:guid}")]
        public async Task<ActionResult<PrescriptionResponseDto>> GetByIdConsultationAsync(Guid consultationId)
        {
            var prescriptions = await _service.GetByIdConsultationAsync(consultationId);
            if (prescriptions == null)
                return NotFound(new { message = $"Prescription avec l'ID {consultationId} non trouvé" });

            return Ok(prescriptions);
        }

        [HttpGet("{id}/total-prise")]
        public async Task<IActionResult> GetTotalPriseAsync(Guid id)
        {
            var result = await _service.GetTotalPriseAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PrescriptionResponseDto>> Create([FromBody] PrescriptionRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PrescriptionResponseDto>> Update(Guid id, [FromBody] PrescriptionRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = $"Prescription avec l'ID {id} non trouvé" });

            return Ok(updated);
        }

    }
}
