using Questao5.Domain.DTO;

namespace Questao5.Application.Queries
{
    public interface IObterSaldoConta
    {
        SaldoReturn ObterSaldo(string idContaCorrente);
    }
}