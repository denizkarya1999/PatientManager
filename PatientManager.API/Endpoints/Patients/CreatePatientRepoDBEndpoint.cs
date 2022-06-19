using PatientManager.API.Models;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.Annotations;

namespace PatientManager.Api.Endpoints.Patients
{
    //CreatePatientEndpoint class will extend into EndpointBaseAsync with request to ResponseDto and will give no result
    public class CreatePatientRepoDBEndpoint : EndpointBaseAsync
        .WithRequest<Patient>
        .WithoutResult
    {
        //Create a connectionString to be used
        private readonly string connectionString = @"Data Source=EPITEC-27\SQLEXPRESS04;Initial Catalog=PatientsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Do the same action as it was available in controller
        [HttpPost("patients/repodb")]
        [SwaggerOperation(Summary = "Create a new patient",
            Description = "Create a new patient",
            OperationId = "Patient.Create.RepoDB",
            Tags = new[] { "Endpoints for Patient API using RepoDB" })]
        public override async Task<ActionResult<Patient>> HandleAsync(Patient patient, CancellationToken cancellationToken = default)
        {
            //Open an Sql connection
            using (var connection = new SqlConnection(connectionString))
            {

                //Execute SQL Comment to add the patient
                /*
                 * INSERT INTO [dbo].[Patient] (Id, FirstName, LastName, Age, Diagnosis, DateAdmitted, OnTreatment)
                 * VALUES ('@Id', '@FirstName', '@LastName', '@Age', '@Diagnosis', '@DateAdmitted', '@OnTreatment');
                 */
                connection.Open();
                var cmd = new SqlCommand("INSERT INTO [dbo].[Patient] (Id, FirstName, LastName, Age, Diagnosis, DateAdmitted, OnTreatment) VALUES (@Id, @FirstName, @LastName, @Age, @Diagnosis, @DateAdmitted, @OnTreatment);", connection);
                cmd.Parameters.AddWithValue("@Id", patient.Id);
                cmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                cmd.Parameters.AddWithValue("@Age", patient.Age);
                cmd.Parameters.AddWithValue("@Diagnosis", patient.Diagnosis);
                cmd.Parameters.AddWithValue("@DateAdmitted", patient.DateAdmitted);
                cmd.Parameters.AddWithValue("@OnTreatment", patient.OnTreatment);
                cmd.ExecuteNonQuery();
                connection.Close();

                //Write a parameter to add 
                return Ok();
            }
        }
    }
}
