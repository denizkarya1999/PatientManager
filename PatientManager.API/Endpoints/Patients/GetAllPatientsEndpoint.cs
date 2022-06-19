using PatientManager.API.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace PatientManager.Api.Endpoints.Patients
{
    //GetAllPatientsEndpoint will extend into EndPointBaseAsync without a request and it will get a list of patients
    public class GetAllPatientsEndpoint : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<Patient>>
    {
        //Have a readonly variable from PatientDBContext
        private readonly PatientDBContext _context;

        //Get the context from PatientDBContext
        public GetAllPatientsEndpoint(PatientDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpGet("/patients")]
        [SwaggerOperation(Summary = "Get all patients",
            Description = "Get all patients",
            OperationId = "Patient.GetAll",
            Tags = new[] { "Endpoints for Patient API" })]
        public override async Task<ActionResult<List<Patient>>> HandleAsync
            (CancellationToken cancellationToken = default)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }

            return await _context.Patients.ToListAsync();
        }
    }
}
