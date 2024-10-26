
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public interface IContaCorrenteRepository
    {
        ContaCorrente? ObtemContaCorrente(string idContaCorrente);
    }
}