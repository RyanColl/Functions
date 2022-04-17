using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Functions.Models;
using System.Linq;

namespace Functions
{
    public class Function
    {
        private readonly ApplicationDbContext _context;

        public Function(ApplicationDbContext context)
        {
            _context = context;
        }
        [FunctionName("Function")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("GetPatients")]
        public IActionResult GetPatients(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "patients")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP GET/posts trigger function processed a request in GetPatients().");

            var patients = _context.Patients.ToArray();

            return new OkObjectResult(patients);
        }

        [FunctionName("GetPatient")]
        public IActionResult GetPatient(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "patients/{id}")] HttpRequest req,
        ILogger log, int id)
        {
            log.LogInformation("C# HTTP GET/posts trigger function processed a request.");
            if (id < 1)
            {
                return new NotFoundResult();
            }
            var patient = _context.Patients.FindAsync(id).Result;
            if (patient == null)
            {
                return new NotFoundResult();
            }
            log.LogInformation(patient.PatientId.ToString());
            return new OkObjectResult(patient);
        }

        [FunctionName("CreatePatient")]
        public async Task<IActionResult> CreatePatient(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "patients")] HttpRequest req,
        ILogger log)
        {
            log.LogInformation("C# HTTP POST/posts trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Patient>(requestBody);
            var patient = new Patient()
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Gender = input.Gender,
                DateOfBirth = input.DateOfBirth,
                Street = input.Street,
                City = input.City,
                Province = input.Province,
                PostalCode = input.PostalCode,
                Country = input.Country,
                Email = input.Email,
                Phone = input.Phone,
                RoomNumber = input.RoomNumber,
                InDate = input.InDate,
                OutDate = input.OutDate,
            };
            _context.Add(patient);
            await _context.SaveChangesAsync();
            log.LogInformation(requestBody);
            return new OkObjectResult(patient);
        }

        [FunctionName("UpdatePatient")]
        public async Task<IActionResult> UpdatePatient(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "patients/{id}")] HttpRequest req,
        ILogger log, int id) {
            log.LogInformation("C# HTTP PUT/posts trigger function processed a request.");
            if (id < 1)
            {
                return new NotFoundResult();
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return new NotFoundResult();
            }
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Patient>(requestBody);
                patient.PatientId = input.PatientId;
                patient.FirstName = input.FirstName;
                patient.LastName = input.LastName;
                patient.Gender = input.Gender;
                patient.DateOfBirth = input.DateOfBirth;
                patient.Street = input.Street;
                patient.City = input.City;
                patient.Province = input.Province;
                patient.PostalCode = input.PostalCode;
                patient.Country = input.Country;
                patient.Email = input.Email;
                patient.Phone = input.Phone;
                patient.RoomNumber = input.RoomNumber;
                patient.InDate = input.InDate;
                patient.OutDate = input.OutDate;
            _context.Update(patient);
            await _context.SaveChangesAsync();
            log.LogInformation(requestBody);
            return new OkObjectResult(patient);
        }

        [FunctionName("DeletePatient")]
        public IActionResult DeletePatient(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "patients/{id}")] HttpRequest req,
        ILogger log, int id) {
            log.LogInformation("C# HTTP DELETE/posts trigger function processed a request.");
            if (id < 1)
            {
                return new NotFoundResult();
            }
            var patient = _context.Patients.FindAsync(id).Result;
            if (patient == null)
            {
                return new NotFoundResult();
            }
            _context.Remove(patient);
            _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
