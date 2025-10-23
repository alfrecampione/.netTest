namespace StringManager.Interfaces;

public interface IRangeValidator
{
    void ValidateRange(int startIndex, int endIndex, int textLength);
    void ValidateRanges(int[][] ranges, int textLength);
}