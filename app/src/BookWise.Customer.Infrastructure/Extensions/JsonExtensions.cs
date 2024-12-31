using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookWise.Customer.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class JsonExtensions
{
    public static IMvcBuilder AddCustomJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options => ConfigureJsonOptions(options.JsonSerializerOptions));
        return builder;
    }

    public static void ConfigureJsonOptions(JsonSerializerOptions options)
    {
        options.WriteIndented = false;
        options.PropertyNameCaseInsensitive = true;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.Converters.Add(new JsonStringEnumConverter());
    }
}
