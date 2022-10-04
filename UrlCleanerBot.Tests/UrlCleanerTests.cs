using UrlCleanerBot.App;

namespace UrlCleanerBot.Tests;

public class UrlCleanerTests
{
    [Theory]
    [InlineData("https://www.youtube.com/watch?v=D9SFvNpBTBM", "https://www.youtube.com/watch?v=D9SFvNpBTBM")]
    [InlineData("https://twitter.com/sinicynr/status/1577366959183646720?s=20&t=pjVwl0yXaXGTosa1KDVjDQ", "https://twitter.com/sinicynr/status/1577366959183646720")]
    public void CelanUrl_CleansUrl(string input, string expected)
    {
        var result = UrlCleaner.CleanUrl(input);
        Assert.Equal(result, expected);
    }
}