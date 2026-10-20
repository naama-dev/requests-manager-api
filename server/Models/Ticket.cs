using System;
using System.ComponentModel.DataAnnotations;

namespace RequestsManager.Api.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string OrganizationName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "New"; // New, InProgress, Completed, Waiting[cite: 1, 11]

        [Required]
        [MaxLength(50)]
        public string Priority { get; set; } = "Medium"; // High, Medium, Low[cite: 1, 11]

        [MaxLength(100)]
        public string AssignedTo { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // שדה קריטי עבור Optimistic Concurrency (מניעת Lost Update)[cite: 1, 2, 11, 12]
        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}