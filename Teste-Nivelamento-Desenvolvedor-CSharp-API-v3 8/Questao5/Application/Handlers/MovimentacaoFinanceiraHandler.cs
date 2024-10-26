using Questao5.Application.Commands;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Handlers
{
    public class MovimentacaoFinanceiraHandler : IHandlerAsync<MovimentacaoFinanceira, Guid>
    {
        public IGerarMovimentacaoFinanceira _command;

        public MovimentacaoFinanceiraHandler(IGerarMovimentacaoFinanceira command)
        {
            _command = command;
        }

        public async Task<Guid> ExecuteAsync(MovimentacaoFinanceira request)
        {
            if (!Enum.IsDefined(typeof(TipoMovimentacaoEnum), request.TipoMovimento))
                throw new Exception("INVALID_TYPE - Tipo de estar entre D () ou C ()");

            if (request.ValorMovimentacao <= 0)
                throw new Exception("INVALID_VALUE - Somente valores positivos para movimentações");

            _command.GerarMovimentacao(request);

            return request.IdRequisicao;
        }
    }
}
