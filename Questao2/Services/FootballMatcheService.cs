using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Questao2.Model;

namespace Questao2.Services
{
    public class FootballMatcheService
    {
        private readonly string footballMatcheApi;

        public FootballMatcheService()
        {
            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //IConfiguration config = builder.Build();
            footballMatcheApi = "https://jsonmock.hackerrank.com/api/football_matches";//config["FootballMatcheApi"];
        }

        public async Task<int> getTotalScoredGoals(string team, int year)
        {
            int totalGoals = 0;
            var urlParamTeam1 = $"team1={team}&year={year}";
            var urlParamTeam2 = $"team2={team}&year={year}";

            totalGoals += await GetAllMatchesAllPages(urlParamTeam1, x => x.Team1Goals);
            totalGoals += await GetAllMatchesAllPages(urlParamTeam2, x => x.Team2Goals);

            return totalGoals;
        }

        public async Task<int> GetAllMatchesAllPages(string urlParam, Func<FootballMatche, int> sum)
        {   
            int totalGoals = 0;
            var tamData = await RequestFuttbollMatchesApi(urlParam);
            totalGoals += tamData.Data.Sum(sum);

            if(tamData.TotalPages == 1)
            {
                return totalGoals;
            }

            for (var page=2; page <= tamData.TotalPages; page++)
            {   
                tamData = await RequestFuttbollMatchesApi($"{urlParam}&page={page}");
                totalGoals += tamData.Data.Sum(sum);
            }

            return totalGoals;
        }

        public async Task<FootballMatcheResponse> RequestFuttbollMatchesApi(string urlParam)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{footballMatcheApi}?{urlParam}");
            var responseString = await response.Content.ReadAsStringAsync();
            var footballMatcheResponse = JsonConvert.DeserializeObject<FootballMatcheResponse>(responseString);
            return footballMatcheResponse;
        }
    }
}
