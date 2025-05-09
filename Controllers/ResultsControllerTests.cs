using Xunit;
using Microsoft.EntityFrameworkCore;
using CSharpPractice.Controllers;
using CSharpPractice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharpPractice.Tests.Controllers
{
    public class ResultsControllerTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_" + System.Guid.NewGuid())
                .Options;
            var context = new AppDbContext(options);

            var meet = new Meet { Id = 1, MeetID = 100, MeetName = "Test Meet" };
            context.Meets.Add(meet);

            for (int i = 1; i <= 30; i++)
            {
                context.PowerliftingResults.Add(new PowerliftingResult
                {
                    Id = i,
                    MeetID = 100,
                    Meet = meet,
                    Name = $"Lifter {i}",
                    Sex = "M",
                    Equipment = "Raw",
                    Age = 25,
                    Division = "Open",
                    TotalKg = 500,
                    Wilks = 400,
                    BestSquatKg = 200,
                    BestBenchKg = 150,
                    BestDeadliftKg = 250
                });
            }

            context.SaveChanges();
            return context;
        }

        [Fact]
        public void Index_Returns_View_With_Recent_Results()
        {
            var context = GetInMemoryDbContext();
            var controller = new ResultsController(context);

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            var model = result.Model as List<PowerliftingResult>;
            Assert.NotNull(model);
            Assert.Equal(20, model.Count);
            Assert.Equal(30, model.First().Id);
        }

        [Fact]
        public async Task Search_Returns_Filtered_And_Paged_Results()
        {
            var context = GetInMemoryDbContext();
            var controller = new ResultsController(context);

            var result = await controller.Search("Lifter 2", page: 1, pageSize: 5) as ViewResult;

            Assert.NotNull(result);
            var model = result.Model as List<PowerliftingResult>;
            Assert.NotNull(model);
            Assert.True(model.Count >= 1);

            var currentPage = result.ViewData["CurrentPage"];
            var totalPages = result.ViewData["TotalPages"];
            Assert.Equal(1, currentPage);
            Assert.NotNull(totalPages);
        }

        [Fact]
        public void Details_Returns_Correct_Result()
        {
            var context = GetInMemoryDbContext();
            var controller = new ResultsController(context);

            var result = controller.Details(1) as ViewResult;

            Assert.NotNull(result);
            var model = result.Model as PowerliftingResult;
            Assert.NotNull(model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public void Details_Returns_Null_When_Not_Found()
        {
            var context = GetInMemoryDbContext();
            var controller = new ResultsController(context);

            var result = controller.Details(999) as ViewResult;

            Assert.NotNull(result);
            Assert.Null(result.Model);
        }
    }
}
