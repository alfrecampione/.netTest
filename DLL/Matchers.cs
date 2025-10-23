namespace StringManager;

public static class Matchers
{
    // ------------------- KMP (Knuth–Morris–Pratt) -------------------
    /// <summary>
    /// Finds all occurrences of a substring within a source string using the KMP algorithm.
    /// </summary>
    /// <param name="source">The text to search within.</param>
    /// <param name="valueToMatch">The substring to search for.</param>
    /// <returns>
    /// An array of [startIndex, endIndex] pairs representing all matches.
    /// Returns an empty array if no matches are found.
    /// </returns>
    public static int[][] KMPMatcher(string source, string valueToMatch)
    {
        // Builds the longest proper prefix-suffix (LPS) table for pattern preprocessing.
        static int[] BuildLps(ReadOnlySpan<char> pattern)
        {
            int[] lps = new int[pattern.Length];
            int len = 0, i = 1;

            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[len])
                {
                    lps[i++] = ++len;
                }
                else if (len != 0)
                {
                    len = lps[len - 1];
                }
                else
                {
                    lps[i++] = 0;
                }
            }

            return lps;
        }

        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(valueToMatch))
            return [];

        ReadOnlySpan<char> text = source.AsSpan();
        ReadOnlySpan<char> pattern = valueToMatch.AsSpan();
        int[] lps = BuildLps(pattern);
        var matches = new List<int[]>(capacity: 8);

        int i = 0, j = 0;

        // Main KMP search loop
        while (i < text.Length)
        {
            if (text[i] == pattern[j])
            {
                i++; j++;

                // Full match found
                if (j == pattern.Length)
                {
                    matches.Add([i - j, i - 1]);
                    j = lps[j - 1]; // Continue searching
                }
            }
            else
            {
                j = (j != 0) ? lps[j - 1] : 0;
                if (j == 0) i++;
            }
        }

        return matches.Count == 0 ? [] : matches.ToArray();
    }

    // ------------------- Quick Search -------------------
    /// <summary>
    /// Finds all occurrences of a substring using the Quick Search algorithm.
    /// A simplified and efficient variant of Boyer–Moore.
    /// </summary>
    /// <param name="source">The text to search within.</param>
    /// <param name="valueToMatch">The substring to search for.</param>
    /// <returns>
    /// An array of [startIndex, endIndex] pairs for each match.
    /// Returns an empty array if no matches are found.
    /// </returns>
    public static int[][] QuickSearchMatcher(string source, string valueToMatch)
    {
        if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(valueToMatch))
            return [];

        ReadOnlySpan<char> text = source.AsSpan();
        ReadOnlySpan<char> pattern = valueToMatch.AsSpan();
        int m = pattern.Length;
        int n = text.Length;

        // Build shift table for ASCII characters
        const int ALPHABET_SIZE = 256;
        int[] shift = new int[ALPHABET_SIZE];
        for (int k = 0; k < ALPHABET_SIZE; k++)
            shift[k] = m + 1;

        for (int k = 0; k < m; k++)
        {
            char c = pattern[k];
            if (c < ALPHABET_SIZE)
                shift[c] = m - k;
        }

        var matches = new List<int[]>(capacity: 8);
        int pos = 0;

        // Main Quick Search loop
        while (pos <= n - m)
        {
            int j = 0;

            // Compare pattern to current window
            while (j < m && text[pos + j] == pattern[j])
                j++;

            if (j == m)
                matches.Add([pos, pos + m - 1]);

            // Advance search window
            if (pos + m >= n)
                break;

            char nextChar = text[pos + m];
            int jump = nextChar < ALPHABET_SIZE ? shift[nextChar] : m + 1;
            pos += jump;
        }

        return matches.Count == 0 ? [] : matches.ToArray();
    }
}
