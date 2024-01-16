using MediatR;
using Real_estate.Application.Persistence;
using Real_estate.Domain.Entities;

namespace Real_estate.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, DeletePropertyCommandResponse>
    {
        private readonly IPropertyRepository propertyRepository;

        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<DeletePropertyCommandResponse> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var deleteResult = await propertyRepository.DeleteAsyncByTitle(request.PropertyTitle);

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