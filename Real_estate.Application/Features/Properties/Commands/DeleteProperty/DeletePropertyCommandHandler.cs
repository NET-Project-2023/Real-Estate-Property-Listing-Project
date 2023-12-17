using MediatR;
using Real_estate.Application.Features.Properties.Commands.DeleteProperty;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, DeletePropertyCommandResponse>
    {
        private readonly IAsyncRepository<Property> propertyRepository;

        public DeletePropertyCommandHandler(IAsyncRepository<Property> propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<DeletePropertyCommandResponse> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var deleteResult = await propertyRepository.DeleteAsync(request.PropertyId);

            if (!deleteResult.IsSuccess)
            {
                return new DeletePropertyCommandResponse
                {
                    Success = false,
                    Message = deleteResult.Error
                };
            }

            // At this point, the delete was successful
            return new DeletePropertyCommandResponse
            {
                Success = true,
                Message = "Property deleted successfully."
            };
        }
    }
}