using FabLibrarian.CardLibrary.Services.Local;
using FluentAssertions;
using Xunit;

namespace FabLibrarian.CardLibrary.Tests;

public class LocalServiceTests
{
    [Theory]
    [InlineData("slippery bogle", "Hyper Driver", 10)]
    [InlineData("slippery boggle", "Hyper Driver", 11)]
    public void LevenschtienDistanceTests(string a, string b, int expected)
    {
        a.LevenshteinDistance(b).Should().Be(expected);
    }
}