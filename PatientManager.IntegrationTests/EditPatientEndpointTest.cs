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
    public class EditPatientEndpointTest
    {
        //Test whether you can edit the patient data or not
        [Fact]
        public async Task EditPatient()
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

            //See whether the patient exists or not
            var getResponse = await client.GetAsync("Patients/b1630f3f-36b4-4a25-a7ee-9b254fb6c08f");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            //Make changes on patient data
            Patient JohnicaDoe = new Patient()
            {
                Id = new Guid("b1630f3f-36b4-4a25-a7ee-9b254fb6c08f"),
                FirstName = "Johnica",
                LastName = "Doe",
                Age = 55,
                Diagnosis = "Prostate Cancer",
                DateAdmitted = new DateTime(2022, 5, 17),
                OnTreatment = true
            };

            //Test whether you can edit the patient or not
            var testContactStringContent_2 = new StringContent(JsonConvert.SerializeObject(JohnicaDoe), Encoding.UTF8, "application/json");
            var putResponse = await client.PutAsync("Patients/b1630f3f-36b4-4a25-a7ee-9b254fb6c08f", testContactStringContent_2);
            putResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        }
    }
}
