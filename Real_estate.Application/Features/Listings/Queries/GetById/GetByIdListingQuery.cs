﻿using MediatR;
using System;

namespace Real_estate.Application.Features.Listings.Queries.GetById
{
    public record GetByIdListingQuery(string title) : IRequest<ListingDto>;
}