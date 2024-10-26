
namespace Questao5.Infrastructure.Database.QueryStore
{
    public interface IObterSaldoRepository
    {
        int ObtemSaldoConta(string idContaCorrente);
    }
}