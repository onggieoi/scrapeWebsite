using Microsoft.AspNetCore.Mvc;

namespace SearchScrape.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScrapeWebController : ControllerBase
    {
        private readonly ILogger<ScrapeWebController> _logger;
        private readonly IScrapeWebService _scapeWebService;
        private readonly ICacheService _cacheService;

        public ScrapeWebController(ILogger<ScrapeWebController> logger,
            IScrapeWebService scapeWebService,
            ICacheService cacheService)
        {
            _logger = logger;
            _scapeWebService = scapeWebService;
            _cacheService = cacheService;
        }

        [HttpPost]
        [Route("url-occurence")]
        public async Task<ActionResult<int>> ScrapeNumberOfOccurenceInSearchResult([FromBody] SearchInput input)
        {
            _logger.LogInformation($"Received Get Number of URL occurence in search result request");

            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            var result = await _cacheService.GetOrAddAsync(
                input.BuildCacheKey(),
                () => _scapeWebService.GetTotalURLOccurenceByKeyword(input));

            _logger.LogInformation($"Finished Get Number of URL occurence in search result request");
            
            return Ok(result);
        }

        [HttpGet]
        [Route("search-engines")]
        public ActionResult<IEnumerable<SearchEngine>> GetSearchEngines()
        {
            _logger.LogInformation($"Received Get Search Engines request");

            var result = _scapeWebService.GetAllSearchEngine();

            _logger.LogInformation($"Finished Get Search Engines request");
            
            return Ok(result);
        }
    }
}
