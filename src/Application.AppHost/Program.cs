internal class Program
{
    private static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var postgres =
            builder.AddPostgres("postgres")
                   .WithImage("postgis/postgis")
                   .WithDataBindMount(
                        source: @"..\..\data\postgresql",
                        isReadOnly: false
                    )
                    .WithPgWeb();

        var db = postgres.AddDatabase("application-db");

        var databaseMigrations =
            builder.AddProject<Projects.Application_Services_DatabaseMigrations>("database-migrations")
                .WithReference(db)
                .WaitFor(db);

        var cache = builder.AddRedis("cache");

        var apiService = builder.AddProject<Projects.Application_Services_WeatherApi>("apiservice");

        builder.AddProject<Projects.Application_Web>("webfrontend")
            .WithExternalHttpEndpoints()
            .WithReference(cache)
            .WaitFor(cache)
            .WithReference(apiService)
            .WaitFor(apiService);

        builder.Build().Run();
    }
}