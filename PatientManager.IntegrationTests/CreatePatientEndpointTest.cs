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
    public class CreatePatientEndpointTest
    {
        //Check whether the endpoint creates the patient or not
        [Fact]
        public async Task AddPatient()
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
            var response = await client.PostAsync("/Patients", jsonString);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            //Check whether the status code is okay
            var getAllResponse = await client.GetAsync("/Patients/b1630f3f-36b4-4a25-a7ee-9b254fb6c08f");
            getAllResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            //Transform patient into a list
            var responseContentString = await getAllResponse.Content.ReadAsStringAsync();
            var allPatients = JsonConvert.DeserializeObject<List<Patient>>(responseContentString);

            //allPatients should contain the example content
            allPatients.ShouldContain(patient => patient.Id == JohnDoe.Id);
        }
    }
}
