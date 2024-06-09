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
            {
                return new ActionResponse<ImageDTO>
                {
                    WasSuccess = false,
                    Message = "Sección no existe"
                };
            }

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
            return new ActionResponse<ImageDTO>
            {
                WasSuccess = true,
                Result = imageDTO
            };

        }


        public async Task<ActionResponse<ImageDTO>> RemoveLastImageAsync(ImageDTO imageDTO)
        {
            var section = await _context.Section
                .Include(x => x.SectionImages)
                .FirstOrDefaultAsync(x => x.Id == imageDTO.SectionId);
            if (section == null)
            {
                return new ActionResponse<ImageDTO>
                {
                    WasSuccess = false,
                    Message = "Seccion no existe"
                };
            }

            if (section.SectionImages is null || section.SectionImages.Count == 0)
            {
                return new ActionResponse<ImageDTO>
                {
                    WasSuccess = true,
                    Result = imageDTO
                };
            }

            var lastImage = section.SectionImages.LastOrDefault();
            await _fileStorage.RemoveFileAsync(lastImage!.Image, "sections");
            _context.SectionImage.Remove(lastImage);

            await _context.SaveChangesAsync();
            imageDTO.Images = section.SectionImages.Select(x => x.Image).ToList();
            return new ActionResponse<ImageDTO>
            {
                WasSuccess = true,
                Result = imageDTO
            };
        }

        public override async Task<ActionResponse<IEnumerable<Section>>> GetAsync()
        {
            var test = await _context.Section
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Section>>
            {
                WasSuccess = true,
                Result = test
            };
        }

        public override async Task<ActionResponse<Section>> GetAsync(int id)
        {
            var section = await _context.Section
                .FirstOrDefaultAsync(c => c.Id == id);
            if (section == null)
            {
                return new ActionResponse<Section>
                {
                    WasSuccess = false,
                    Message = MessageStrings.DbCountryNotFoundMessage
                };
            }

            return new ActionResponse<Section>
            {
                WasSuccess = true,
                Result = section
            };
        }

        public override async Task<ActionResponse<IEnumerable<Section>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Section
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            return new ActionResponse<IEnumerable<Section>>
            {
                WasSuccess = true,
                Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(paging)
                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Tests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            int count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };

        }
    }
}
