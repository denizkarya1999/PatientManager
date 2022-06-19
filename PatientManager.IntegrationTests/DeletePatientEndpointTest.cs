using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using PatientManager.API.Models;
using PatientManager.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Shouldly;
using Newtonsoft.Json;

namespace PatientManager.IntegrationTests
{
    public class DeletePatientEndpointTest
    {
        //Check whether the endpoint is able to delete the patient
        [Fact]
        public async Task DeletePatient()
        {
            //Make connection
            await using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();

            //Create a new patient
            Patient JohnDoe = new Patient()
            {
                Id = new Guid("b1630f3f-36b4-4a25-a7ee-9b254fb6c08f"),
                FirstName = "Deniz",
                LastName = "Acikbas",
                Age = 22,
                Diagnosis = "N/A",
                DateAdmitted = new DateTime(2022, 5, 17),
                OnTreatment = false
            };

            //Convert values into Json
            var jsonString = new StringContent(JsonConvert.SerializeObject(JohnDoe), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Patients", jsonString);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            //Delete the patient
            var deleteResponse = await client.DeleteAsync("/Patients/b1630f3f-36b4-4a25-a7ee-9b254fb6c08f");
            deleteResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            //See whether the patient exists or not
            var getResponse = await client.GetAsync("/Patients/b1630f3f-36b4-4a25-a7ee-9b254fb6c08f");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

    }
}
