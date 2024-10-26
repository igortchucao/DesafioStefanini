using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Handlers;
using Questao5.Domain.DTO;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancoController : ControllerBase
    {
        private readonly ILogger<BancoController> _logger;
        private IHandlerAsync<MovimentacaoFinanceira, Guid> _handlerAsync;
        private IHandlerAsync<string, SaldoReturn> _saldohandlerAsync;

        public BancoController(ILogger<BancoController> logger, 
            IHandlerAsync<MovimentacaoFinanceira, Guid> handlerAsync,
            IHandlerAsync<string, SaldoReturn> saldoHandlerAsync)
        {
            _logger = logger;
            _handlerAsync = handlerAsync;
            _saldohandlerAsync = saldoHandlerAsync;
        }

        /// <summary>
        /// Cria uma nova Movimentação Financeira.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///        {
        ///          "idContaCorrente": "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
        ///          "valorMovimentacao": 100,
        ///          "dataMovimentacao": "2024-10-26T01:55:23.310Z",
        ///          "tipoMovimento": 67
        ///        }
        ///        
        /// </remarks>
        /// <param name="request">Os detalhes da movimentação.</param>
        /// <returns>Retorna o id da movimentação feita.</returns>
        /// <response code="200">Movimentação feita com sucesso.</response>
        [HttpPost(Name = "PostMovimentacaoCC")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] MovimentacaoFinanceira movimentacao)
        {
            try
            {

                return Ok(_handlerAsync.ExecuteAsync(movimentacao).Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Confere Saldo da Conta Corrente
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///      GET  /GetSaldoConta?B6BAFC09-6967-ED11-A567-055DFA4A16C9
        ///        
        /// </remarks>
        /// <param name="request">Id da Conta Corrente.</param>
        [HttpGet(Name = "GetSaldoConta")]
        public ActionResult Get([FromQuery] string idContaCorrente)
        {
            try
            {
                return Ok(_saldohandlerAsync.ExecuteAsync(idContaCorrente).Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}