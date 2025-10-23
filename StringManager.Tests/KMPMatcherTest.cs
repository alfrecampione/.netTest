namespace StringManager.Tests;

public class KMPMatcherTests
{
    // Test case 1: Basic single match
    [Fact]
    public void KMPMatcher_ShouldFindSingleMatch()
    {
        // Arrange
        string source = "ABABDABACDABABCABAB";
        string valueToMatch = "ABABCABAB";

        // Expected indices:
        // Source indices are 0-based. The match starts at index 10 and ends at index 18 (inclusive).
        int[][] expected = [[10, 18]];

        // Act
        int[][] actual = Matchers.KMPMatcher(source, valueToMatch);

        // Assert
        Assert.Equal(expected.Length, actual.Length);
        Assert.Equal(expected[0], actual[0]);
    }

    // Test case 2: Multiple non-overlapping matches
    [Fact]
    public void KMPMatcher_ShouldFindMultipleMatches()
    {
        // Arrange
        string source = "ABCDEABCDEABCDE";
        string valueToMatch = "CDE";

        // Expected indices: (2, 4), (7, 9), (12, 14)
        int[][] expected =
        [
            [2, 4],
            [7, 9],
            [12, 14]
        ];

        // Act
        int[][] actual = Matchers.KMPMatcher(source, valueToMatch);

        // Assert
        Assert.Equal(expected.Length, actual.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], actual[i]);
        }
    }

    // Test case 3: Overlapping matches (KMP handles this by design)
    [Fact]
    public void KMPMatcher_ShouldFindOverlappingMatches()
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
        int[][] actual = Matchers.KMPMatcher(source, valueToMatch);

        // Assert
        Assert.Equal(expected.Length, actual.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], actual[i]);
        }
    }

    // Test case 4: No match found
    [Fact]
    public void KMPMatcher_ShouldReturnEmptyArray_WhenNoMatch()
    {
        // Arrange
        string source = "Hello World";
        string valueToMatch = "Goodbye";

        // Act
        int[][] actual = Matchers.KMPMatcher(source, valueToMatch);

        // Assert
        Assert.Empty(actual);
    }
}