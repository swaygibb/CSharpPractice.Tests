using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using CSharpPractice.Helpers;
using CSharpPractice.Models;

namespace CSharpPractice.Tests.Helpers
{
    public class PagingHelperTests
    {
        [Fact]
        public void SetPowerliftingPaging_Returns_Correct_Page_And_TotalPages()
        {
            var results = Enumerable.Range(1, 25)
                .Select(i => new PowerliftingResult { 
                    Id = i, 
                    Sex = "Male", 
                    Name = "Test Name", 
                    MeetID = 0, 
                    Equipment = "Test", 
                    Division = "Test",
                    Age = 20
                })
                .ToList()
                .AsQueryable();

            int page = 2;
            int pageSize = 10;

            var (returnedPage, totalPages, pagedResults) = PagingHelper.SetPowerliftingPaging(results, page, pageSize);

            Assert.Equal(page, returnedPage);
            Assert.Equal(3, totalPages); // 25 items, 10 per page → 3 total pages
            Assert.Equal(10, pagedResults.Count()); // page 2 → should have 10 items
            Assert.Equal(11, pagedResults.First().Id); // page 2 starts at item 11
            Assert.Equal(20, pagedResults.Last().Id);  // page 2 ends at item 20
        }

        [Fact]
        public void SetPowerliftingPaging_Returns_Remaining_Items_On_Last_Page()
        {
            var results = Enumerable.Range(1, 23)
                .Select(i => new PowerliftingResult
                {
                    Id = i,
                    Sex = "Male",
                    Name = "Test Name",
                    MeetID = 0,
                    Equipment = "Test",
                    Division = "Test",
                    Age = 20
                })
                .AsQueryable();

            int page = 3;
            int pageSize = 10;

            var (_, totalPages, pagedResults) = PagingHelper.SetPowerliftingPaging(results, page, pageSize);

            Assert.Equal(3, totalPages);
            Assert.Equal(3, pagedResults.Count());
            Assert.Equal(21, pagedResults.First().Id);
            Assert.Equal(23, pagedResults.Last().Id);
        }
    }
}
