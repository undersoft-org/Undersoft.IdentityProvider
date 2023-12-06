﻿using System.Linq;
using FluentAssertions;
using Undersoft.IDP.Admin.BusinessLogic.Mappers;
using Undersoft.IDP.Admin.UnitTests.Mocks;
using Xunit;

namespace Undersoft.IDP.Admin.UnitTests.Mappers
{
    public class IdentityResourceMappers
    {
        [Fact]
        public void CanMapIdentityResourceToModel()
        {
            //Generate entity
            var identityResource = IdentityResourceMock.GenerateRandomIdentityResource(1);

            //Try map to DTO
            var identityResourceDto = identityResource.ToModel();

            //Assert
            identityResourceDto.Should().NotBeNull();

            identityResourceDto.Should().BeEquivalentTo(identityResource, options =>
                options.Excluding(o => o.UserClaims)
		            .Excluding(o => o.Properties)
		            .Excluding(o => o.Created)
		            .Excluding(o => o.Updated)
		            .Excluding(o => o.NonEditable));

            //Assert collection
            identityResource.UserClaims.Select(x => x.Type).Should().BeEquivalentTo(identityResourceDto.UserClaims);
        }

        [Fact]
        public void CanMapIdentityResourceDtoToEntity()
        {
            //Generate DTO
            var identityResourceDto = IdentityResourceDtoMock.GenerateRandomIdentityResource(1);

            //Try map to entity
            var identityResource = identityResourceDto.ToEntity();

            identityResource.Should().NotBeNull();

            identityResourceDto.Should().BeEquivalentTo(identityResource, options =>
                options.Excluding(o => o.UserClaims)
				.Excluding(o => o.Properties)
		            .Excluding(o => o.Created)
		            .Excluding(o => o.Updated)
		            .Excluding(o => o.NonEditable));

            //Assert collection
            identityResource.UserClaims.Select(x => x.Type).Should().BeEquivalentTo(identityResourceDto.UserClaims);
        }
    }
}
