internal class Program
{
    private static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var azureMapsSubscriptionKey = builder.AddParameter("azure-maps-subscription-key", secret: true);
        var postgresAdminUsername = builder.AddParameter("postgres-admin-username");
        var postgresAdminPassword = builder.AddParameter("postgres-admin-password", secret: true);

        var postgres =
            builder.AddAzurePostgresFlexibleServer("postgres")
                   .WithPasswordAuthentication(postgresAdminUsername, postgresAdminPassword)
                   .RunAsContainer(container =>
                   {
                       container.WithImage("postgis/postgis");
                       container.WithDataBindMount(
                           source: "../../data/postgres",
                           isReadOnly: false
                       ).WithPgWeb();                   
                   });

        var cache = builder.AddRedis("cache");

        var db = postgres.AddDatabase("application-db");

        var databaseMigrations =
            builder.AddProject<Projects.Application_Services_DatabaseMigrations>("database-migrations")
                   .WithReference(db)
                   .WaitFor(db);

        builder.Build().Run();
    }
}