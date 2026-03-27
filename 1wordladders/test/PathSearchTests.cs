using Xunit;

namespace _1wordLadders.Tests;

public class PathSearchTests
{
    [Fact]
    public void ShortestPath_ReturnsZero_WhenStartEqualsEnd()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("apple");

        int result = PathSearch.ShortestPath(wordLadder, "apple", "apple");

        Assert.Equal(0, result);
    }

    [Fact]
    public void ShortestPath_ReturnsDistance_WhenPathExists()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("apple");
        wordLadder.InsertWord("appel");

        int result = PathSearch.ShortestPath(wordLadder, "apple", "appel");

        Assert.Equal(1, result);
    }

    [Fact]
    public void ShortestPath_ReturnsNegativeOne_WhenNoPath()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("apple");
        wordLadder.InsertWord("zebra");

        int result = PathSearch.ShortestPath(wordLadder, "apple", "zebra");

        Assert.Equal(-1, result);
    }

    [Fact]
    public void ShortestPath_ReturnsCorrectDistance_ForLongerPath()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("there");
        wordLadder.InsertWord("where");
        wordLadder.InsertWord("input");
        wordLadder.InsertWord("putin");

        int result = PathSearch.ShortestPath(wordLadder, "there", "where");

        Assert.Equal(1, result);
    }
}