using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuromasters.borders.Shared
{
    public record ApplicationConfig
    {
        public ConnectionStrings ConnectionStrings { get; init; }
        public string[] CorsOrigins { get; init; }
        public string BuildId { get; init; }
        public Logging ApplicationInsights { get; init; }
    }

    [ExcludeFromCodeCoverage]
    public record Logging
    {
        public string? ConnectionString { get; init; }
    }

    public record ConnectionStrings
    {
        public string DefaultConnection { get; init; }
    }
}
