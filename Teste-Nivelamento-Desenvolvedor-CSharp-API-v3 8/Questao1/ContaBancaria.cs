using System.Globalization;

namespace Questao1;

class ContaBancaria 
{
    public int Numero_Conta { get; }
    public Cliente Titular  { get; set; }
    private double Saldo { get; set; }

    public ContaBancaria(int numero_Conta, string nome_titular, double deposito_inicial = 0)
    {
        Numero_Conta = numero_Conta;
        Titular = new Cliente(nome_titular);
        Saldo = deposito_inicial;
    }

    public void Saque(double valor)
    {
        Saldo -= (valor + 3.5d); 
    }

    public void Deposito(double valor)
    {
        Saldo += valor;
    }

    public Cliente AlterarCliente(string nome)
    {
        Titular.Nome = nome;
        return Titular;
    }

    public override string ToString()
    {
        return $"Conta {Numero_Conta}, Titular: {Titular.Nome}, Saldo: $ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";
    }
}


