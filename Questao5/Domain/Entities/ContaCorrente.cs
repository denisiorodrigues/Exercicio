﻿namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    public Guid Id { get; private set; }
    public int Numero { get; set; }
    public string Titular { get; set; }
    public bool Ativo { get; set; }
}
