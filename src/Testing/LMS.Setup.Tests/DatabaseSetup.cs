using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using LMS.Data;
using LMS.Data.Migrations;

namespace LMS.Setup.Tests
{
    public class DatabaseSetup
    {
        private readonly string _connectionString;

        public DatabaseSetup(string connectionString)
        {
            //this ensures our db related tests can access the correct database. not dev/staging/prod/etc.
            _connectionString = connectionString;
        }

        public void Drop_Migrate_And_Reseed_Database()
        {
            Console.WriteLine("Connection String: " + _connectionString);

            //(1) drop all tables
            ReallyDropAllTables();

            //(2) Run migrator - this is same as update-database
            RunMigrations();

            //(3) Seed
            Seed();
        }

        public void MigrateAndSeedDatabase()
        {
            RunMigrations();
            Seed();
        }

        private void RunMigrations()
        {
            Console.WriteLine("Starting migrations...");
            var configuration = new Configuration
            {
                TargetDatabase = new DbConnectionInfo(_connectionString, "System.Data.SqlClient")
            };
            var dbMigrator = new DbMigrator(configuration);
            dbMigrator.Update();
            Console.WriteLine("Migrations completed");
        }

        private void Seed()
        {
            using (var LaeContext = new LmsContext(_connectionString))
            {
                Console.WriteLine("Seeding test data into database");
                DbInitialize.Seed(_connectionString);
                Console.WriteLine("Seeding test data is complete");
            }
        }

        /// <summary>
        /// Drops all FK contraints and tables in the database that is specified in the connection string.
        /// http://dba.stackexchange.com/questions/90033/how-do-i-drop-all-constraints-from-all-tables
        /// http://stackoverflow.com/questions/27606518/how-to-drop-all-tables-from-database-with-one-sql-query
        /// Note: sp_msforeachtable is not supported in azure.
        /// </summary>
        private void ReallyDropAllTables()
        {
            const string DropDatabaseSql = @"
                DECLARE @sql NVARCHAR(MAX);
                SET @sql = N'';
                SELECT @sql = @sql + N'
                  ALTER TABLE ' + QUOTENAME(s.name) + N'.'
                  + QUOTENAME(t.name) + N' DROP CONSTRAINT '
                  + QUOTENAME(c.name) + ';'
                FROM sys.objects AS c
                INNER JOIN sys.tables AS t
                ON c.parent_object_id = t.[object_id]
                INNER JOIN sys.schemas AS s 
                ON t.[schema_id] = s.[schema_id]
                WHERE c.[type] IN ('D','C','F','PK','UQ')
                ORDER BY c.[type];
                EXEC sys.sp_executesql @sql;
                DECLARE @sql2 NVARCHAR(max)=''
                SELECT @sql2 += ' Drop table [' + TABLE_SCHEMA + '].['+ TABLE_NAME + ']'
                FROM   INFORMATION_SCHEMA.TABLES
                WHERE  TABLE_TYPE = 'BASE TABLE'
                Exec Sp_executesql @sql2
                ";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var sqlToExecute = String.Format(DropDatabaseSql, connection.Database);

                    var command = new SqlCommand(sqlToExecute, connection);

                    Console.WriteLine("Dropping all FK contraints, then all tables...");
                    command.ExecuteNonQuery();
                    Console.WriteLine("All tables dropped");
                }
            }
            catch (SqlException sqlException)
            {
                if (sqlException.Message.StartsWith("Cannot open database"))
                {
                    Console.WriteLine("Database does not exist.");
                    return;
                }
                throw;
            }
        }
    }
}
