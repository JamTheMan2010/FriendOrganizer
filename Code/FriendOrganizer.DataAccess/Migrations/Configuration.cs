namespace FriendOrganizer.DataAccess.Migrations
{
    using FriendOrganizer.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDbContext context)
        {
            context.Friends.AddOrUpdate(
                f => f.FirstName,
                new Friend { FirstName = "Thomas", LastName = "Whitbread" },
                new Friend { FirstName = "Simon", LastName = "Jackson" },
                new Friend { FirstName = "Iain", LastName = "Telford" },
                new Friend { FirstName = "Richard", LastName = "Wilson" }
                );

            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                new ProgrammingLanguage { Name = "C" },
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "F#" },
                new ProgrammingLanguage { Name = "Swift" },
                new ProgrammingLanguage { Name = "Haskell" },
                new ProgrammingLanguage { Name = "C++" },
                new ProgrammingLanguage { Name = "Visual Basic" },
                new ProgrammingLanguage { Name = "Pascal" },
                new ProgrammingLanguage { Name = "Python" },
                new ProgrammingLanguage { Name = "JavaScript" },
                new ProgrammingLanguage { Name = "TypeScript" },
                new ProgrammingLanguage { Name = "Java" }
                );
        }
    }
}
