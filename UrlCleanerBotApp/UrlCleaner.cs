using System;
using System.Text.RegularExpressions;
using System.Web;

namespace UrlCleanerBot.App;

public static class UrlCleaner
{
    private static readonly string regexString = @"(http|https)\:\/\/[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(\/\S*)?";

    private static readonly Dictionary<string, string[]> allowedParamsSettings
        = new()
        {
            {"youtube", new []{"v"} }
        };

    public static string[] ExtractUrls(string message)
    {
       var matches = Regex.Matches(message, regexString);
        return matches.Select(m=>m.Value).ToArray();
    }

    public static string CleanUrl(string stringUrl)
    {
        if (!Uri.TryCreate(stringUrl, UriKind.Absolute, out var uri))
        {
            return string.Empty;
        }
        var cleanStringUrl = stringUrl[..stringUrl.IndexOf('?')];
        var allowedParams = allowedParamsSettings.FirstOrDefault(p => uri.Host.Contains(p.Key)).Value;
        if (allowedParams is not null)
        {
            var queryParams = HttpUtility.ParseQueryString(uri.Query);
            return $"{cleanStringUrl}?{string.Join("&", allowedParams.Select(p => $"{p}={queryParams.Get(p)}"))}";
        }
        return cleanStringUrl;
    }
}

