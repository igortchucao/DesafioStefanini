using Questao5.Application.Commands;
using Questao5.Application.Queries;
using Questao5.Domain.DTO;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Handlers
{
    public class ObtemSaldoHandler : IHandlerAsync<string, SaldoReturn>
    {
        public IObterSaldoConta _command;

        public ObtemSaldoHandler(IObterSaldoConta command)
        {
            _command = command;
        }

        public async Task<SaldoReturn> ExecuteAsync(string request)
        {
            return _command.ObterSaldo(request);
        }
    }
}
