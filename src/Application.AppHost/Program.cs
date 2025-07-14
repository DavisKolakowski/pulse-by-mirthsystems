internal class Program
{
    private static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var postgres =
            builder.AddAzurePostgresFlexibleServer("postgres")
                      .RunAsContainer(container =>
                      {
                          container.WithImage("postgis/postgis");
                          container.WithDataVolume(
                              name: "postgres-data",
                              isReadOnly: false
                          )
                          .WithPgWeb();
                      });

        var db = postgres.AddDatabase("application-db");

        var databaseMigrations =
            builder.AddProject<Projects.Application_Services_DatabaseMigrations>("database-migrations")
                .WithReference(db)
                .WaitFor(db);

        var cache = builder.AddRedis("cache");

        builder.Build().Run();
    }
}