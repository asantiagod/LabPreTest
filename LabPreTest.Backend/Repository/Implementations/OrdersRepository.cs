﻿using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;

        public OrdersRepository(DataContext context, IUsersRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PagingDTO pagination)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
                return BuildErrorActionResponse<IEnumerable<Order>>("Usuario no valido");

            var queryable = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Details!)
                .ThenInclude(d => d.Test)
                .Include(o => o.Details!)
                .ThenInclude(d => d.Medic)
                .Include(o => o.Details!)
                .ThenInclude(d => d.Patient)
                .AsQueryable();

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
                queryable = queryable.Where(o => o.User!.Email == email);

            var result = await queryable
                .OrderBy(x => x.Id)
                .Paginate(pagination)
                .ToListAsync();
            return BuildSuccessfulActionResponse<IEnumerable<Order>>(result);
        }

        public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PagingDTO pagination)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
                return BuildErrorActionResponse<int>("Usuario no valido");

            var queryable = _context.Orders.AsQueryable();

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
                queryable = queryable.Where(o => o.User!.Email == email);

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);

            return BuildSuccessfulActionResponse((int)totalPages);
        }

        public override async Task<ActionResponse<Order>> GetAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User!)
                .ThenInclude(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .Include(o => o.Details!)
                .ThenInclude(od => od.Test)
                .Include(o => o.Details!)
                .ThenInclude(od => od.Medic)
                .Include(o => o.Details!)
                .ThenInclude(od => od.Patient)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return BuildErrorActionResponse<Order>("La orden no existe");

            return BuildSuccessfulActionResponse(order);
        }

        public async Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
                return BuildErrorActionResponse<Order>("El usuario no existe");

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            var isUser = await _usersRepository.IsUserInRoleAsync(user, UserType.User.ToString());
            if (!isAdmin && !isUser)
                return BuildErrorActionResponse<Order>("No tienes permiso para realizar esta acción");

            var order = await _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id != orderDTO.Id);
            if (order == null)
                return BuildErrorActionResponse<Order>("La orden no existe");

            order.Status = orderDTO.Status;
            _context.Update(order);
            await _context.SaveChangesAsync();

            return BuildSuccessfulActionResponse(order);
        }

        private static ActionResponse<T> BuildErrorActionResponse<T>(string errorMessage)
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = errorMessage,
            };
        }

        private static ActionResponse<T> BuildSuccessfulActionResponse<T>(T result)
        {
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = result
            };
        }
    }
}