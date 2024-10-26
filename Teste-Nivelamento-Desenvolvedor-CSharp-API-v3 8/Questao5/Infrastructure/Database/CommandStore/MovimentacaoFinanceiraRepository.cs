using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentacaoFinanceiraRepository : IMovimentacaoFinanceiraRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentacaoFinanceiraRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void InsertMovimentacaoFinanceira(MovimentacaoFinanceira movimentacao)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var query = @$"INSERT INTO movimento (idmovimento,idcontacorrente,datamovimento,tipomovimento,valor) VALUES (" +
                            @$"'{movimentacao.IdRequisicao}', '{movimentacao.idContaCorrente}', '{movimentacao.DataMovimentacao}'" +
                          @$", '{movimentacao.TipoChar}', {movimentacao.ValorMovimentacao});";

            connection.Execute(query);
        }
    }

}
