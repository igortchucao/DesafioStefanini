using Questao2.Model;
using System.Text.Json;

namespace Questao2.Service;

public class FootballMatchesService
{
    public async Task<int> getTotalScoredGoals(string team, int year)
    {
        List<FootballMatches> data = await GetGolsPorAno(team, year);

        var team1 = data.Where(x => x.team1.Equals(team)).Sum(x => int.Parse(x.team1goals));
        var team2 = data.Where(x => x.team2.Equals(team)).Sum(x => int.Parse(x.team2goals));

        return team1 + team2;
    }

    public async Task<List<FootballMatches>> GetGolsPorAno(string team, int year)
    {
        List<FootballMatches> footballList = new List<FootballMatches>();
        FootballMatchesData football = new FootballMatchesData();

        int page = 1;
   
        using (var client = new HttpClient())
        {
            for (int teamNumber = 1; teamNumber <= 2; teamNumber++)
            {
                try
                {
                    do
                    {
                        string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team{teamNumber}={team}&page={page}";

                        HttpResponseMessage? response = await client.GetAsync(url);

                        response.EnsureSuccessStatusCode();

                        if (response.IsSuccessStatusCode)
                        {
                            var body = await response.Content.ReadAsStringAsync();
                            football = JsonSerializer.Deserialize<FootballMatchesData>(body);
                            footballList.AddRange(football.data);
                            page++;
                        }else
                        {
                            throw new Exception(response.EnsureSuccessStatusCode().ToString());
                        }
                    } while (page <= football.total_pages);

                    page = 1;
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Erro na chamada da API");
                }
            }
            
            return footballList;
        }
    }
}
