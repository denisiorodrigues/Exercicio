using System;
using System.Globalization;

namespace Questao1.Model;

public class ContaBancaria
{
    private const double TAXA_POR_SAQUE = 3.5;

    public ContaBancaria(int numero, string nomeTitular)
    {
        Numero = numero;
        NomeTitular = nomeTitular;
        Saldo = 0;
    }

    public ContaBancaria(int numero, string nomeTitular, double saldo)
    {
        Numero = numero;
        NomeTitular = nomeTitular;
        Saldo = saldo;
    }

    public int Numero { get; }

    public string NomeTitular { get; set; }

    public double Saldo { get; set; }

    public void Deposito(double quantia)
    {
        if(quantia <= 0)
        {
            throw new InvalidOperationException("Saldo de depósito inválido");
        }

        this.Saldo += quantia;
    }

    public void Saque(double quantia)
    {
        var valorSaque = this.Saldo - quantia - TAXA_POR_SAQUE;

        this.Saldo = valorSaque;
    }

    public override string ToString()
    {
        return $"Conta {this.Numero}, Titular: {this.NomeTitular}, Saldo: {this.Saldo.ToString("C2", new CultureInfo("en-US"))}";
    }
}
