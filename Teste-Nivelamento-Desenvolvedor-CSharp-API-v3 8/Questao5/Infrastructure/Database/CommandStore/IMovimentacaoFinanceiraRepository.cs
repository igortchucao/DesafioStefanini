using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public interface IMovimentacaoFinanceiraRepository
    {
        void InsertMovimentacaoFinanceira(MovimentacaoFinanceira movimentacao);
    }
}