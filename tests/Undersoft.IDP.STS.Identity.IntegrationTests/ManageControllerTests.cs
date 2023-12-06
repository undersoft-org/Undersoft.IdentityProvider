﻿using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Undersoft.IDP.STS.Identity.IntegrationTests.Base;
using Undersoft.IDP.STS.Identity.IntegrationTests.Common;
using Undersoft.IDP.STS.Identity.IntegrationTests.Mocks;
using Xunit;

namespace Undersoft.IDP.STS.Identity.IntegrationTests
{
    public class ManageControllerTests : BaseClassFixture
    {
        public ManageControllerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AuthorizeUserCanAccessManageViews()
        {
            // Clear headers
            Client.DefaultRequestHeaders.Clear();

            // Register new user
            var registerFormData = UserMocks.GenerateRegisterData();
            var registerResponse = await UserMocks.RegisterNewUserAsync(Client, registerFormData);

            // Get cookie with user identity for next request
            Client.PutCookiesOnRequest(registerResponse);

            foreach (var route in RoutesConstants.GetManageRoutes())
            {
                // Act
                var response = await Client.GetAsync($"/Manage/{route}");

                // Assert
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task UnAuthorizeUserCannotAccessManageViews()
        {
            // Clear headers
            Client.DefaultRequestHeaders.Clear();

            foreach (var route in RoutesConstants.GetManageRoutes())
            {
                // Act
                var response = await Client.GetAsync($"/Manage/{route}");

                // Assert      
                response.StatusCode.Should().Be(HttpStatusCode.Redirect);

                //The redirect to login
                response.Headers.Location.ToString().Should().Contain("Account/Login");
            }
        }

        [Fact]
        public async Task UserIsAbleToUpdateProfile()
        {
            // Clear headers
            Client.DefaultRequestHeaders.Clear();

            // Register new user
            var registerFormData = UserMocks.GenerateRegisterData();
            var registerResponse = await UserMocks.RegisterNewUserAsync(Client, registerFormData);

            // Get cookie with user identity for next request
            Client.PutCookiesOnRequest(registerResponse);

            // Prepare request to update profile
            const string manageAction = "/Manage/Index";
            var manageResponse = await Client.GetAsync(manageAction);
            var antiForgeryToken = await manageResponse.ExtractAntiForgeryToken();

            var manageProfileData = UserMocks.GenerateManageProfileData(registerFormData["Email"], antiForgeryToken);

            // Update profile
            var requestWithAntiForgeryCookie = RequestHelper.CreatePostRequestWithCookies(manageAction, manageProfileData, manageResponse);
            var requestWithIdentityCookie = CookiesHelper.CopyCookiesFromResponse(requestWithAntiForgeryCookie, registerResponse);
            var responseMessage = await Client.SendAsync(requestWithIdentityCookie);

            // Assert      
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Redirect);

            //The redirect to login
            responseMessage.Headers.Location.ToString().Should().Be("/Manage");
        }
    }
}
