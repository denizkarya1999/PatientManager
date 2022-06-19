using PatientManager.API.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PatientManager.Api.Endpoints.Patients
{
    //DeletePatientEndpoint class will extend into EndpointBaseAsync with an ID request and will get a Patient based on that ID
    public class DeletePatientEndpoint : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult
    {
        //Have a readonly variable from PatientDBContext
        private readonly PatientDBContext _context;

        //Get the context from PatientDBContext
        public DeletePatientEndpoint(PatientDBContext context)
        {
            _context = context;
        }
        //Do the same action as it was available in controller
        [HttpDelete("patients/{id}")]
        [SwaggerOperation(Summary = "Delete the patient",
            Description = "Delete the patient",
            OperationId = "Patient.Delete",
            Tags = new[] { "Endpoints for Patient API" })]
        public override async Task<ActionResult> HandleAsync(Guid id, CancellationToken cancellationToken = default)
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

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
