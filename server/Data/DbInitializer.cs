using Microsoft.EntityFrameworkCore;
using RequestsManager.Api.Models;

namespace RequestsManager.Api.Data
{
    public static class DbInitializer
    {
        private const int TotalRecords = 100_000;
        private const int BatchSize = 5_000;

        public static void Initialize(AppDbContext context)
        {
            // כבר קיימות רשומות – אין צורך לייצר מחדש
            if (context.Tickets.Any())
            {
                return;
            }

            var organizations = new[] { "Tech Corp", "Alpha Industries", "Global Soft", "Beta Ltd", "Delta Solutions", "Omega Systems", "Prime Services" };
            var statuses = new[] { "New", "InProgress", "Completed", "Waiting" };
            var priorities = new[] { "High", "Medium", "Low" };
            var assignees = new[] { "Alice", "Bob", "Charlie", "Diana", "Unassigned" };
            var topics = new[]
            {
                "Login issue", "Invoice error", "Payment failure", "Report not loading",
                "Password reset", "Data export problem", "Integration timeout",
                "Missing permissions", "Slow dashboard", "Email notification bug"
            };

            // Seed קבוע – הנתונים זהים בכל הרצה
            var random = new Random(42);
            var tickets = new List<Ticket>(BatchSize);

            for (int i = 1; i <= TotalRecords; i++)
            {
                // מתחילים מיום אחד אחורה כדי ש-UpdatedAt לא ייפול בעתיד
                var createdDate = DateTime.UtcNow.AddDays(-random.Next(1, 365));

                tickets.Add(new Ticket
                {
                    Title = $"{topics[random.Next(topics.Length)]} - request #{i}",
                    OrganizationName = organizations[random.Next(organizations.Length)],
                    Status = statuses[random.Next(statuses.Length)],
                    Priority = priorities[random.Next(priorities.Length)],
                    AssignedTo = assignees[random.Next(assignees.Length)],
                    CreatedAt = createdDate,
                    UpdatedAt = createdDate.AddHours(random.Next(1, 24))
                });

                if (tickets.Count == BatchSize)
                {
                    SaveBatch(context, tickets);
                }
            }

            if (tickets.Count > 0)
            {
                SaveBatch(context, tickets);
            }
        }

        private static void SaveBatch(AppDbContext context, List<Ticket> tickets)
        {
            context.Tickets.AddRange(tickets);
            context.SaveChanges();
            context.ChangeTracker.Clear(); // משחרר את הרשומות מהזיכרון
            tickets.Clear();
        }
    }
}