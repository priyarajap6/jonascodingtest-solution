using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET: api/Employee
        public IEnumerable<EmployeeDto> GetAll()
        {
            var items = _employeeService.GetAllEmployee();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]EmployeeDto value)
        {
            EmployeeInfo employee = new EmployeeInfo();
            employee.SiteId = value.SiteId;
            employee.CompanyCode = value.CompanyCode;
            employee.EmployeeCode = value.EmployeeCode;
            employee.EmployeeName = value.EmployeeName;
            employee.Occupation = value.Occupation;
            employee.EmployeeStatus = value.EmployeeStatus;
            employee.EmailAddress = value.EmailAddress;
            employee.Phone = value.Phone;
            employee.LastModified = value.LastModified;

            HttpResponseMessage returnMessage = new HttpResponseMessage();

            try
            {
                _employeeService.AddEmployee(employee);
                returnMessage = new HttpResponseMessage(HttpStatusCode.Created);
                returnMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Post, "Employee Added Successfully");
            }
            catch (Exception ex)
            {
                returnMessage = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                returnMessage.RequestMessage = new HttpRequestMessage(HttpMethod.Post, ex.ToString());
            }
            return await Task.FromResult(returnMessage);
        }
    }
}
