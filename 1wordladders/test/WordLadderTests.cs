using Xunit;

namespace _1wordLadders.Tests;

public class WordLadderTests
{
    [Fact]
    public void InsertWord_AddsWordToGraph()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("apple");

        Assert.Contains("apple", wordLadder.WordGraph.Keys);
    }

    [Fact]
    public void InsertWord_CreatesConnections()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("apple");
        wordLadder.InsertWord("appel");

        // "apple" and "appel" have the same last 4 letters (p,p,l,e)
        Assert.Contains("appel", wordLadder.WordGraph["apple"]);
        Assert.Contains("apple", wordLadder.WordGraph["appel"]);
    }

    [Fact]
    public void InsertWord_NoConnection_WhenLastFourLettersDontMatch()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("apple");
        wordLadder.InsertWord("zebra");

        // "apple" and "zebra" should not connect
        Assert.DoesNotContain("zebra", wordLadder.WordGraph["apple"]);
        Assert.DoesNotContain("apple", wordLadder.WordGraph["zebra"]);
    }

    [Fact]
    public void CanConnect_ReturnsTrue_WhenLastFourLettersAreSubset()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("abcde");
        wordLadder.InsertWord("bcdea");

        // Assuming "abcde" last 4: bcde, "bcdea" has b,c,d,e,a so yes
        Assert.Contains("bcdea", wordLadder.WordGraph["abcde"]);
    }

    [Fact]
    public void CanConnect_ReturnsFalse_WhenLetterMissing()
    {
        var wordLadder = new WordLadder();
        wordLadder.InsertWord("abcde");
        wordLadder.InsertWord("bcfga");

        // "abcde" last 4: bcde, "bcfga" has b,c,f,g,a - missing d,e
        Assert.DoesNotContain("bcfga", wordLadder.WordGraph["abcde"]);
    }
}