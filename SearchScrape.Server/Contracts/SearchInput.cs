using System.Text.Json;

public class SearchInput
{
    public int SearchEngineId { get; set; }

    public string Keyword { get; set; }

    public string Url { get; set; }

    public string BuildCacheKey()
    {
        var jsonString = JsonSerializer.Serialize(this);
        return jsonString.ComputeSha256Hash();
    }
}