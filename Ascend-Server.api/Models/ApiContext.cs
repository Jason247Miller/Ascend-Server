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

            Users.AddRange(new List<User>
         {
            new User
            {
                Id = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
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
                Id = Guid.Parse("348b40fa-c81b-11ed-afa1-0242ac120002"),
                UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
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
                Id = Guid.Parse("48268692-c81b-11ed-afa1-0242ac120002"),
                UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
                EntryName = "What are you most greatful for?",
                Deleted = false
            },

          new GuidedJournalEntry
          {
            Id = Guid.Parse("2102282c-c81c-11ed-afa1-0242ac120002"),
            UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
            EntryName = "What did you learn today?",
            Deleted = false
          }
        });

            Habits.AddRange(new List<Habit>
        {
         new Habit {
                Id = Guid.Parse("e58a9560-3ed8-4eaa-b97e-c958179961e2"),
                UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
                HabitName = "20 minutes of cardio",
                Deleted = false
        },

        new Habit
        {
            Id = Guid.Parse("e58a9560-3ed8-4eaa-b97e-c958179961e3"),
            UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
            HabitName = "Learned 1 new thing",
            Deleted = false
        }});

            HabitCompletionLogs.AddRange(new List<HabitCompletionLog>
        {
        new HabitCompletionLog {
                Id = Guid.Parse("d58a9560-8ed8-4eaa-b97e-c958179961e9"),
                UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
                HabitId = Guid.Parse("e58a9560-3ed8-4eaa-b97e-c958179961e2"),
                Completed = true,
                Date = new DateOnly(2023, 3, 16)
            },

        new HabitCompletionLog
        {
            Id = Guid.Parse("d58a9560-9ed8-4eaa-b97e-c958179961e9"),
            UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
            HabitId = Guid.Parse("e58a9560-3ed8-4eaa-b97e-c958179961e3"),
            Completed = true,
            Date = new DateOnly(2023, 3, 16)
        }});

            GuidedJournalLogs.AddRange(new List<GuidedJournalLog>
        {
        new GuidedJournalLog
        {
            Id = Guid.Parse("a58a9560-3ed8-4eaa-b97e-c958179961e9"),
            UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
            EntryId = Guid.Parse("48268692-c81b-11ed-afa1-0242ac120002"),
            EntryTextValue = "text here",
            Date = new DateOnly(2023, 03, 16)
        },
        new GuidedJournalLog
        {
            Id = Guid.Parse("f58a9560-3ed8-4eaa-b97e-c958179961e9"),
            UserId = Guid.Parse("f2d1b702-c81a-11ed-afa1-0242ac120002"),
            EntryId = Guid.Parse("2102282c-c81c-11ed-afa1-0242ac120002"),
            EntryTextValue = "text here",
            Date = new DateOnly(2023, 03, 16)
        }});

            SaveChanges();
        }
    }
}
