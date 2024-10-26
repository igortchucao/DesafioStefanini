using Questao5.Domain.Entities;

namespace Questao5.Application.Commands
{
    public interface IGerarMovimentacaoFinanceira
    {
        void GerarMovimentacao(MovimentacaoFinanceira movimentacao);
    }
}