﻿

using Real_estate.Application.Features.Properties.Commands;

namespace Real_estate.Application.Features.Properties.Queries.GetByOwnerUsername
{
    public class GetByOwnerUsernamePropertyResponse
    {
        public List<PropertyDto> Properties { get; set; }
    }
}
