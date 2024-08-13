
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

public class ScrapeWebService : IScrapeWebService
{
    private readonly IList<SearchEngine> _searchEngines;
    private readonly ILogger<ScrapeWebService> _logger;

    public ScrapeWebService(List<SearchEngine> searchEngines, ILogger<ScrapeWebService> logger)
    {
        _searchEngines = searchEngines;
        _logger = logger;
    }

    public async Task<int> GetTotalURLOccurenceByKeyword(SearchInput input)
    {
        _logger.LogInformation("Start scraping website in ScapeWebService");

        int result = 0;
        var searchEngine = _searchEngines.FirstOrDefault(s => s.Id == input.SearchEngineId);

        if (searchEngine == null)
        {
            throw new ArgumentException("");
        }

        using HttpClient client = new();

        string html = await client.GetStringAsync(UriHelper.BuildSearchUri(searchEngine, input.Keyword));

        var urlPattern = new Regex(@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})");

        var urlMatches = urlPattern.Matches(html);

        foreach (Match urlMatch in urlMatches)
        {
            if (urlMatch.Value.Contains(input.Url, StringComparison.OrdinalIgnoreCase))
            {
                result++;
            }
        }

        _logger.LogInformation($"Finished scraping website in ScapeWebService");

        return result;
    }

    public IEnumerable<SearchEngine> GetAllSearchEngine()
    {
        return _searchEngines;
    }
}