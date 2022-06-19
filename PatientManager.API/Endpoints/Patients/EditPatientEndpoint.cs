using PatientManager.API.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using PatientManager.Shared;

namespace PatientManager.Api.Endpoints.Patients
{
    //EditPatientEndpoint class will extend into EndPointBaseAsync with request to Patient without showing a result
    public class EditPatientEndpoint : EndpointBaseAsync
        .WithRequest<PatientResponseDTO>
        .WithoutResult
    {
        //Have a readonly variable from PatientDBContext
        private readonly PatientDBContext _context;

        //Get the context from PatientDBContext
        public EditPatientEndpoint(PatientDBContext context)
        {
            _context = context;
        }

        //Do the same action as it was available in controller
        [HttpPut("/patients/edit")]
        [SwaggerOperation(Summary = "Edit the patient",
            Description = "Edit the patient",
            OperationId = "Patient.Edit",
            Tags = new[] { "Endpoints for Patient API" })]
        public override async Task<ActionResult> HandleAsync(PatientResponseDTO patientReqest, CancellationToken cancellationToken = default)
        {
            var patient = await _context.Patients.FindAsync(patientReqest.Id);

            if (patient is not null)
            {
                patient.Age = patientReqest.Age.GetValueOrDefault();
                patient.OnTreatment = patientReqest.OnTreatment;
                patient.Diagnosis = patientReqest.Diagnosis;
                patient.DateAdmitted = patientReqest.DateAdmitted;
                patient.LastName = patientReqest.LastName;
                patient.FirstName = patientReqest.FirstName;
                
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
