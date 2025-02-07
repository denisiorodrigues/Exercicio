namespace Questao5.Domain.Entities;
/*
 chave_idempotencia TEXT(37) PRIMARY KEY, -- identificacao chave de idempotencia
	requisicao TEXT(1000), -- dados de requisicao
	resultado TEXT(1000) -- dados de retorno

 */
public class Idempotence : Entity
{
    public string Request { get; set; }
    public string Response { get; set; }
}
