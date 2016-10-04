namespace UnitTestingExample.Tests.BaseExamples
{
    using System;
    using System.Threading;

    using Xunit;

    // ReSharper disable once InconsistentNaming
    public class E2_XUnit_Helpers
    {
        // [ ] Log to console using ITestOutputHelper

        [Fact]
        public void Write_to_console()
        {
            Console.WriteLine("Test started.");

            // Act

            Thread.Sleep(10);

            // Assert

            Console.WriteLine("Test ended");
        }
    }
}