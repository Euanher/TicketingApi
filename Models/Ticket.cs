// Ticket.cs
namespace TicketingApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        // Foreign Key
        public int AssignedUserId { get; set; }

        // Navigation Property (Foreign Key relationship)
        public User? AssignedUser { get; set; }
    }
}

// User.cs
namespace TicketingApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }

        // Navigation Property (One-to-many relationship)
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
