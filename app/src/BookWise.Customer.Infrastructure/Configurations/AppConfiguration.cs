using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace BookWise.Customer.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed class AppConfiguration
{
    private const string EnviromentDev = "dev";
    private const string EnvironmentHom = "hom";

    public CreateCustomerSqsConfig? CreateCustomerSqsConfig { get; set; }

    public AuditoriaConfig? AuditoriaConfig { get; set; }

    public UserImageConfig? UserImage { get; set; }

    public string? Environment { get; set; }

<<<<<<< HEAD
    public bool IsDevelopment =>
        EnviromentDev.Equals(Environment, StringComparison.OrdinalIgnoreCase);

    public bool IsStaging =>
        EnvironmentHom.Equals(Environment, StringComparison.OrdinalIgnoreCase);
=======
    public bool IsDevelopment => 
        EnviromentDev.Equals(Environment, StringComparison.OrdinalIgnoreCase);

    public bool IsStaging => 
        EnvironmentHom.Equals(Environment,StringComparison.OrdinalIgnoreCase);
>>>>>>> cc8244b (feat: implements audit log)
}
