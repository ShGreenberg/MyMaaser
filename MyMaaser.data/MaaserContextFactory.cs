using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyMaaser.data
{
    public class MaaserContextFactory : IDesignTimeDbContextFactory<MaaserContext>
    {
        public MaaserContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}MyMaaser.web"))
                 .AddJsonFile("appsettings.json")
                 .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();
            return new MaaserContext(config.GetConnectionString("ConStr"));
        }
    }
}
