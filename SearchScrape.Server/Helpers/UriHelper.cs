public class UriHelper
{
    public static Uri BuildSearchUri(SearchEngine searchEngine, string keyword)
    {
        string searchKeyword = searchEngine.SearchKeywordQuery.Replace("SEARCH_KEYWORD", Uri.EscapeDataString(keyword));
        string searchURLRequest = $"{searchEngine.SearchURL}/{searchKeyword}";

        return new Uri(searchURLRequest);
    }
}