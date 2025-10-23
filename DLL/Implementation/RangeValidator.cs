using StringManager.Interfaces;

namespace StringManager.Implementation;

public class RangeValidator : IRangeValidator
{
    public void ValidateRange(int startIndex, int endIndex, int textLength)
    {
        if (startIndex < 0 || endIndex < startIndex || endIndex >= textLength)
        {
            throw new ArgumentException($"Invalid range: start={startIndex}, end={endIndex}, length={textLength}");
        }
    }

    public void ValidateRanges(int[][] ranges, int textLength)
    {
        if (ranges == null || ranges.Length == 0)
            return;

        for (int i = 0; i < ranges.Length; i++)
        {
            ValidateRange(ranges[i][0], ranges[i][1], textLength);

            if (i > 0 && ranges[i][0] <= ranges[i - 1][1])
            {
                throw new ArgumentException("Ranges must not overlap");
            }
        }
    }
}