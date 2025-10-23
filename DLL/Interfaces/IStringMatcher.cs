namespace StringManager.Interfaces;

public interface IStringMatcher
{
    int[][] GetMatchingIndices(string valueToMatch, Func<string, string, int[][]> customMatcher);
}