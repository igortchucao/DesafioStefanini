using System;
using System.Threading.Tasks;
using NSubstitute;
using Questao5.Application.Commands;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Xunit;

namespace Questao5.Tests.Application.Handlers
{
    public class MovimentacaoFinanceiraHandlerTests
    {
        private readonly IGerarMovimentacaoFinanceira _gerarMovimentacaoMock;
        private readonly MovimentacaoFinanceiraHandler _handler;

        public MovimentacaoFinanceiraHandlerTests()
        {
            _gerarMovimentacaoMock = Substitute.For<IGerarMovimentacaoFinanceira>();
            _handler = new MovimentacaoFinanceiraHandler(_gerarMovimentacaoMock);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldGenerateMovimentacao_WhenRequestIsValid()
        {
            // Arrange
            var movimentacao = new MovimentacaoFinanceira
            {
                TipoMovimento = TipoMovimentacaoEnum.Credito, 
                ValorMovimentacao = 100.00M
            };

            // Act
            var result = await _handler.ExecuteAsync(movimentacao);

            // Assert
            Assert.Equal(movimentacao.IdRequisicao, result);
            _gerarMovimentacaoMock.Received(1).GerarMovimentacao(movimentacao);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenTipoMovimentoIsInvalid()
        {
            // Arrange
            var movimentacao = new MovimentacaoFinanceira
            {
                TipoMovimento = (TipoMovimentacaoEnum)999, // Tipo inválido
                ValorMovimentacao = 100.00M
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.ExecuteAsync(movimentacao));
            Assert.Equal("INVALID_TYPE - Tipo de estar entre D () ou C ()", exception.Message);
            _gerarMovimentacaoMock.DidNotReceive().GerarMovimentacao(movimentacao);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrowException_WhenValorMovimentacaoIsZeroOrNegative()
        {
            // Arrange
            var movimentacao = new MovimentacaoFinanceira
            {
                TipoMovimento = TipoMovimentacaoEnum.Debito,
                ValorMovimentacao = -100.00M // Valor inválido
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.ExecuteAsync(movimentacao));
            Assert.Equal("INVALID_VALUE - Somente valores positivos para movimentações", exception.Message);
            _gerarMovimentacaoMock.DidNotReceive().GerarMovimentacao(movimentacao);
        }
    }
}
