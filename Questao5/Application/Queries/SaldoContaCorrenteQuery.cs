﻿using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries
{
    public class SaldoContaCorrenteQuery : IRequest<SaldoContaCorrenteResponse>
    {
        public int NumeroConta { get; set; }
    }
}
