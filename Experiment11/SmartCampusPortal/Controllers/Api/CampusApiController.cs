using Microsoft.AspNetCore.Mvc;
using SmartCampusPortal.Data;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Controllers.Api
{
    [ApiController]
    [Route("api/campus")]
    [Produces("application/json")]
    public class CampusApiController : ControllerBase
    {
        private readonly ICampusRepository _repository;

        public CampusApiController(ICampusRepository repository)
        {
            _repository = repository;
        }

        // GET: /api/campus?category={category}
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? category = null)
        {
            var tickets = _repository.GetAllTickets(category);
            return Ok(tickets);
        }

        // GET: /api/campus/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var ticket = _repository.GetTicketById(id);
            if (ticket == null)
            {
                return NotFound(new { message = $"Campus service ticket #{id} was not found." });
            }
            return Ok(ticket);
        }

        // POST: /api/campus
        [HttpPost]
        public IActionResult Create([FromBody] CampusServiceTicket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = _repository.CreateTicket(ticket);
            return CreatedAtAction(nameof(GetById), new { id = created.TicketId }, created);
        }

        // PUT: /api/campus/{id}/status
        [HttpPut("{id:int}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] StatusUpdateDto? dto, [FromQuery] string? status = null)
        {
            string newStatus = dto?.Status ?? status ?? string.Empty;

            if (string.IsNullOrWhiteSpace(newStatus))
            {
                return BadRequest(new { message = "Status value is required." });
            }

            bool updated = _repository.UpdateTicketStatus(id, newStatus);
            if (!updated)
            {
                return NotFound(new { message = $"Ticket #{id} not found to update status." });
            }

            return Ok(new
            {
                message = "Status updated successfully",
                ticketId = id,
                status = newStatus,
                updatedAt = DateTime.Now
            });
        }

        // DELETE: /api/campus/{id}
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool deleted = _repository.DeleteTicket(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Ticket #{id} not found to delete." });
            }

            return Ok(new
            {
                message = $"Ticket #{id} deleted successfully",
                ticketId = id
            });
        }
    }
}
