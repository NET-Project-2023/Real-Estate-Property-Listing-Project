using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_estate.Application.Features.Properties.Queries.GetByName
{
    public record GetByNamePropertyQuery(string Title) : IRequest<PropertyDto>;
}
