namespace Questao5.Domain.DTO;

public class SaldoReturn
{
    public int NumeroContaCorrente { get; set; }
    public string NomeTitular { get; set; }
    public DateTime DataConsulta { get; set; }
    public string ValorSaldoAtual { get; set; }

    public SaldoReturn(int numeroContaCorrente, string nomeTitular, DateTime dataConsulta, string valorSaldoAtual)
    {
        NumeroContaCorrente = numeroContaCorrente;
        NomeTitular = nomeTitular;
        DataConsulta = dataConsulta;
        ValorSaldoAtual = valorSaldoAtual;
    }
}
