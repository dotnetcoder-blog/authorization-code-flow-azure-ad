using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dnc.Services.Employee.DesignTime
{
    //IDesignTimeDbContextFactory is used by the EF
    //Core CLI tooling to create an instance of the derived DbContext class
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Dnc-EmployeeDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connectionString);
            Console.WriteLine(connectionString);
            return new AppDbContext(optionsBuilder.Options);
        }
    }


    //1- dotnet ef migrations add Initial -o Migrations -c Dnc.Services.Employee.AppDbContext

    //2- To confirm that the migration was created and is waiting to be applied, execute the list command
    //dotnet ef migrations list -c Dnc.Services.Employee.AppDbContext


    //3-The easiest method of applying the migration to the database is to drop the database and re-create it. If that
    //is an option, you can enter the following commands and move on to the next section:

    //dotnet ef database drop -f dotnet ef database update Initial -c Dnc.Services.Employee.AppDbContext
}
