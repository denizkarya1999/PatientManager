using PatientManager.API.Models;
using PatientManager.Shared;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RepoDb;
using Swashbuckle.AspNetCore.Annotations;

namespace PatientManager.Api.Endpoints.Patients
{
    //GetPatientByIDEndpoint class will extend into EndpointBaseAsync with an ID request and will get a Patient based on that ID
    public class GetByIdRepoDbEndpoint : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<PatientResponseDTO>
    {
        //Create a connectionString to be used
        private readonly string connectionString = @"Data Source=EPITEC-27\SQLEXPRESS04;Initial Catalog=PatientsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Do the same action as it was available in controller
        [HttpGet("/patients/repodb/{id}")]
        [SwaggerOperation(Summary = "Get all patients",
            Description = "Get all patients",
            OperationId = "Patient.GetAll.RepoDB",
            Tags = new[] { "Endpoints for Patient API using RepoDB" })]
        public override async Task<ActionResult<PatientResponseDTO>> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //Open an Sql connection
            using (var connection = new SqlConnection(connectionString))
            {
                //Create a parameter to make Id usable for SQL command
                var parameter = new { Id = id };
                //Execute SQL command to get the user by Id
                var patient = (await connection.ExecuteQueryAsync<PatientResponseDTO>("SELECT * FROM [dbo].[Patient] WHERE Id = @Id;", parameter)).FirstOrDefault();
                //Return patient
                return patient;
            }
        }
    }
}
