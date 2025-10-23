namespace StringManager.Interfaces;

public interface IStringReplacer
{
    string Replace(int startIndex, int endIndex, string newValue);
    string ReplaceMany(int[][] ranges, string newValue);
}