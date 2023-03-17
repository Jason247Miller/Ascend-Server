

using Microsoft.EntityFrameworkCore;
namespace Models;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {}
    public DbSet<GuidedJournalLog> GuidedJournalLogs { get; set; }
    public DbSet<GuidedJournalEntry> GuidedJournalEntries { get; set; }
    public DbSet<Habit> Habits { get; set; }
    public DbSet<HabitCompletionLog> HabitCompletionLogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<WellnessRating> WellnessRatings { get; set; }

    public void Seed(IServiceProvider serviceProvider)
    {
        // Populate the in-memory database with mock data
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApiContext>();

            //seed data below
            Users.AddRange(new List<User>
         {
            new User
            {
                Id = 1,
                FirstName = "Jason",
                LastName = "Miller",
                Email = "jason.miller@gmail.com",
                Password = "testPassword123!"
            }
        });

            WellnessRatings.AddRange(new List<WellnessRating>
        {
            new WellnessRating
            {
                Id = 1,
                UserId = 1,
                Date =  new DateOnly(2023, 3, 16),
                SleepRating = 7,
                ExerciseRating = 5,
                NutritionRating = 2,
                StressRating = 9,
                SunlightRating = 4,
                MindfulnessRating = 2,
                ProductivityRating = 9,
                MoodRating = 9,
                EnergyRating = 9,
                OverallDayRating = 4
            }
        });

            GuidedJournalEntries.AddRange(new List<GuidedJournalEntry>
        {
            new GuidedJournalEntry
            {
                Id = 1,
                UserId = 1,
                EntryName = "What are you most greatful for?",
                Uuid = "2e2bd1d4-c4a3-475a-bc8a-5aea1156e0ec",
                Deleted = false
            },

          new GuidedJournalEntry
          {
            Id = 2,
            UserId = 1,
            EntryName = "What did you learn today?",
            Uuid = "d58a9560-3ed8-4eaa-b97e-c558179861e9",
            Deleted = false
          }
        });

            Habits.AddRange(new List<Habit>
        {
         new Habit {
                Id = 1,
                UserId = 1,
                HabitName = "20 minutes of cardio",
                Uuid = "d58a9560-3ed8-4eaa-b97e-c558179861e8",
                Deleted = false
        },

        new Habit
        {
            Id = 2,
            UserId = 1,
            HabitName = "Learned 1 new thing",
            Uuid = "2e2bd1d4-c4a3-475a-bc8a-5aea1156e0ec",
            Deleted = false
        }});

            HabitCompletionLogs.AddRange(new List<HabitCompletionLog>
        {
        new HabitCompletionLog {
                Id = 1,
                UserId = 1,
                HabitId = "d58a9560-3ed8-4eaa-b97e-c558179861e8",
                Completed = true,
                Date = new DateOnly(2023, 3, 16)
            },

        new HabitCompletionLog
        {
            Id = 2,
            UserId = 1,
            HabitId = "2e2bd1d4-c4a3-475a-bc8a-5aea1156e0ec",
            Completed = true,
            Date = new DateOnly(2023, 3, 16)
        }});

            GuidedJournalLogs.AddRange(new List<GuidedJournalLog>
        {
        new GuidedJournalLog
        {
            Id = 1,
            UserId = 1,
            EntryId = "2e2bd1d4-c4a3-475a-bc8a-5aea1156e0ec",
            EntryTextValue = "text here",
            Date = new DateOnly(2023, 03, 16)
        },
        new GuidedJournalLog
        {
            Id = 2,
            UserId = 2,
            EntryId = "d58a9560-3ed8-4eaa-b97e-c558179861e9",
            EntryTextValue = "text here",
            Date = new DateOnly(2023, 03, 16)
        }});

            SaveChanges();
        }
    }
}
