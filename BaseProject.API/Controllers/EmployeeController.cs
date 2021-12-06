using BaseProject.API.Domain;
using BaseProject.API.Domain.DTO;
using BaseProject.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BaseProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(IApplicationDbContext dbContext, IApplicationReadDbConnection readDbConnection, IApplicationWriteDbConnection writeDbConnection)
        {
            _dbContext = dbContext;
            _readDbConnection = readDbConnection;
            _writeDbConnection = writeDbConnection;
        }
        public IApplicationDbContext _dbContext { get; }
        public IApplicationReadDbConnection _readDbConnection { get; }
        public IApplicationWriteDbConnection _writeDbConnection { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = $"SELECT * FROM Employees";
            var employees = await _readDbConnection.QueryAsync<Employee>(query);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllEmployeesById(int id)
        {
            var employees = await _dbContext.Employees.Include(a => a.Department).Where(a => a.Id == id).ToListAsync();
            return Ok(employees);
        }

        [HttpPost, Route("NewEmployee")]
        public async Task<IActionResult> AddNewEmployeeWithDepartmentAudit(EmployeeDto employeeDto) 
        {
            if (ModelState.IsValid)
            {
                //TODO: sue automapper to create new employee
                Employee employee = new Employee() 
                {
                    Name = employeeDto.Name,
                    Email = employeeDto.Email,
                    DepartmentId = 1
                };
                _dbContext.Employees.Add(employee);
                //TODO: extract user id: User?.FindFirst(ClaimTypes.NameIdentifier).Value
                await _dbContext.SaveChangesAsync("lreb", default);
                return Ok(employeeDto);
            }
            return BadRequest(employeeDto);
        }

        [HttpPost, Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployeeWithDepartmentAudit(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                //TODO: sue automapper to create new employee
                Employee employee = new Employee()
                {
                    Id = 1,
                    Name = employeeDto.Name,
                    Email = employeeDto.Email,
                    DepartmentId = 1
                };
                _dbContext.Employees.Update(employee);
                //TODO: extract user id: User?.FindFirst(ClaimTypes.NameIdentifier).Value
                await _dbContext.SaveChangesAsync("lreb", default);
                return Ok(employeeDto);
            }
            return BadRequest(employeeDto);
        }

        [HttpPost, Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployeeWithDepartmentAudit(EmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                //TODO: sue automapper to create new employee
                Employee employee = new Employee()
                {
                    Id = 4,
                    Name = employeeDto.Name,
                    Email = employeeDto.Email
                };
                _dbContext.Employees.Remove(employee);
                //TODO: extract user id: User?.FindFirst(ClaimTypes.NameIdentifier).Value
                await _dbContext.SaveChangesAsync("lreb", default);
                return Ok(employeeDto);
            }
            return BadRequest(employeeDto);
        }


        [HttpPost]
        public async Task<IActionResult> AddNewEmployeeWithDepartment(EmployeeDto employeeDto)
        {
            _dbContext.Connection.Open();
            using (var transaction = _dbContext.Connection.BeginTransaction())
            {
                try
                {
                    _dbContext.Database.UseTransaction(transaction as DbTransaction);
                    //Check if Department Exists (By Name)
                    bool DepartmentExists = await _dbContext.Departments.AnyAsync(a => a.Name == employeeDto.Department.Name);
                    if (DepartmentExists)
                    {
                        throw new Exception("Department Already Exists");
                    }
                    //Add Department
                    var addDepartmentQuery = $"INSERT INTO Departments(Name,Description) VALUES('{employeeDto.Department.Name}','{employeeDto.Department.Description}');SELECT CAST(SCOPE_IDENTITY() as int)";
                    var departmentId = await _writeDbConnection.QuerySingleAsync<int>(addDepartmentQuery, transaction: transaction);
                    //Check if Department Id is not Zero.
                    if (departmentId == 0)
                    {
                        throw new Exception("Department Id");
                    }
                    //Add Employee
                    var employee = new Employee
                    {
                        DepartmentId = departmentId,
                        Name = employeeDto.Name,
                        Email = employeeDto.Email
                    };
                    await _dbContext.Employees.AddAsync(employee);
                    await _dbContext.SaveChangesAsync(default);
                    //Commmit
                    transaction.Commit();
                    //Return EmployeeId
                    return Ok(employee.Id);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    _dbContext.Connection.Close();
                }
            }
        }
    }
}
