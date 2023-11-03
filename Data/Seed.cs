using Microsoft.AspNetCore.Identity;
using MoviesDatabase.Data;
using MoviesDataBaseApp.Models;
using System.Net;

namespace MoviesDataBaseApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {

                            Name = "Barbie",
                            Image = "https://www.barbie-themovie.com/images/gallery/img1.jpg",
                            Genre = new Genre()
                            {
                                GenreName = "Fantasy"
                            },
                            Director = new Director()
                            {
                                Name = "Greta GerWig",
                                Age = 40
                            },
                            Studios = new Studios()
                            {
                                StudioName = "Warner Bros",
                                CEO = "Michael De Luca and Pamela Abdy"
                            },

                            Language = "English",

                            Award = new Award()
                            {
                                AwardName = "Golden Globe - Best Entertainer",
                                AwardDate = new DateTime(2023,8,29)
                            },

                            ReleaseDate = new DateTime(2023 , 7 , 21) ,

                            Description = "Barbie and Ken are having the time of their lives in the colorful and seemingly perfect world of Barbie Land. However, when they get a chance to go to the real world, they soon discover the joys and perils of living among humans.",

                         },



                         new Movie()
                        {
                            Name = "Oppenheimer",
                            Image = "https://m.media-amazon.com/images/M/MV5BMDBmYTZjNjUtN2M1MS00MTQ2LTk2ODgtNzc2M2QyZGE5NTVjXkEyXkFqcGdeQXVyNzAwMjU2MTY@._V1_.jpg",
                            Genre = new Genre()
                            {
                                GenreName = "Thriller"
                            },
                            Director = new Director()
                            {
                                Name = "Christopher Nolan",
                                Age = 53
                            },
                            Studios = new Studios()
                            {
                                StudioName = "Universal Pictures",
                                CEO = "Peter Cramer"
                            },

                            Language = "English",

                            Award = new Award()
                            {
                                AwardName = "Academy Award - Best Picture",
                                AwardDate = new DateTime(2023,8,29)
                            },

                            ReleaseDate = new DateTime(2023 , 7 , 21) ,

                            Description = "During World War II, Lt. Gen. Leslie Groves Jr. appoints physicist J. Robert Oppenheimer to work on the top-secret Manhattan Project. Oppenheimer and a team of scientists spend years developing and designing the atomic bomb. Their work comes to fruition on July 16, 1945, as they witness the world's first nuclear explosion, forever changing the course of history.",

                         },


                           new Movie()
                        {
                            Name = "Elemental",
                            Image = "https://lumiere-a.akamaihd.net/v1/images/p_disney_elemental_homeent_v1_2292_0312c1d7.jpeg",
                            Genre = new Genre()
                            {
                                GenreName = "Fantasy"
                            },
                            Director = new Director()
                            {
                                Name = "Peter Sohn",
                                Age = 53
                            },
                            Studios = new Studios()
                            {
                                StudioName = "Walt Disney Studios Motion Pictures",
                                CEO = "Tony Chambers"
                            },

                            Language = "English",

                            Award = new Award()
                            {
                                AwardName = "Emmy Award - Best Picture - Animation",
                                AwardDate = new DateTime(2023,8,29)
                            },

                            ReleaseDate = new DateTime(2023 , 6 , 16) ,

                            Description = "In a city where fire, water, land, and air residents live together, a fiery young woman and a go-with-the-flow guy discover something elemental: how much they actually have in common.",

                         },

                            new Movie()
                        {
                            Name = "Rocky Aur Rani ki Prem Kahani",
                            Image = "https://m.media-amazon.com/images/M/MV5BODEzYzE1MTgtYmJjMC00ODg1LWIxYjAtMjBhOTFmODljYTU0XkEyXkFqcGdeQXVyMTUyNjIwMDEw._V1_.jpg",
                            Genre = new Genre()
                            {
                                GenreName = "Romance"
                            },
                            Director = new Director()
                            {
                                Name = "Karan Johar",
                                Age = 51
                            },
                            Studios = new Studios()
                            {
                                StudioName = "Dharma Productions",
                                CEO = "Apoorva Mehta"
                            },

                            Language = "Hindi",

                            Award = new Award()
                            {
                                AwardName = "Filmare Award - Best RomCom Entertainer",
                                AwardDate = new DateTime(2023,9,4)
                            },

                            ReleaseDate = new DateTime(2023 , 7 , 28) ,

                            Description = "Flamboyant Punjabi Rocky and intellectual Bengali journalist Rani fall in love despite their differences. After facing family opposition, they decide to live with each other's families for three months before getting married.",

                            },

                    });
                    context.SaveChanges();
                }

            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "manidev@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "manikarnikadixit",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

    }
}
