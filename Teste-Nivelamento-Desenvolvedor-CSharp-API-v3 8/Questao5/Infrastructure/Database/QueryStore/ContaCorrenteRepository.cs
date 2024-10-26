using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public ContaCorrente? ObtemContaCorrente(string idContaCorrente)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var contaCorrente = connection.Query<ContaCorrente>($"SELECT * " +
                                                                $"FROM contacorrente " +
                                                                $"WHERE idcontacorrente='{idContaCorrente}' ").FirstOrDefault();

            return contaCorrente;
        }
    }

}
