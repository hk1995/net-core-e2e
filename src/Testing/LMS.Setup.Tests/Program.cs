using System;
using System.Configuration;

namespace LMS.Setup.Tests
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args == null)
            {
                Console.WriteLine(@"Nothing happened. You didn't enter any args.""");
                return 0;
            }

            var mainArg = args[0];
            switch (mainArg)
            {
                case "/UpdateTestDb":
                    if (args.Length > 1 && args[1] == "-local")
                        return UpdateTestDb(true);
                    return UpdateTestDb();
                case "/RunMigrations":
                    return args.Length > 1
                        ? RunMigrationsAndSeed(args[1])
                        : 0;
                default:
                    return 0;
            }
        }

        private static int UpdateTestDb(bool local = false)
        {
            //Hard code this so that we don't accidently pass in a production string
            var env = local ? "Local" : "Default";
            var testConnectionString = ConfigurationManager.ConnectionStrings[env].ConnectionString;
            Console.WriteLine("Deleting all tables from database...");

            var databaseRestorer = new DatabaseSetup(testConnectionString);
            try
            {
                databaseRestorer.Drop_Migrate_And_Reseed_Database();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Failed to wipe and restore database");
                Console.WriteLine(exception.ToString());
                return 1;
            }

            Console.WriteLine("Restoring database complete");
            return 0;
        }

        private static int RunMigrationsAndSeed(string connectionString)
        {
            var databaseRestorer = new DatabaseSetup(connectionString);
            try
            {
                databaseRestorer.MigrateAndSeedDatabase();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Failed to run migrations and seed database.");
                Console.WriteLine(exception.ToString());
                return 1;
            }

            Console.WriteLine("Migrations and Seeding complete.");
            return 0;
        }
    }
}
