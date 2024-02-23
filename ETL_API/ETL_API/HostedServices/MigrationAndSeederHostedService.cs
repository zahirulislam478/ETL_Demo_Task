using ETL_API.Services;

namespace ETL_API.HostedServices
{
    public class MigrationAndSeederHostedService(IServiceProvider serviceProvider) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<MigrationAndSeederService>();
            await seeder.MigrateAsync();
            await seeder.SeedAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
