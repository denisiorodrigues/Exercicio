using Newtonsoft.Json;

namespace Questao2.Model;

public class FootballMatcheResponse
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    
    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }
    public ICollection<FootballMatche> Data { get; set; }
}
