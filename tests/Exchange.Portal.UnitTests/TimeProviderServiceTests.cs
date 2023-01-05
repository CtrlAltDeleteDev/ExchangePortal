namespace Exchange.Portal.UnitTests;

[UnitTest]
public class TimeProviderServiceTests
{
    private readonly TimeProviderService _sut = new(); 
    
    [Fact]
    public void GetDateTimeOffsetUTC_Now_ShouldDateTimeOffsetInUtc()
    {
        // arrange // act
        DateTimeOffset result = _sut.GetDateTimeOffsetUTC();

        // assert
        result.UtcDateTime.Kind.Should().Be(DateTimeKind.Utc);
    }
}