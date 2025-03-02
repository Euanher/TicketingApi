using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketingApi.Models;  // Ensure models are referenced
using System.Collections.Generic;
using System.Linq;

namespace TicketingApi.Controller  // Corrected namespace to match folder structure
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController(ILogger<TicketsController> logger) : ControllerBase
    {
        // Primary constructor used for dependency injection
        private readonly ILogger<TicketsController> _logger = logger;

        // Simplified collection initialization
        private static readonly List<Ticket> Tickets =
        [
            new Ticket { Id = 1, Title = "Sample Ticket 1", Description = "Description of ticket 1", Status = "Open", AssignedUser = new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" } },
            new Ticket { Id = 2, Title = "Sample Ticket 2", Description = "Description of ticket 2", Status = "Closed", AssignedUser = new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" } }
        ];

        // GET: api/tickets
        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTickets()
        {
            _logger.LogInformation("Fetching all tickets");
            return Ok(Tickets);
        }

        // GET: api/tickets/{id}
        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            var ticket = Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                _logger.LogWarning("Ticket with ID {TicketId} not found", id);
                return NotFound(new { message = "Ticket not found" });
            }
            return Ok(ticket);
        }

        // POST: api/tickets
        [HttpPost]
        public ActionResult<Ticket> CreateTicket([FromBody] Ticket newTicket)
        {
            if (newTicket == null || string.IsNullOrEmpty(newTicket.Title))
            {
                _logger.LogWarning("Invalid ticket data received");
                return BadRequest(new { message = "Invalid ticket data" });
            }

            newTicket.Id = Tickets.Count > 0 ? Tickets.Max(t => t.Id) + 1 : 1;
            Tickets.Add(newTicket);
            _logger.LogInformation("Ticket {TicketId} created successfully", newTicket.Id);

            return CreatedAtAction(nameof(GetTicket), new { id = newTicket.Id }, newTicket);
        }

        // PUT: api/tickets/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTicket(int id, [FromBody] Ticket updatedTicket)
        {
            var ticket = Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                _logger.LogWarning("Update failed: Ticket with ID {TicketId} not found", id);
                return NotFound(new { message = "Ticket not found" });
            }

            ticket.Title = updatedTicket.Title ?? ticket.Title;
            ticket.Description = updatedTicket.Description ?? ticket.Description;
            ticket.Status = updatedTicket.Status ?? ticket.Status;
            ticket.AssignedUser = updatedTicket.AssignedUser ?? ticket.AssignedUser;

            _logger.LogInformation("Ticket {TicketId} updated successfully", id);
            return NoContent();
        }

        // DELETE: api/tickets/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            var ticket = Tickets.FirstOrDefault(t => t.Id == id);
            if (ticket == null)
            {
                _logger.LogWarning("Delete failed: Ticket with ID {TicketId} not found", id);
                return NotFound(new { message = "Ticket not found" });
            }

            Tickets.Remove(ticket);
            _logger.LogInformation("Ticket {TicketId} deleted successfully", id);
            return NoContent();
        }
    }
}


