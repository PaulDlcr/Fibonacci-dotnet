using System.Threading.Tasks;
using System.Collections.Generic;
using Leonardo;
using Xunit;

namespace Leonardo.Tests
{
    public class FibonacciTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        [InlineData(6, 8)]
        public void Run_ValidInput_ReturnsCorrectResult(int input, int expected)
        {
            // Act
            var result = Fibonacci.Run(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task RunAsync_ValidInputs_ReturnsCorrectResults()
        {
            // Arrange
            string[] inputs = { "5", "6", "7" };
            var expectedResults = new List<FibonacciResult>
            {
                new FibonacciResult(5, 5),
                new FibonacciResult(6, 8),
                new FibonacciResult(7, 13),
            };

            // Act
            var results = await Fibonacci.RunAsync(inputs);

            // Assert
            Assert.Equal(expectedResults.Count, results.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.Equal(expectedResults[i].Input, results[i].Input);
                Assert.Equal(expectedResults[i].Result, results[i].Result);
            }
        }

        [Fact]
        public async Task RunAsync_InvalidInputs_ThrowsException()
        {
            // Arrange
            string[] inputs = { "5", "invalid", "-2" };

            // Act & Assert
            await Assert.ThrowsAsync<FormatException>(() => Fibonacci.RunAsync(inputs));
        }

        [Fact]
        public async Task RunAsync_EmptyInputs_ReturnsEmptyList()
        {
            // Arrange
            string[] inputs = { };

            // Act
            var results = await Fibonacci.RunAsync(inputs);

            // Assert
            Assert.Empty(results);
        }
    }
}