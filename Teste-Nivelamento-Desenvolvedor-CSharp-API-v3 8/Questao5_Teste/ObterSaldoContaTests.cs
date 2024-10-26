using System;
using NSubstitute;
using Questao5.Application.Queries;
using Questao5.Domain.DTO;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore;
using Questao5.Infrastructure.Database.QueryStore;
using Xunit;

namespace Questao5.Tests.Application.Queries
{
    public class ObterSaldoContaTests
    {
        private readonly IObterSaldoRepository _saldoRepoMock;
        private readonly IContaCorrenteRepository _contaCorrenteRepoMock;
        private readonly ObterSaldoConta _obterSaldoConta;

        public ObterSaldoContaTests()
        {
            _saldoRepoMock = Substitute.For<IObterSaldoRepository>();
            _contaCorrenteRepoMock = Substitute.For<IContaCorrenteRepository>();
            _obterSaldoConta = new ObterSaldoConta(_saldoRepoMock, _contaCorrenteRepoMock);
        }

        [Fact]
        public void ObterSaldo_ShouldReturnSaldoReturn_WhenContaCorrenteIsValidAndActive()
        {
            // Arrange
            var idContaCorrente = "123";
            var contaCorrente = new ContaCorrente
            {
                idContaCorrente = idContaCorrente,
                numero = 12345,
                nome = "Conta Teste",
                ativo = 1
            };

            _contaCorrenteRepoMock.ObtemContaCorrente(idContaCorrente).Returns(contaCorrente);
            _saldoRepoMock.ObtemSaldoConta(idContaCorrente).Returns(1000);

            // Act
            var result = _obterSaldoConta.ObterSaldo(idContaCorrente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(12345, result.NumeroContaCorrente);
            Assert.Equal("Conta Teste", result.NomeTitular);
            Assert.StartsWith("R$", result.ValorSaldoAtual);
            Assert.Equal("R$1000", result.ValorSaldoAtual);
        }

        [Fact]
        public void ObterSaldo_ShouldThrowException_WhenContaCorrenteDoesNotExist()
        {
            // Arrange
            var idContaCorrente = "123";
            _contaCorrenteRepoMock.ObtemContaCorrente(idContaCorrente).Returns((ContaCorrente)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _obterSaldoConta.ObterSaldo(idContaCorrente));
            Assert.Equal("INVALID_ACCOUNT - Conta Corrente não existe", exception.Message);
            _saldoRepoMock.DidNotReceive().ObtemSaldoConta(idContaCorrente);
        }

        [Fact]
        public void ObterSaldo_ShouldThrowException_WhenContaCorrenteIsInactive()
        {
            // Arrange
            var idContaCorrente = "123";
            var contaCorrente = new ContaCorrente
            {
                idContaCorrente = idContaCorrente,
                numero = 12345,
                nome = "Conta Teste",
                ativo = 0 // Conta inativa
            };

            _contaCorrenteRepoMock.ObtemContaCorrente(idContaCorrente).Returns(contaCorrente);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _obterSaldoConta.ObterSaldo(idContaCorrente));
            Assert.Equal("INACTIVE_ACCOUNT - Conta Corrente Inativa", exception.Message);
            _saldoRepoMock.DidNotReceive().ObtemSaldoConta(idContaCorrente);
        }
    }
}
