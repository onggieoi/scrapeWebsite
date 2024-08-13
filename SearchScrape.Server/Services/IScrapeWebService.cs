public interface IScrapeWebService
{
    Task<int> GetTotalURLOccurenceByKeyword(SearchInput input);

    IEnumerable<SearchEngine> GetAllSearchEngine();
}