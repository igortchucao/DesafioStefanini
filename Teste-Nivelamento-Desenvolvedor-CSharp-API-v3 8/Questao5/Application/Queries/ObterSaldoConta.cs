using Questao5.Domain.DTO;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;

namespace Questao5.Application.Queries;

public class ObterSaldoConta : IObterSaldoConta
{
    public IObterSaldoRepository _repoSaldo;
    public IContaCorrenteRepository _repoCC;

    public ObterSaldoConta(IObterSaldoRepository repoSaldo, IContaCorrenteRepository repoCC)
    {
        _repoSaldo = repoSaldo;
        _repoCC = repoCC;
    }

    public SaldoReturn ObterSaldo(string idContaCorrente)
    {
        var contaCorrente = _repoCC.ObtemContaCorrente(idContaCorrente);

        if (contaCorrente is null)
            throw new Exception("INVALID_ACCOUNT - Conta Corrente não existe");
        else if (contaCorrente.ativo == 0)
            throw new Exception("INACTIVE_ACCOUNT - Conta Corrente Inativa");

        var saldo = _repoSaldo.ObtemSaldoConta(idContaCorrente);
        var saldoreturn = new SaldoReturn(contaCorrente.numero, contaCorrente.nome, DateTime.Now, "R$" + saldo);

        return saldoreturn;

    }
}
