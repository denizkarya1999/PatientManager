using PatientManager.API.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace PatientManager.Api.Endpoints.Patients
{
    //InActiveEndpoint class will extend into EndPointBaseAsync with request to Patient without showing a result
    public class StartTreatmentPatientEndpoint : EndpointBaseAsync
        .WithRequest<Guid>
        .WithoutResult
    {
        //Have a readonly variable from PatientDBContext
        private readonly PatientDBContext _context;

        //Get the context from PatientDBContext
        public StartTreatmentPatientEndpoint(PatientDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpPut("/patients/{id}/ontreatment")]
        [SwaggerOperation(Summary = "Start the treatment for patient",
            Description = "Start the treatment for patient",
            OperationId = "Patient.StartTreatment",
            Tags = new[] { "Endpoints for Patient API" })]
        public override async Task<ActionResult> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //Try to find the contact
            if (_context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            //Make changes on the patient
            patient.StartTreatment();

            //Save changes
            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
    }
}
