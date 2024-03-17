using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCore.HttpLogic.Services.Connection.Data;

public record struct HttpConnectionData()
{
    public TimeSpan? Timeout { get; set; } = null;

    public CancellationToken CancellationToken { get; set; } = default;

    public string ClientName { get; set; }
}