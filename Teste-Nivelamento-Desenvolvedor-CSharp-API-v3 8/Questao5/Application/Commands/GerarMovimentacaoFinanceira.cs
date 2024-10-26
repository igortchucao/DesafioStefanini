using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore;

namespace Questao5.Application.Commands
{
    public class GerarMovimentacaoFinanceira : IGerarMovimentacaoFinanceira
    {
        public IMovimentacaoFinanceiraRepository _repoMovim;
        public IContaCorrenteRepository _repoCC;

        public GerarMovimentacaoFinanceira(IMovimentacaoFinanceiraRepository repo, IContaCorrenteRepository repoCC)
        {
            _repoMovim = repo;
            _repoCC = repoCC;
        }

        public void GerarMovimentacao(MovimentacaoFinanceira movimentacao)
        {
            var contaCorrente = _repoCC.ObtemContaCorrente(movimentacao.idContaCorrente);

            if (contaCorrente is null)
                throw new Exception("INVALID_ACCOUNT - Conta Corrente não existe");
            else if (contaCorrente.ativo == 0)
                throw new Exception("INACTIVE_ACCOUNT - Conta Corrente Inativa");

            _repoMovim.InsertMovimentacaoFinanceira(movimentacao);

        }
    }
}
