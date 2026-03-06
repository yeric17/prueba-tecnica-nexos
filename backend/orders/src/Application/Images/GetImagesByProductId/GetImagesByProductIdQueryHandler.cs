using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Images.DTOs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Images.GetImagesByProductId
{
    internal sealed class GetImagesByProductIdQueryHandler : IQueryHandler<GetImagesByProductIdQuery, List<ImageDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetImagesByProductIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ImageDto>>> Handle(GetImagesByProductIdQuery query, CancellationToken cancellationToken)
        {
            var images = await _context.Images
                .Where(i => i.ProductId == query.ProductId)
                .OrderByDescending(i => i.IsPrimary)
                .ThenBy(i => i.CreatedAt)
                .Select(i => new ImageDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    FileName = i.FileName,
                    FilePath = i.FilePath,
                    ContentType = i.ContentType,
                    FileSize = i.FileSize,
                    IsPrimary = i.IsPrimary,
                    CreatedAt = i.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return images;
        }
    }
}
