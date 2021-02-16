using System;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using UserManager.Tests.Common.Api.Clients;
using UserManager.Tests.Common.Api.Mock;
using UserManager.Tests.Common.Mongo;
using UserManager.Tests.Common.Mongo.Models;

namespace UserManager.Tests.Tests.UserTests
{
    public class UserTests
    {
        [Test]
        public void GetUserById_UserReturnedSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var email = $"{id}@pm.bet";
            var externalId = 2;
            var expectedAvatar = "FakeAvatar";

            var expectedUser = new UserDto
            {
                Id = id,
                Email = email,
                FirstName = $"{id}FirstName",
                LastName = $"{id}LastName",
                ExternalId = externalId
            };

            var userFakeResponse = new UserResponse
            {
                User = new User
                {
                    Id = 2,
                    Email = email,
                    FirstName = "FakeFirstName",
                    LastName = "FakeLastName",
                    Avatar = expectedAvatar
                }
            };

            MongoDbController.AddUser(expectedUser);

            Mock.Get($"/api/users/{externalId}", userFakeResponse);

            // Act
            var response = UserManagerClient.GetUserById(id);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode is incorrect");
            var actualUser = JsonConvert.DeserializeObject<Common.Api.Contracts.User>(response.Content);

            Assert.AreEqual(expectedUser.Email, actualUser.Email, "Email is incorrect");
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName, "FirstName is incorrect");
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName, "LastName is incorrect");
            Assert.AreEqual(expectedAvatar, actualUser.Avatar, "Avatar is incorrect");
        }
    }
}