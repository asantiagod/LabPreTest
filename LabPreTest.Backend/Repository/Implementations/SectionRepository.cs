using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class SectionRepository : GenericRepository<Section>, ISectionRepository
    {
        private readonly IFileStorage _fileStorage;
        private readonly DataContext _context;

        public SectionRepository(DataContext context, IFileStorage fileStorage) : base(context)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<ActionResponse<ImageDTO>> AddImageAsync(ImageDTO imageDTO)
        {
            var section = await _context.Section
                .Include(x => x.SectionImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.SectionId);
            if (section == null)
                return ActionResponse<ImageDTO>.BuildFailed(MessageStrings.DbSectionNotFoundMessage);

            for (int i = 0; i < imageDTO.Images.Count; i++)
            {
                if (!imageDTO.Images[i].StartsWith("https://"))
                {
                    var photoSection = Convert.FromBase64String(imageDTO.Images[i]);
                    imageDTO.Images[i] = await _fileStorage.SaveFileAsync(photoSection, ".jpg", "products");
                    section.SectionImages!.Add(new SectionImage { Image = imageDTO.Images[i] });
                }
            }

            _context.Update(section);
            await _context.SaveChangesAsync();

            return ActionResponse<ImageDTO>.BuildSuccessful(imageDTO);
        }

        public async Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO)
        {
            var section = await _context.Section
                .Include(x => x.SectionImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.SectionId);
            if (section == null)
                return ActionResponse<ImageDTO>.BuildFailed(MessageStrings.DbSectionNotFoundMessage);

            if (section.SectionImages is null || section.SectionImages.Count == 0)
                return ActionResponse<ImageDTO>.BuildSuccessful(imageDTO);

            var lastImage = section.SectionImages.LastOrDefault();
            await _fileStorage.RemoveFileAsync(lastImage!.Image, "sections");
            _context.SectionImage.Remove(lastImage);

            await _context.SaveChangesAsync();
            imageDTO.Images = section.SectionImages.Select(x => x.Image).ToList();
            return ActionResponse<ImageDTO>.BuildSuccessful(imageDTO);
        }

        public override async Task<ActionResponse<IEnumerable<Section>>> GetAsync()
        {
            var section = await _context.Section
                .OrderBy(x => x.Name)
                .ToListAsync();
            return ActionResponse<IEnumerable<Section>>.BuildSuccessful(section);
        }

        public override async Task<ActionResponse<Section>> GetAsync(int id)
        {
            var section = await _context.Section
                .FirstOrDefaultAsync(c => c.Id == id);
            if (section == null)
                return ActionResponse<Section>.BuildFailed(MessageStrings.DbSectionNotFoundMessage);

            return ActionResponse<Section>.BuildSuccessful(section);
        }

        public override async Task<ActionResponse<IEnumerable<Section>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Section
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            var result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(paging)
                .ToListAsync();
            return ActionResponse<IEnumerable<Section>>.BuildSuccessful(result);
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Tests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            int count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return ActionResponse<int>.BuildSuccessful(totalPages);
        }
    }
}