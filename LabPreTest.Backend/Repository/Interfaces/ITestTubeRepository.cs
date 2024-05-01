﻿using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface ITestTubeRepository
    {
        Task<ActionResponse<IEnumerable<TestTube>>> GetAsync();

        Task<ActionResponse<TestTube>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<TestTube>>> GetAsync(PagingDTO paging);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);
    }
}
