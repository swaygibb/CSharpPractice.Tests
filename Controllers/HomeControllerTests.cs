using CSharpPractice.Controllers;
using CSharpPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;
using Xunit;

namespace CSharpPractice.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;
        private readonly Mock<ILogger<HomeController>> _mockLogger;

        public HomeControllerTests()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_mockLogger.Object);
        }

        [Fact]
        public void Index_Returns_ViewResult()
        {
            var result = _controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_Returns_ViewResult()
        {
            var result = _controller.Privacy();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_Returns_ViewResult_With_HttpContext_TraceIdentifier_When_No_Activity()
        {
            Activity.Current = null;
            var fakeTraceId = "trace-123";
            var httpContext = new DefaultHttpContext();
            httpContext.TraceIdentifier = fakeTraceId;
            _controller.ControllerContext.HttpContext = httpContext;

            var result = _controller.Error();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ErrorViewModel>(viewResult.Model);
            Assert.Equal(fakeTraceId, model.RequestId);
        }
    }
}
