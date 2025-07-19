internal class Program
{
    private static void Main(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        var azureMapsSubscriptionKey = builder.AddParameter("azure-maps-subscription-key", secret: true);
        var keycloakAdminUsername = builder.AddParameter("keycloak-admin-username");
        var keycloakAdminPassword = builder.AddParameter("keycloak-admin-password", secret: true);
        var postgresAdminUsername = builder.AddParameter("postgres-admin-username");
        var postgresAdminPassword = builder.AddParameter("postgres-admin-password", secret: true);

        var keycloak = 
            builder.AddKeycloak("keycloak", 8080, keycloakAdminUsername, keycloakAdminPassword)
                   .WithDataBindMount(
                       source: "../../data/keycloak"
                   )
                   //.WithRealmImport(
                   //    "../../configuration/keycloak/realms/application-realm.json"
                   //)
                   .WithExternalHttpEndpoints();

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
            builder.AddProject<Projects.Application_Services_DatabaseInitializer>("database-initializer")
                   .WithReference(db)
                   .WaitFor(db);

        builder.Build().Run();
    }
}