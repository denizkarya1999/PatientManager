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
    public class GetByIdRepoDbEndpointTest
    {
        //Test whether you can get the patient by Id or not (using RepoDb)
        [Fact]
        public async Task GetPatientBasedOnID()
        {
            //Make connection
            await using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();

            //Create a new patient
            Patient JohnDoe = new Patient()
            {
                Id = new Guid("b1630f3f-36b4-4a25-a7ee-9b254fb6c08f"),
                FirstName = "John",
                LastName = "Doe",
                Age = 45,
                Diagnosis = "Diabetes",
                DateAdmitted = new DateTime(2022, 5, 17),
                OnTreatment = false
            };

            //Convert values into Json
            var jsonString = new StringContent(JsonConvert.SerializeObject(JohnDoe), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/patients/repodb", jsonString);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            //See whether the patient exists or not
            var getResponse = await client.GetAsync("/patients/repodb/b1630f3f-36b4-4a25-a7ee-9b254fb6c08f}");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            //Transform the patient into a list
            var responseContentString = await getResponse.Content.ReadAsStringAsync();
            var OnePatient = JsonConvert.DeserializeObject<Patient>(responseContentString);

            //All patients should contain the example content
            OnePatient.Id.ShouldBe(JohnDoe.Id);
        }
    }
}
