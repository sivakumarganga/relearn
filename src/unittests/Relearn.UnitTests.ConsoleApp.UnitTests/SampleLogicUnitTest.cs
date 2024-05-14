using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.UnitTests.ConsoleApp.UnitTests
{
    public class SampleLogicUnitTest
    {
        [Fact]
        public void SampleLogic_VerifingLog()
        {

            // Arrange
            var loggerMock = new Mock<ILogger<SampleLogic>>();
            var sampleLogic = new SampleLogic(loggerMock.Object);
            // Act
            sampleLogic.Run();

            // Assert
            loggerMock.Verify(logger => logger.Log(
                                                    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
                                                    0,
                                                    It.Is<It.IsAnyType>((@o, @t) => @o.ToString() == "Hello, World!" && @t.Name == "FormattedLogValues"),
                                                    It.IsAny<Exception>(),
                                                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                                                Times.Once);
            Mock.VerifyAll();
        }
    }
}
