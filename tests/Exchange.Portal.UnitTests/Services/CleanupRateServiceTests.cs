using System.Data;
using Exchange.Portal.Infrastructure.Documents;
using Marten;
using Microsoft.Extensions.Logging;
using Moq;

namespace Exchange.Portal.UnitTests.Services;

[UnitTest]
public class CleanupRateServiceTests
{
    private readonly Mock<IDocumentStore> _documentStoreMock = new();
    private readonly Mock<IDocumentSession> _sessionMock = new();
    private readonly Mock<ILogger<CleanupRateService>> _loggerMock = new();

    private readonly CleanupRateService _sut;

    public CleanupRateServiceTests()
    {
        _sut = new CleanupRateService(_documentStoreMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CleanupAsync_PassedTimeLimit_ShouldCallDeleteWhere()
    {
        //arrange
        DateTimeOffset timeLimit = DateTimeOffset.UtcNow;
        _documentStoreMock.Setup(x => x.LightweightSession(IsolationLevel.ReadCommitted)).Returns(_sessionMock.Object);

        //act
        await _sut.CleanupAsync(timeLimit);

        //assert
        _sessionMock.Verify(x => x.DeleteWhere<ExchangeRateDocument>(ex => ex.Timestamp < timeLimit), Times.Once);
        _sessionMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task CleanupAsync_ThrowsException_ShouldNotCallDeleteWhere()
    {
        //arrange
        DateTimeOffset timeLimit = DateTimeOffset.UtcNow;
        _documentStoreMock.Setup(x => x.LightweightSession(IsolationLevel.ReadCommitted)).Throws<Exception>();

        //act
        await _sut.CleanupAsync(timeLimit);

        //assert
        _sessionMock.Verify(x => x.DeleteWhere<ExchangeRateDocument>(ex => ex.Timestamp < timeLimit), Times.Never);
        _sessionMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}