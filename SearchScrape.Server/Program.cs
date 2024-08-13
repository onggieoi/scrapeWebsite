var builder = WebApplication.CreateBuilder(args);

// Configure Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("RedisConnection").Get<string>();
    options.InstanceName = "SearchScrape:";
});

// settings
var searchEngines = builder.Configuration.GetSection("SearchEngines").Get<List<SearchEngine>>();

// Add DI
if (searchEngines != null)
{
    builder.Services.AddSingleton(searchEngines);
}

builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Services.AddScoped<IScrapeWebService, ScrapeWebService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseDefaultFiles();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
