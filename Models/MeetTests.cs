using Xunit;
using CSharpPractice.Models;
using System;
using System.Collections.Generic;

namespace CSharpPractice.Tests.Models
{
    public class MeetTests
    {
        [Fact]
        public void Meet_Can_Set_And_Get_Properties()
        {
            var date = new DateTime(2024, 5, 8);
            var results = new List<PowerliftingResult>
            {
                new PowerliftingResult {
                    Id = 1,
                    Sex = "Male",
                    Name = "Test Name",
                    MeetID = 0,
                    Equipment = "Test",
                    Division = "Test",
                    Age = 20
                },
                new PowerliftingResult {
                    Id = 2,
                    Sex = "Male",
                    Name = "Test Name",
                    MeetID = 0,
                    Equipment = "Test",
                    Division = "Test",
                    Age = 20
                }
            };

            var meet = new Meet
            {
                Id = 1,
                MeetID = 123,
                MeetPath = "/path/to/meet",
                Federation = "USAPL",
                Date = date,
                MeetCountry = "USA",
                MeetState = "ND",
                MeetTown = "Fargo",
                MeetName = "North Dakota State Championships",
                Results = results
            };

            Assert.Equal(1, meet.Id);
            Assert.Equal(123, meet.MeetID);
            Assert.Equal("/path/to/meet", meet.MeetPath);
            Assert.Equal("USAPL", meet.Federation);
            Assert.Equal(date, meet.Date);
            Assert.Equal("USA", meet.MeetCountry);
            Assert.Equal("ND", meet.MeetState);
            Assert.Equal("Fargo", meet.MeetTown);
            Assert.Equal("North Dakota State Championships", meet.MeetName);
            Assert.Equal(2, meet.Results.Count);
            Assert.Equal(1, meet.Results[0].Id);
            Assert.Equal(2, meet.Results[1].Id);
        }
    }
}
