using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using Dapper;

namespace Questao5.Infrastructure.Database.QueryStore;

public class ObterSaldoRepository : IObterSaldoRepository
{
    private readonly DatabaseConfig databaseConfig;

    public ObterSaldoRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public int ObtemSaldoConta(string idContaCorrente)
    {
        using var connection = new SqliteConnection(databaseConfig.Name);

        var contaCorrente = connection.Query<int>($"SELECT SUM(valor) as Saldo " +
                                                  $"FROM movimento " +
                                                  $"WHERE idcontacorrente = '{idContaCorrente}' " +
                                                  $"GROUP BY idcontacorrente").FirstOrDefault();

        return contaCorrente;
    }
}
