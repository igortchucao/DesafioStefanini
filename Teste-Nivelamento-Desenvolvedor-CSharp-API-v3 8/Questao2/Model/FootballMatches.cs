namespace Questao2.Model;

public class FootballMatchesData
{
    public List<FootballMatches> data { get; set; }
    public int total_pages { get; set; }

    public FootballMatchesData()
    {
        data = new List<FootballMatches>();
    }
}

public class FootballMatches
{
    public string team1 { get; set; }
    public string team2 { get; set; }
    public string team1goals { get; set; }
    public string team2goals { get; set; }       
}
