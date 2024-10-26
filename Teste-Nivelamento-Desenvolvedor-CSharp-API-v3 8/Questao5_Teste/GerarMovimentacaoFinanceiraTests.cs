using NSubstitute;
using Questao5.Application.Commands;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore;
using System;
using Xunit;

namespace Questao5.Tests.Application.Commands
{
    public class GerarMovimentacaoFinanceiraTests
    {
        private readonly IMovimentacaoFinanceiraRepository _movimentacaoRepoMock;
        private readonly IContaCorrenteRepository _contaCorrenteRepoMock;
        private readonly GerarMovimentacaoFinanceira _gerarMovimentacao;

        public GerarMovimentacaoFinanceiraTests()
        {
            _movimentacaoRepoMock = Substitute.For<IMovimentacaoFinanceiraRepository>();
            _contaCorrenteRepoMock = Substitute.For<IContaCorrenteRepository>();
            _gerarMovimentacao = new GerarMovimentacaoFinanceira(_movimentacaoRepoMock, _contaCorrenteRepoMock);
        }

        [Fact]
        public void GerarMovimentacao_ShouldInsertMovimentacao_WhenContaCorrenteIsValidAndActive()
        {
            // Arrange
            var movimentacao = new MovimentacaoFinanceira
            {
                idContaCorrente = "123",
                TipoMovimento = TipoMovimentacaoEnum.Credito,
                ValorMovimentacao = 100.00M
            };

            var contaCorrente = new ContaCorrente
            {
                idContaCorrente = "123",
                ativo = 1
            };

            _contaCorrenteRepoMock.ObtemContaCorrente(movimentacao.idContaCorrente).Returns(contaCorrente);

            // Act
            _gerarMovimentacao.GerarMovimentacao(movimentacao);

            // Assert
            _movimentacaoRepoMock.Received(1).InsertMovimentacaoFinanceira(movimentacao);
        }

        [Fact]
        public void GerarMovimentacao_ShouldThrowException_WhenContaCorrenteDoesNotExist()
        {
            // Arrange
            var movimentacao = new MovimentacaoFinanceira
            {
                idContaCorrente = "123",
                TipoMovimento = TipoMovimentacaoEnum.Credito,
                ValorMovimentacao = 100.00M
            };

            _contaCorrenteRepoMock.ObtemContaCorrente(movimentacao.idContaCorrente).Returns((ContaCorrente)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _gerarMovimentacao.GerarMovimentacao(movimentacao));
            Assert.Equal("INVALID_ACCOUNT - Conta Corrente não existe", exception.Message);
            _movimentacaoRepoMock.DidNotReceive().InsertMovimentacaoFinanceira(movimentacao);
        }

        [Fact]
        public void GerarMovimentacao_ShouldThrowException_WhenContaCorrenteIsInactive()
        {
            // Arrange
            var movimentacao = new MovimentacaoFinanceira
            {
                idContaCorrente = "123",
                TipoMovimento = TipoMovimentacaoEnum.Credito,
                ValorMovimentacao = 100.00M
            };

            var contaCorrente = new ContaCorrente
            {
                idContaCorrente = "123",
                ativo = 0
            };

            _contaCorrenteRepoMock.ObtemContaCorrente(movimentacao.idContaCorrente).Returns(contaCorrente);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _gerarMovimentacao.GerarMovimentacao(movimentacao));
            Assert.Equal("INACTIVE_ACCOUNT - Conta Corrente Inativa", exception.Message);
            _movimentacaoRepoMock.DidNotReceive().InsertMovimentacaoFinanceira(movimentacao);
        }
    }
}
