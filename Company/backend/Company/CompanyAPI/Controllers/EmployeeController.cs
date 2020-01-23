﻿using System.Linq;
using System.Threading.Tasks;
using CompanyAPI.Domain.Models;
using CompanyAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeService.GetAll();

            return StatusCode(StatusCodes.Status200OK, new { employees = employees.ToList() });
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]
        public async Task<IActionResult> Get(int id)
        {
            bool employeeExists = await _employeeService.Exists(id);
            if (!employeeExists)
                return StatusCode(StatusCodes.Status406NotAcceptable, new { message = "Employee not exists." });

            var employee = await _employeeService.GetById(id);

            return StatusCode(StatusCodes.Status200OK, new { selectedEmployee = employee });
        }

        [HttpGet]
        [Route("{name: string}")]
        [Authorize("Bearer")]
        public async Task<IActionResult> Get(string name)
        {
            var employees = await _employeeService.FindByName(name);

            return StatusCode(StatusCodes.Status200OK, new { selectedEmployees = employees });
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            var result = await _employeeService.Add(employee);

            return StatusCode(StatusCodes.Status200OK, new { employee = result });
        }

        [HttpPut]
        [Authorize("Bearer")]
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            bool employeeExists = await _employeeService.Exists(employee.Id);
            if (!employeeExists)
                return StatusCode(StatusCodes.Status406NotAcceptable, new { message = "Employee not exists." });

            var alteredEmployee = await _employeeService.Alter(employee);

            return StatusCode(StatusCodes.Status200OK, new { employee = alteredEmployee });
        }

        [HttpDelete]
        [Authorize("Bearer")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromBody] Employee employee)
        {
            _employeeService.Delete(employee);

            return StatusCode(StatusCodes.Status204NoContent);
        }

        //[HttpPost]
        //[Authorize("Bearer")]
        //[Route("addAddress")]
        //public async Task<IActionResult> AddAddress([FromBody] Address address)
        //{
        //    bool employeeExists = await _employeeService.Exists(address.EmployeeId);
        //    if (!employeeExists)
        //        return StatusCode(StatusCodes.Status406NotAcceptable, new { message = "Employee not exists." });

        //    Address newAddress = await _addressService.Add(address);

        //    await _employeeAddressService.Add(new EmployeeAddress(newAddress));

        //    return StatusCode(StatusCodes.Status200OK, new { message = "Add" });
        //}
    }
}