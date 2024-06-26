﻿using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Interfaces
{
    public interface ITestUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Test>>> GetAsync();

        Task<ActionResponse<Test>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Test>>> GetAsync(PagingDTO paging);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);
    }
}
