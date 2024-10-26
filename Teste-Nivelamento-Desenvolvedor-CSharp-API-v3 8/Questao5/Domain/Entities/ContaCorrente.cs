using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    public string idContaCorrente { get; set; }
    public int numero { get; set; }
    public string nome { get; set; }
    public int ativo { get; set; }


    public ContaCorrente() 
    { 
    }
}
