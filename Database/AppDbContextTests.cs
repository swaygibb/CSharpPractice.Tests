using CSharpPractice.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CSharpPractice.Tests.Database
{
    public class AppDbContextTests
    {
        [Fact]
        public async Task Can_Save_And_Retrieve_Meet_And_PowerliftingResult()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var meet = new Meet
                {
                    MeetID = 123,
                    MeetName = "Test Meet"
                };

                context.Meets.Add(meet);

                var result = new PowerliftingResult
                {
                    MeetID = 123,
                    Name = "John Doe",
                    Sex = "M",
                    Equipment = "Raw",
                    Age = 30,
                    Division = "Open"
                };

                context.PowerliftingResults.Add(result);
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var savedMeet = await context.Meets.AsNoTracking().Include(m => m.Results).FirstOrDefaultAsync();
                var savedResult = await context.PowerliftingResults.AsNoTracking().FirstOrDefaultAsync();

                Assert.NotNull(savedMeet);
                Assert.Equal(123, savedMeet.MeetID);
                Assert.Equal("Test Meet", savedMeet.MeetName);

                Assert.NotNull(savedResult);
                Assert.Equal("John Doe", savedResult.Name);
                Assert.Equal(savedMeet.MeetID, savedResult.MeetID);
            }
        }
    }
}
