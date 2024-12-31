using System.ComponentModel;

namespace BookWise.Customer.Infrastructure.LogAudit.Enums;

public enum AuditoriaOperacao
{
    [Description("consulta")]
    Consulta = 1,

    [Description("insercao")]
    Insercao = 2,

    [Description("atualizacao")]
    Atualizacao = 3,

    [Description("remocao")]
    Remocao = 4
}
