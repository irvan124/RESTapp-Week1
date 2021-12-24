using RESTapp.Data;
using RESTapp.Models;
using System;
using System.Linq;

namespace RESTapp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

           if(context.Authors.Any())
            {
                return;
            }

            // Seeding the DATA
            var authors = new Author[]
         {
                new Author{FirstName="Morad",LastName="Hansen",DateOfBirth=DateTime.Parse("1990-12-12"), MainCategory="Physics"},
                new Author{FirstName="Walang",LastName="Jamir",DateOfBirth=DateTime.Parse("1997-10-12"), MainCategory="Data Science"},
                new Author{FirstName="Peter",LastName="Parker",DateOfBirth=DateTime.Parse("1999-12-12"), MainCategory="Sci Fi"},
                new Author{FirstName="Tony",LastName="Stark",DateOfBirth=DateTime.Parse("1998-12-12"), MainCategory="Informatics"},
                new Author{FirstName="Bruce",LastName="Banner",DateOfBirth=DateTime.Parse("2000-12-12"), MainCategory="Action"},
         };

            foreach (var a in authors)
            {
                context.Authors.Add(a);
            }
            context.SaveChanges();

            var courses = new Course[]
              {
                new Course{Title="R Fundamentals",Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dignissim vestibulum sapien at accumsan. In vel lacus et elit dapibus mollis. Nullam pharetra nibh purus, vel accumsan turpis interdum pharetra.", AuthorID=1},
                new Course{Title="Microservices Architecture",Description="Microservices is self contained servers", AuthorID=3},
                new Course{Title="Frontend Programming",Description="Making user stunned with FrontEnd Course",AuthorID=1}, 
                new Course{Title="Backend RESTful API",Description="RESTFUL course", AuthorID=1},
                new Course{Title="Entity Frmework Core",Description="As Know As EF", AuthorID=2},
                new Course{Title="Ninja Guidence",Description="Haw tu be e ninja", AuthorID=5},
              };

            foreach (var c in courses)
            {
                context.Courses.Add(c);
            }

            context.SaveChanges();

        }
    }
}