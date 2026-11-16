namespace Lab11_MercyJanampa.Domain.Entities;

public class Ticket
{
    public Guid TicketId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public User User { get; set; } = null!;

    public ICollection<Response> Responses { get; set; } = new List<Response>();
}