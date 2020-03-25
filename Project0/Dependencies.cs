using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Project0.DataAccess;
using Project0.Library.Interfaces;
using Project0.Library.Model;

namespace Project0.ConsoleUI
{
    public class Dependencies : IDesignTimeDbContextFactory<Project0Context>
    {
        public Project0Context CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Project0Context>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);

            return new Project0Context(optionsBuilder.Options);
        }

        public IProject0Repository CreateProject0Repository()
        {
            var dbContext = CreateDbContext();
            return new Project0Repository(dbContext);
        }
    }
}
