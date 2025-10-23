using StringManager.Implementation;

namespace StringManager.Tests;

public class StringManagerTests
{
    private readonly RangeValidator _validator = new();

    // Test the single replacement method
    [Fact]
    public void Replace_ShouldReplaceTextInMiddle()
    {
        // Arrange
        string source = "The quick brown fox jumps over the lazy dog.";
        var manager = new MyStringManager(source, _validator);
        string newValue = "silly wolf";
        int startIndex = 4; // Start of 'quick'
        int endIndex = 18;  // End of 'brown fox'
        string expected = "The silly wolf jumps over the lazy dog.";

        // Act
        string actual = manager.Replace(startIndex, endIndex, newValue);

        // Assert
        Assert.Equal(expected, actual);
    }

    // Test the ReplaceMany method with multiple non-overlapping ranges
    [Fact]
    public void ReplaceMany_ShouldReplaceMultipleNonOverlappingRanges()
    {
        // Arrange
        string source = "One two three four five six.";
        var manager = new MyStringManager(source, _validator);
        string newValue = "ZERO";

        // Indices for 'two', 'four', 'six'
        int[][] ranges =
        [
            [4, 6],   // 'two'
            [14, 17], // 'four'
            [24, 26]  // 'six'
        ];
        string expected = "One ZERO three ZERO five ZERO.";

        // Act
        string actual = manager.ReplaceMany(ranges, newValue);

        // Assert
        Assert.Equal(expected, actual);
    }

    // Test the ReplaceMany method with ranges given out of order (must be sorted internally)
    [Fact]
    public void ReplaceMany_ShouldHandleUnsortedRanges()
    {
        // Arrange
        string source = "Apple Banana Carrot Durian";
        var manager = new MyStringManager(source, _validator);
        string newValue = "Fruit";

        // Indices for 'Banana' and 'Apple' given in reverse start order
        int[][] ranges =
        [
            [6, 11],   // 'Banana'
            [0, 4]     // 'Apple'
        ];
        string expected = "Fruit Fruit Carrot Durian";

        // Act
        string actual = manager.ReplaceMany(ranges, newValue);

        // Assert
        Assert.Equal(expected, actual);
    }

    // Test edge case: Replacement at the start and end of the string
    [Fact]
    public void ReplaceMany_ShouldHandleStartAndEndRanges()
    {
        // Arrange
        string source = "START_Middle_END";
        var manager = new MyStringManager(source, _validator);
        string newValue = "Replaced";

        // Indices for 'START' and 'END'
        int[][] ranges =
        [
            [0, 4],     // 'START'
            [13, 15]    // 'END'
        ];
        string expected = "Replaced_Middle_Replaced";

        // Act
        string actual = manager.ReplaceMany(ranges, newValue);

        // Assert
        Assert.Equal(expected, actual);
    }
}