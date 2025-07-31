using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Portfolio.Data
{

    public static class SeedData
    {
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var context = services.GetRequiredService<AppDbContext>();

            // Ensure DB is created and up to date
            context.Database.Migrate();

            // Create roles
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create Admin user
            var adminEmail = "bonganerampai@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Bongani Rampai",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Create Regular user
            var userEmail = "user@example.com";
            var normalUser = await userManager.FindByEmailAsync(userEmail);
            if (normalUser == null)
            {
                normalUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FullName = "Test User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(normalUser, "User@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }

            // OPTIONAL: Add sample data like blog posts, skills, testimonials here
            // e.g. context.BlogPosts.Add(...); await context.SaveChangesAsync();
            // Seed projects
            //if (!context.Projects.Any(p => p.Title == "Portfolio Website"))
            if (!context.Projects.Any())
            {
                context.Projects.AddRange(
                    new Project
                    {
                        Title = "Portfolio Website",
                        Description = "A personal portfolio using ASP.NET Core MVC.",
                        Technologies = "ASP.NET Core, EF Core, Bootstrap",
                        GitHubLink = "https://github.com/BonganiRampai/portfolio",
                        ImageUrl = "/images/project1.jpg",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Project
                    {
                        Title = "Task Manager",
                        Description = "A simple task management system with user auth.",
                        Technologies = "ASP.NET Core, Identity, SQLServer",
                        GitHubLink = "https://github.com/BonganiRampai/taskmanager",
                        ImageUrl = "/images/project7.jpg",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Project
                    {
                        Title = "Digital Clock",
                        Description = "A simple digital clock that displays real time.",
                        Technologies = "HTML, CSS and JavaScript",
                        GitHubLink = "https://github.com/BonganiRampai/digital-clock",
                        ImageUrl = "/images/project4.png",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Project
                    {
                        Title = "Superhero App",
                        Description = "A superhero app that displays avengers and their biography",
                        Technologies = "HTML and CSS",
                        GitHubLink = "https://github.com/BonganiRampai/Superhero-App",
                        ImageUrl = "/images/project3.png",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Project
                    {
                        Title = "Pie-City-Zoo-App",
                        Description = "A zoo web app that displays variety of animals in the zoo and their biography.",
                        Technologies = "HTML and CSS",
                        GitHubLink = "https://github.com/BonganiRampai/Pie-City-Zoo-App",
                        ImageUrl = "/images/project2.png",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Project
                    {
                        Title = "CarParts",
                        Description = "A .NET app that shows carparts, login and registering forms for different users.",
                        Technologies = "ASP.NET Core, Identity, SQLServer",
                        GitHubLink = "https://github.com/BonganiRampai/CarParts",
                        ImageUrl = "/images/project5.png",
                        CreatedAt = DateTime.UtcNow
                    }
                );
                await context.SaveChangesAsync();
            }

            // Seed blog posts
            if (!context.Blogs.Any())
            {
                context.Blogs.AddRange(
                    new Blog
                    {
                        Title = "Welcome to My Dev Blog",
                        Content = "This is a placeholder blog post about my journey.",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Blog
                    {
                        Title = "How I Built This Portfolio",
                        Content = "This post explains the tech stack and challenges...",
                        CreatedAt = DateTime.UtcNow
                    }
                );
                await context.SaveChangesAsync();
            }

            // Seed contact messages
            if (!context.ContactMessages.Any())
            {
                context.ContactMessages.AddRange(
                    new ContactMessage
                    {
                        Name = "Bongani Rampai",
                        Email = "bonganerampai@gmail.com",
                        Message = "Hello, I loved your projects!",
                        SubmittedAt = DateTime.UtcNow
                    },
                    new ContactMessage
                    {
                        Name = "Lethabo Rampai",
                        Email = "thizarampai@gmail.com",
                        Message = "Hello, I loved your projects!",
                        SubmittedAt = DateTime.UtcNow
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
