using System;

namespace RequestsManager.Api.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        // מפתח זר (Foreign Key)
        public int TicketId { get; set; }

        // מאפיין ניווט (Navigation Property) - מאפשר לגשת לכל פרטי הפנייה מתוך לוג השינויים בקלות
        public Ticket Ticket { get; set; } = null!;

        public string PreviousStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
        public string ChangedBy { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}