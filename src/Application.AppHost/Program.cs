internal class Program
{
    private static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var azureMapsSubscriptionKey = builder.AddParameter("azure-maps-subscription-key", secret: true);
        var keycloakAdminUsername = builder.AddParameter("keycloak-admin-username");
        var keycloakAdminPassword = builder.AddParameter("keycloak-admin-password", secret: true);

        var keycloak = 
            builder.AddKeycloak("keycloak", 8080, keycloakAdminUsername, keycloakAdminPassword)
                   .WithDataVolume(name: "keycloak-data");

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

        var cache = builder.AddRedis("cache");

        var db = postgres.AddDatabase("application-db");

        var databaseMigrations =
            builder.AddProject<Projects.Application_Services_DatabaseMigrations>("database-migrations")
                   .WithReference(db)
                   .WaitFor(db);

        builder.Build().Run();
    }
}