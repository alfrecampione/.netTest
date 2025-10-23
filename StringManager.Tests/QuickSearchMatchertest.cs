namespace StringManager.Tests;

/// <summary>
/// Unit tests for the QuickSearchMatcher implementation in the Matchers class.
/// </summary>
public class QuickSearchMatcherTests
{
    // Test case 1: Basic single match
    [Fact]
    public void QuickSearchMatcher_ShouldFindSingleMatch()
    {
        // Arrange
        string source = "The apple is ripe.";
        string valueToMatch = "apple";

        // Match starts at index 4 and ends at index 8 (inclusive).
        int[][] expected = [[4, 8]];

        // Act
        int[][] actual = Matchers.QuickSearchMatcher(source, valueToMatch);

        // Assert
        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected[0], actual[0]);
    }

    // Test case 2: Multiple non-overlapping matches
    [Fact]
    public void QuickSearchMatcher_ShouldFindMultipleMatches()
    {
        // Arrange
        string source = "BANANA BANANA MAN";
        string valueToMatch = "ANA";

        // Expected indices: (1, 3), (3, 5), (8, 10), (10, 12)
        int[][] expected =
        [
            [1, 3],
            [3, 5],
            [8, 10],
            [10, 12]
        ];

        // Act
        int[][] actual = Matchers.QuickSearchMatcher(source, valueToMatch);

        // Assert (Requires sorting to ensure order if necessary, but Quick Search finds them sequentially)
        Assert.Equal(expected.Length, actual.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], actual[i]);
        }
    }

    // Test case 3: Overlapping matches (Quick Search generally finds these unless the shift skips them)
    [Fact]
    public void QuickSearchMatcher_ShouldFindOverlappingMatches()
    {
        // Arrange
        string source = "AAAAA";
        string valueToMatch = "AAA";

        // Expected indices: (0, 2), (1, 3), (2, 4)
        int[][] expected =
        [
            [0, 2],
            [1, 3],
            [2, 4]
        ];

        // Act
        int[][] actual = Matchers.QuickSearchMatcher(source, valueToMatch);

        // Assert
        Assert.Equal(expected.Length, actual.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], actual[i]);
        }
    }

    // Test case 4: No match found
    [Fact]
    public void QuickSearchMatcher_ShouldReturnEmptyArray_WhenNoMatch()
    {
        // Arrange
        string source = "Azure Developer";
        string valueToMatch = "Cloud";

        // Act
        int[][] actual = Matchers.QuickSearchMatcher(source, valueToMatch);

        // Assert
        Assert.Empty(actual);
    }

    // Test case 5: Match at the very beginning
    [Fact]
    public void QuickSearchMatcher_ShouldFindMatchAtStart()
    {
        // Arrange
        string source = "PrefixText";
        string valueToMatch = "Prefix";

        // Expected indices: (0, 5)
        int[][] expected = [[0, 5]];

        // Act
        int[][] actual = Matchers.QuickSearchMatcher(source, valueToMatch);

        // Assert
        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected[0], actual[0]);
    }

    // Test case 6: Match at the very end
    [Fact]
    public void QuickSearchMatcher_ShouldFindMatchAtEnd()
    {
        // Arrange
        string source = "TextSuffix";
        string valueToMatch = "Suffix";

        // Expected indices: (4, 9)
        int[][] expected = [[4, 9]];

        // Act
        int[][] actual = Matchers.QuickSearchMatcher(source, valueToMatch);

        // Assert
        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected[0], actual[0]);
    }

    // Test case 7: Empty source string
    [Fact]
    public void QuickSearchMatcher_ShouldReturnEmptyArray_WhenSourceIsEmpty()
    {
        // Arrange
        string source = "";
        string valueToMatch = "Pattern";

        // Act
        int[][] actual = Matchers.QuickSearchMatcher(source, valueToMatch);

        // Assert
        Assert.Empty(actual);
    }

    // Test case 8: Empty pattern string
    [Fact]
    public void QuickSearchMatcher_ShouldReturnEmptyArray_WhenPatternIsEmpty()
    {
        // Arrange
        string source = "Source";
        string valueToMatch = "";

        // Act
        int[][] actual = Matchers.QuickSearchMatcher(source, valueToMatch);

        // Assert
        Assert.Empty(actual);
    }
}
