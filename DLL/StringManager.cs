using System.Text;
using StringManager.Interfaces;
using StringManager.Implementation;

namespace StringManager;

public class MyStringManager : IStringMatcher, IStringReplacer
{
    private readonly string sourceText;
    private readonly StringBuilder buffer;
    private readonly IRangeValidator validator;

    public MyStringManager(string source)
        : this(source, new RangeValidator())
    {
    }

    public MyStringManager(string source, IRangeValidator validator)
    {
        this.sourceText = source ?? throw new ArgumentNullException(nameof(source));
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this.buffer = new StringBuilder(source.Length);
    }

    public int[][] GetMatchingIndices(string valueToMatch, Func<string, string, int[][]> customMatcher)
    {
        ArgumentNullException.ThrowIfNull(valueToMatch);
        ArgumentNullException.ThrowIfNull(customMatcher);

        return customMatcher(this.sourceText, valueToMatch);
    }

    public string Replace(int startIndex, int endIndex, string newValue)
    {
        ArgumentNullException.ThrowIfNull(newValue);

        validator.ValidateRange(startIndex, endIndex, sourceText.Length);

        buffer.Clear();
        buffer.Append(sourceText, 0, startIndex)
              .Append(newValue)
              .Append(sourceText, endIndex + 1, sourceText.Length - endIndex - 1);

        return buffer.ToString();
    }

    public string ReplaceMany(int[][] ranges, string newValue)
    {
        ArgumentNullException.ThrowIfNull(newValue);
        if (ranges == null || ranges.Length == 0) return sourceText;

        // Sort ranges for sequential processing
        Array.Sort(ranges, (a, b) => a[0].CompareTo(b[0]));
        validator.ValidateRanges(ranges, sourceText.Length);

        buffer.Clear();
        int lastEnd = 0;

        foreach (var range in ranges)
        {
            int start = range[0];
            int end = range[1];

            // Append text between last range and current range
            buffer.Append(sourceText, lastEnd, start - lastEnd)
                  .Append(newValue);

            lastEnd = end + 1;
        }

        // Append remaining text after last range
        if (lastEnd < sourceText.Length)
        {
            buffer.Append(sourceText, lastEnd, sourceText.Length - lastEnd);
        }

        return buffer.ToString();
    }
}