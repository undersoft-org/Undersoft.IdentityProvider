﻿using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Undersoft.IDP.Admin.BusinessLogic.Dtos.Configuration;
using Undersoft.IDP.Admin.EntityFramework.Extensions.Common;

namespace Undersoft.IDP.Admin.BusinessLogic.Mappers
{
    public static class ApiScopeMappers
    {
        static ApiScopeMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiScopeMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static ApiScopesDto ToModel(this PagedList<ApiScope> scopes)
        {
            return scopes == null ? null : Mapper.Map<ApiScopesDto>(scopes);
        }

        public static ApiScopeDto ToModel(this ApiScope resource)
        {
            return resource == null ? null : Mapper.Map<ApiScopeDto>(resource);
        }

        public static ApiScope ToEntity(this ApiScopeDto resource)
        {
            return resource == null ? null : Mapper.Map<ApiScope>(resource);
        }

        public static ApiScopeProperty ToEntity(this ApiScopePropertiesDto resource)
        {
            return resource == null ? null : Mapper.Map<ApiScopeProperty>(resource);
        }

        public static ApiScopePropertiesDto ToModel(this PagedList<ApiScopeProperty> scope)
        {
            return scope == null ? null : Mapper.Map<ApiScopePropertiesDto>(scope);
        }

        public static ApiScopePropertiesDto ToModel(this ApiScopeProperty scope)
        {
            return scope == null ? null : Mapper.Map<ApiScopePropertiesDto>(scope);
        }
    }
}