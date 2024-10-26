using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class MovimentacaoFinanceira
{
    public Guid IdRequisicao { get; }
    public string idContaCorrente { get; set; }
    public decimal ValorMovimentacao { get; set; }
    public DateTime DataMovimentacao { get; set; }
    public TipoMovimentacaoEnum TipoMovimento{ get; set; }
    public char TipoChar => (char)TipoMovimento;

    public MovimentacaoFinanceira() 
    { 
        IdRequisicao = Guid.NewGuid();
    }
}
