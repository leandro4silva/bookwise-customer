using System.Diagnostics.CodeAnalysis;

namespace BookWise.Customer.Infrastructure.LogAudit.Dtos;

[ExcludeFromCodeCoverage]
public sealed class AuditoriaConfig
{
    public bool Active { get; set; }

    public string? QueueUrl { get; set; }
}
