using System;
using System.Threading.Tasks;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Persistence;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibraryManagement.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private DbContextOptions<LibraryDbContext> CreateDbContextOptions() =>
            new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnCorrectUser()
        {
            // Arrange
            var options = CreateDbContextOptions();

            using (var context = new LibraryDbContext(options))
            {
                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Name = "John",
                        Email = "john.doe@example.com",
                        Password = "password123",
                        Role = "Admin"
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Jane",
                        Email = "jane.smith@example.com",
                        Password = "secure456",
                        Role = "User"
                    }
                );
                await context.SaveChangesAsync();
            }

            // Act
            User result;
            using (var context = new LibraryDbContext(options))
            {
                var repository = new UserRepository(context);
                result = await repository.GetUserByEmailAsync("jane.smith@example.com");
            }

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
            Assert.Equal("Jane", result.Name);
            Assert.Equal("jane.smith@example.com", result.Email);
            Assert.Equal("secure456", result.Password);
            Assert.Equal("User", result.Role);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ShouldReturnNull_WhenEmailNotFound()
        {
            // Arrange
            var options = CreateDbContextOptions();

            using (var context = new LibraryDbContext(options))
            {
                context.Users.Add(new User
                {
                    Id = 1,
                    Name = "John",
                    Email = "john.doe@example.com",
                    Password = "password123",
                    Role = "Admin"
                });
                await context.SaveChangesAsync();
            }

            // Act
            User result;
            using (var context = new LibraryDbContext(options))
            {
                var repository = new UserRepository(context);
                result = await repository.GetUserByEmailAsync("nonexistent@example.com");
            }

            // Assert
            Assert.Null(result);
        }
    }
}
