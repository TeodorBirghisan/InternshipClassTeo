using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InternshipClass.Tests
{
    public class StartupTests
    {
        [Fact]
        public void ShouldConvertURLToHerokuString()
        {
            //Assume
            string url = "postgres://ozqxazypxkyarj:2707177261a47a7151668aa89c8f1546b97fde4eff6eb558e4de4bb590e679a6@ec2-54-155-35-88.eu-west-1.compute.amazonaws.com:5432/d87t481o5497ko";

            //Act
            var herokuConnectionString = Startup.ConverDatabaseURLToHerokuString(url);

            //Assert
            Assert.Equal("Server=ec2-54-155-35-88.eu-west-1.compute.amazonaws.com;Port=5432;Database=d87t481o5497ko;User Id=ozqxazypxkyarj;Password=2707177261a47a7151668aa89c8f1546b97fde4eff6eb558e4de4bb590e679a6;Pooling=true;SSL Mode=Require;Trust Server Certificate=True;",herokuConnectionString);
        }

        [Fact]
        public void ShouldThrowExceptionOnCorruptURL()
        {
            //Assume
            string url = "Server = 127.0.0.1; Port = 5432; Database = internshipClass; User Id = internshipClass; Password = SBDk8LO3ys5uBB3xOQZr;";

            //Act and Assert
            var exception = Assert.Throws<FormatException>(() => Startup.ConverDatabaseURLToHerokuString(url));

            Assert.StartsWith("DatabaseURL cannot be converted! Check this ", exception.Message);
        }
    }
}
