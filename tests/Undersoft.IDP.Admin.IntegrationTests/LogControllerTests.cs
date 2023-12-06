﻿using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Undersoft.IDP.Admin.IntegrationTests.Base;
using Undersoft.IDP.Admin.UI.Configuration.Constants;
using Xunit;

namespace Undersoft.IDP.Admin.IntegrationTests
{
    public class LogControllerTests : BaseClassFixture
    {
        public LogControllerTests(TestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ReturnRedirectInErrorsLogWithoutAdminRole()
        {
            //Remove
            Client.DefaultRequestHeaders.Clear();

            // Act
            var response = await Client.GetAsync("/log/errorslog");

            // Assert           
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);

            //The redirect to login
            response.Headers.Location.ToString().Should().Contain(AuthenticationConsts.AccountLoginPage);
        }

        [Fact]
        public async Task ReturnRedirectInAuditLogWithoutAdminRole()
        {
            //Remove
            Client.DefaultRequestHeaders.Clear();

            // Act
            var response = await Client.GetAsync("/log/auditlog");

            // Assert           
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);

            //The redirect to login
            response.Headers.Location.ToString().Should().Contain(AuthenticationConsts.AccountLoginPage);
        }

        [Fact]
        public async Task ReturnSuccessInErrorsLogWithAdminRole()
        {
            SetupAdminClaimsViaHeaders();

            // Act
            var response = await Client.GetAsync("/log/errorslog");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ReturnSuccessInAuditLogWithAdminRole()
        {
            SetupAdminClaimsViaHeaders();

            // Act
            var response = await Client.GetAsync("/log/auditlog");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
