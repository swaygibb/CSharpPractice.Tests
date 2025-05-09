using Xunit;
using CSharpPractice.Models;
using System;

namespace CSharpPractice.Tests.Models
{
    public class PowerliftingResultTests
    {
        [Fact]
        public void PowerliftingResult_Can_Set_And_Get_Properties()
        {
            var result = new PowerliftingResult
            {
                Id = 1,
                MeetID = 123,
                Name = "John Doe",
                Sex = "M",
                Equipment = "Raw",
                Age = 30,
                Division = "Open",
                BodyweightKg = 90,
                WeightClassKg = 93,
                Squat4Kg = null,
                BestSquatKg = 250,
                Bench4Kg = null,
                BestBenchKg = 150,
                Deadlift4Kg = null,
                BestDeadliftKg = 260,
                TotalKg = 660,
                Place = 1,
                Wilks = 420
            };

            Assert.Equal(1, result.Id);
            Assert.Equal(123, result.MeetID);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("M", result.Sex);
            Assert.Equal("Raw", result.Equipment);
            Assert.Equal(30, result.Age);
            Assert.Equal("Open", result.Division);
            Assert.Equal(90, result.BodyweightKg);
            Assert.Equal(93, result.WeightClassKg);
            Assert.Equal(250, result.BestSquatKg);
            Assert.Equal(150, result.BestBenchKg);
            Assert.Equal(260, result.BestDeadliftKg);
            Assert.Equal(660, result.TotalKg);
            Assert.Equal(1, result.Place);
            Assert.Equal(420, result.Wilks);
        }

        [Theory]
        [InlineData(150, "Beginner")]
        [InlineData(250, "Novice")]
        [InlineData(350, "Intermediate")]
        [InlineData(450, "Advanced")]
        [InlineData(550, "Elite")]
        public void LifterRating_Returns_Correct_Category(decimal wilks, string expected)
        {
            var result = new PowerliftingResult
            {
                MeetID = 1,
                Name = "Test",
                Sex = "M",
                Equipment = "Raw",
                Age = 25,
                Division = "Open",
                Wilks = wilks
            };

            Assert.Equal(expected, result.LifterRating);
        }

        [Fact]
        public void Commentary_Returns_Elite_Performance_Message_When_Wilks_Over_500()
        {
            var result = new PowerliftingResult
            {
                MeetID = 1,
                Name = "John Doe",
                Sex = "M",
                Equipment = "Raw",
                Age = 25,
                Division = "Open",
                Wilks = 520
            };

            var message = result.Commentary;

            Assert.Contains("John Doe delivered an elite performance", message);
        }

        [Fact]
        public void Commentary_Returns_Strong_Woman_Message_When_Female_Wilks_Over_400()
        {
            var result = new PowerliftingResult
            {
                MeetID = 1,
                Name = "Jane Doe",
                Sex = "F",
                Equipment = "Raw",
                Age = 30,
                Division = "Open",
                Wilks = 420
            };

            var message = result.Commentary;

            Assert.Contains("Jane Doe is one of the strongest women", message);
        }

        [Fact]
        public void Commentary_Returns_Monster_Deadlift_Message_When_Deadlift_Over_300()
        {
            var result = new PowerliftingResult
            {
                MeetID = 1,
                Name = "Jake",
                Sex = "M",
                Equipment = "Raw",
                Age = 30,
                Division = "Open",
                BestDeadliftKg = 305
            };

            var message = result.Commentary;

            Assert.Contains("Jake's deadlift is monstrous", message);
        }

        [Fact]
        public void Commentary_Returns_Not_Completed_When_Total_Null()
        {
            var result = new PowerliftingResult
            {
                MeetID = 1,
                Name = "No Total",
                Sex = "M",
                Equipment = "Raw",
                Age = 30,
                Division = "Open",
                TotalKg = null
            };

            var message = result.Commentary;

            Assert.Contains("No Total has not completed their total yet.", message);
        }

        [Theory]
        [InlineData("Shane Hammock", true)]
        [InlineData("Jeff Bumanglag", true)]
        [InlineData("Jake Anderson", true)]
        [InlineData("Justin Zottl", true)]
        [InlineData("Unknown Lifter", false)]
        public void IsNotable_Returns_Correct_Value(string name, bool expected)
        {
            var result = new PowerliftingResult
            {
                MeetID = 1,
                Name = name,
                Sex = "M",
                Equipment = "Raw",
                Age = 25,
                Division = "Open"
            };

            Assert.Equal(expected, result.IsNotable);
        }

        [Fact]
        public void Recent_Returns_20_Most_Recent_Results()
        {
            var results = Enumerable.Range(1, 100)
                .Select(i => new PowerliftingResult
                {
                    Id = i,
                    MeetID = 1,
                    Name = $"Lifter {i}",
                    Sex = "M",
                    Equipment = "Raw",
                    Age = 25,
                    Division = "Open"
                })
                .AsQueryable();

            var recentResults = PowerliftingResult.Recent(results).ToList();

            Assert.Equal(20, recentResults.Count);
            Assert.Equal(100, recentResults[0].Id);
            Assert.Equal(81, recentResults.Last().Id);
        }
    }
}
