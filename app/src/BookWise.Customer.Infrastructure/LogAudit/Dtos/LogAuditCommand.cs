using BookWise.Customer.Infrastructure.LogAudit.Enums;
using System.Diagnostics.CodeAnalysis;

namespace BookWise.Customer.Infrastructure.LogAudit.Dtos;

[ExcludeFromCodeCoverage]
public sealed class LogAuditCommand
{
    public AuditoriaOperacao Operacao { get; set; }

    public string? Descricao { get; set; }

    public string? ValorMinimo { get; set; }

    public string? ValorNovo { get; set; }

    public LogAuditCommand(AuditoriaOperacao operacao, string? descricao, string valorMinimo = "", string valorNovo = "")
    {
        Operacao = operacao;
        Descricao = descricao;
        ValorMinimo = valorMinimo;
        ValorNovo = valorNovo;
    }
}
