using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Extensions
{
    public static class NpgsqlOptionsBuilderExtensions
    {
        /// <summary>
        /// Configures the default settings for PostgreSQL options.
        /// </summary>
        /// <param name="options">The Npgsql options builder.</param>
        public static void ConfigureDefaults(this NpgsqlDbContextOptionsBuilder options)
        {
            options.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM");
            options.MigrationsHistoryTable("__EFMigrationsHistory", "migrations");
        }
    }
}
