﻿using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;

        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .OrderBy(x => x.Name)
                .ToListAsync();
            return ActionResponse<IEnumerable<Country>>.BuildSuccessful(countries);
        }

        public override async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var country = await _context.Countries
                .Include(c => c.States!)
                .ThenInclude(s => s.Cities!)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
                return ActionResponse<Country>.BuildFailed(MessageStrings.DbCountryNotFoundMessage);

            return ActionResponse<Country>.BuildSuccessful(country);
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Countries
                .Include(c => c.States)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            var result = await queryable
                            .OrderBy(x => x.Name)
                            .Paginate(paging)
                            .ToListAsync();
            return ActionResponse<IEnumerable<Country>>.BuildSuccessful(result);
        }

        public async Task<IEnumerable<Country>> GetComboAsync()
        {
            return await _context.Countries
                        .OrderBy(c => c.Name)
                        .ToListAsync();
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Countries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            int count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return ActionResponse<int>.BuildSuccessful(totalPages);
        }
    }
}