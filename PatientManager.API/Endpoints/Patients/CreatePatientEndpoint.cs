using PatientManager.API.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PatientManager.Api.Endpoints.Patients
{
    //CreatePatientEndpoint class will extend into EndpointBaseAsync with request to Patient and will give no result
    public class CreatePatientEndpoint : EndpointBaseAsync
        .WithRequest<Patient>
        .WithoutResult
    {

        //Have a readonly variable from PatientDBContext
        private readonly PatientDBContext _context;

        //Get the context from PatientDBContext
        public CreatePatientEndpoint(PatientDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpPost("/patients")]
        [SwaggerOperation(Summary = "Create a new patient",
            Description = "Create a new patient", 
            OperationId = "Patient.Create",
            Tags = new[] {"Endpoints for Patient API"})]
        public override async Task<ActionResult> HandleAsync(Patient patient, CancellationToken cancellationToken = default)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'PatientDBContext.Patients'  is null.");
            }
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
