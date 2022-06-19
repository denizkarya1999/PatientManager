using PatientManager.API.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PatientManager.Api.Endpoints.Patients
{
    //GetPatientByIDEndpoint class will extend into EndpointBaseAsync with an ID request and will get a Patient based on that ID
    public class GetPatientByIDEndpoint : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<Patient>
    {
        //Have a readonly variable from PatientDBContext
        private readonly PatientDBContext _context;

        //Get the context from PatientDBContext
        public GetPatientByIDEndpoint(PatientDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpGet("patients/{id}")]
        [SwaggerOperation(Summary = "Get the particular patient",
            Description = "Get the particular patient",
            OperationId = "Patient.GetID",
            Tags = new[] { "Endpoints for Patient API" })]
        public override async Task<ActionResult<Patient>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }
    }
}
