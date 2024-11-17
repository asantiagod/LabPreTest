﻿using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Backend.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LabPreTest.Backend.Controllers
{

    public class GenericController<T> : Controller where T : class
    {
        private readonly IGenericUnitOfWork<T> _unitOfWork;

        public GenericController(IGenericUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Full)]
        public virtual async Task<IActionResult> GetAsync()
        {
            var action = await _unitOfWork.GetAsync();
            if (action.WasSuccess)
                return Ok(action.Result);

            return BadRequest();
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            var action = await _unitOfWork.GetAsync(id);
            if (action.WasSuccess)
                return Ok(action.Result);
            return NotFound();
        }
        [AllowAnonymous]
        [HttpGet]
        public virtual async Task<IActionResult> GetAsync([FromQuery] PagingDTO paging)
        {
            var action = await _unitOfWork.GetAsync(paging);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }
        [AllowAnonymous]
        [HttpGet(ApiRoutes.TotalPages)]
        public virtual async Task<IActionResult> GetPagesAsync([FromQuery] PagingDTO paging)
        {
            var action = await _unitOfWork.GetTotalPagesAsync(paging);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest();
        }
        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(T model)
        {
            var action = await _unitOfWork.AddAsync(model);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest(action.Message);
        }
        [AllowAnonymous]
        [HttpPut]
        public virtual async Task<IActionResult> PutAsync(T model)
        {
            var action = await _unitOfWork.UpdateAsync(model);
            if (action.WasSuccess)
                return Ok(action.Result);
            return BadRequest(action.Message);
        }
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _unitOfWork.DeleteAsync(id);
            if (action.WasSuccess)
                return NoContent();

            return BadRequest(action.Message);
        }
    }
}