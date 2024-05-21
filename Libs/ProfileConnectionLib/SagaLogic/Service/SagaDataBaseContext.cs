using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace ProfileConnectionLib.SagaLogic.Service;

public class SagaDataBaseContext : SagaDbContext
{
    public SagaDataBaseContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override IEnumerable<ISagaClassMap> Configurations { get; }
}