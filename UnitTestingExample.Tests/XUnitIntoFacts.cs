namespace UnitTestingExample.Tests
{
    using FluentAssertions;

    using Xunit;

    public class XUnitIntoFacts
    {
        [Fact]
        public void Should_add_two_numbers()
        {
            const int a = 2;
            const int b = 3;
            const int expectedResult = 5;

            // Act
            var result = a + b;

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}