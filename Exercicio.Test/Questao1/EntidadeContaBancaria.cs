using Questao1.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio.Test.Questao1
{
    public class EntidadeContaBancaria
    {
        [Fact]
        public void ValorZeroQuandoOValorDeDepositoInicialEOpcional()
        {
            /// Arrange
            double valorEsperado = 0;
            /// Act
            ContaBancaria conta = new ContaBancaria(5447, "Milton Gonçalves");

            /// Assert
            Assert.Equal(valorEsperado, conta.Saldo);
        }

        [Fact]
        public void ValorDaContaEsperadoIgualAoValorQuandoCriado()
        {
            /// Arrange
            double valorEsperado = 350.00;
            /// Act
            ContaBancaria conta = new ContaBancaria(5447, "Milton Gonçalves", 350.00);
            /// Assert
            Assert.Equal(valorEsperado, conta.Saldo);
        }

        [Fact]
        public void ValidarSaldoFinalDepoisDeTresSaquesAplicandoATaxaTresECinquenta()
        {
            /// Arrange
            double saldoInicial= 350.00;
            double valorEsperado = 305.00;
            ContaBancaria conta = new ContaBancaria(5447, "Milton Gonçalves", saldoInicial);
            
            /// Act
            conta.Saque(7.0);
            conta.Saque(7.0);
            conta.Saque(7.0);
            conta.Saque(10.0);

            /// Assert
            Assert.Equal(valorEsperado, conta.Saldo);
        }

        [Fact]
        public void ContaComSaldoNegativoAposMaisDeUmSaque()
        {
            /// Arrange
            double saldoInicial = 100;
            double valorEsperado = -7;
            ContaBancaria conta = new ContaBancaria(5447, "Milton Gonçalves", saldoInicial);

            /// Act
            conta.Saque(50.0);
            conta.Saque(50.0);

            /// Assert
            Assert.Equal(valorEsperado, conta.Saldo);
        }

        [Fact]
        public void FormatoRetornoDaconta()
        {
            /// Arrange
            double saldoInicial = 100;
            ContaBancaria conta = new ContaBancaria(5447, "Milton Gonçalves", saldoInicial);
            string valorEsperadoDadosConta = "Conta 5447, Titular: Milton Gonçalves, Saldo: $100.00";

            /// Act
            string dadosConta = conta.ToString();
            /// Assert
            Assert.Equal(valorEsperadoDadosConta, dadosConta);
        }
    }
}
