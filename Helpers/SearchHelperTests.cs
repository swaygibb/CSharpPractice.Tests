using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using CSharpPractice.Helpers;
using CSharpPractice.Models;

namespace CSharpPractice.Tests.Helpers
{
    public class SearchHelperTests
    {
        [Fact]
        public void SearchQuery_Returns_Results_Matching_Name()
        {
            var results = GetSampleResults().AsQueryable();
            string searchString = "Test John";

            var query = SearchHelper.SearchQuery(results, searchString);

            Assert.Single(query);
            Assert.Contains(query, r => r.Name == "Test John");
        }

        [Fact]
        public void SearchQuery_Returns_Results_Matching_Division()
        {
            var results = GetSampleResults().AsQueryable();
            string searchString = "Open";

            var query = SearchHelper.SearchQuery(results, searchString);

            Assert.Single(query);
            Assert.Contains(query, r => r.Division == "Open");
        }

        [Fact]
        public void SearchQuery_Returns_Results_Matching_MeetName()
        {
            var results = GetSampleResults().AsQueryable();
            string searchString = "State Championship";

            var query = SearchHelper.SearchQuery(results, searchString);

            Assert.Single(query);
            Assert.Contains(query, r => r.Meet?.MeetName == "State Championship");
        }

        [Fact]
        public void SearchQuery_Returns_Results_Matching_Federation()
        {
            var results = GetSampleResults().AsQueryable();
            string searchString = "USAPL";

            var query = SearchHelper.SearchQuery(results, searchString);

            Assert.Single(query);
            Assert.Contains(query, r => r.Meet?.Federation == "USAPL");
        }

        private List<PowerliftingResult> GetSampleResults()
        {
            return new List<PowerliftingResult>
            {
                new PowerliftingResult {
                    Sex = "Male",
                    Name = "Test John",
                    MeetID = 0,
                    Equipment = "Test",
                    Division = "Open",
                    Age = 20, 
                    Meet = new Meet { 
                        MeetID = 0, 
                        MeetName = "State Championship", 
                        Federation = "USAPL" 
                    } 
                },
                new PowerliftingResult {
                    Sex = "Male",
                    Name = "Test Name",
                    MeetID = 0,
                    Equipment = "Test",
                    Division = "Test",
                    Age = 20,
                    Meet = new Meet {
                        MeetID = 0,
                        MeetName = "Meet One",
                        Federation = "FED1"
                    }
                },
                new PowerliftingResult { 
                    Sex = "Male",
                    Name = "Test Name",
                    MeetID = 0,
                    Equipment = "Test",
                    Division = "Test",
                    Age = 20,
                    Meet = new Meet {
                        MeetID = 0,
                        MeetName = "Meet Two",
                        Federation = "FED2"
                    }
                },
            };
        }
    }
}
