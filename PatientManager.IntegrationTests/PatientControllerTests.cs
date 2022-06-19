using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using PatientManager.API.Models;
using Newtonsoft.Json;
using System.Text;
using PatientManager.Api;

namespace PatientManager.IntegrationTests
{
    public class PatientControllerTests
    {
        //Test whether it adds a patient or not
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
            var response = await client.PostAsync("/api/Patients", jsonString);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        //Test whether it gets the added contact
        [Fact]
        public async Task GetPatient()
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
            var response = await client.PostAsync("/api/Patients", jsonString);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            //See whether the code exists or not
            var getResponse = await client.GetAsync("/api/Patients/b1630f3f-36b4-4a25-a7ee-9b254fb6c08f");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            //Transform patient into a list
            var responseContentString = await getResponse.Content.ReadAsStringAsync();
            var OnePatient = JsonConvert.DeserializeObject<Patient>(responseContentString);

            //All patients should contain John Doe`s Id
            OnePatient.Id.ShouldBe(JohnDoe.Id);
        }

        //Test whether it deletes the added patient
        [Fact]
        public async Task DeletePatient()
        {
            //Make connection
            await using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();

            //Create a new patient
            Patient JohnDoe = new Patient()
            {
                Id = new Guid("37445af2-c0d4-4277-8724-7aec1857c87d"),
                FirstName = "John",
                LastName = "Doe",
                Age = 45,
                Diagnosis = "Diabetes",
                DateAdmitted = new DateTime(2022, 5, 17),
                OnTreatment = false
            };

            //Convert values into Json
            var jsonString = new StringContent(JsonConvert.SerializeObject(JohnDoe), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Patients", jsonString);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            //Delete the contact
            var deleteResponse = await client.DeleteAsync("/api/Patients/37445af2-c0d4-4277-8724-7aec1857c87d");
            deleteResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            //See whether the code exists or not
            var getResponse = await client.GetAsync("/api/Patients/37445af2-c0d4-4277-8724-7aec1857c87d");
            getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Test whether it gets all the patients
        [Fact]
        public async Task GetAllPatients()
        {
            //Make connection
            await using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();

            //Create a new patient
            Patient JohnDoe = new Patient()
            {
                Id = new Guid("37445af2-c0d4-4277-8724-7aec1857c87d"),
                FirstName = "John",
                LastName = "Doe",
                Age = 45,
                Diagnosis = "Diabetes",
                DateAdmitted = new DateTime(2022, 5, 17),
                OnTreatment = false
            };

            //Convert values into Json
            var jsonString = new StringContent(JsonConvert.SerializeObject(JohnDoe), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Patients", jsonString);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            //See whether the code exists or not
            var getAllResponse = await client.GetAsync("/api/Patients");
            getAllResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            //Transform contact into a list
            var responseContentString = await getAllResponse.Content.ReadAsStringAsync();
            var allPatients = JsonConvert.DeserializeObject<List<Patient>>(responseContentString);

            //allContacts should contain the example content
            allPatients.ShouldContain(patient => patient.Id == JohnDoe.Id);
        }

        //Test whether you can edit the created value
        [Fact]
        public async Task EditPatient()
        {
            //Make connection
            await using var application = new WebApplicationFactory<Startup>();
            using var client = application.CreateClient();

            //Create a new patient
            var JohnDoe = new Patient()
            {
                Id = new Guid("37445af2-c0d4-4277-8724-7aec1857c87d"),
                FirstName = "John",
                LastName = "Doe",
                Age = 45,
                Diagnosis = "Diabetes",
                DateAdmitted = new DateTime(2022, 5, 17),
                OnTreatment = false
            };

            //Convert values into Json
            var testContactStringContent = new StringContent(JsonConvert.SerializeObject(JohnDoe), Encoding.UTF8, "application/json");
            var postResponse = await client.PostAsync("/api/Patients", testContactStringContent);
            postResponse.StatusCode.ShouldBe(HttpStatusCode.Created);

            //See whether the code exists or not
            var getAllResponse = await client.GetAsync($"/api/Patients/{JohnDoe.Id}");
            getAllResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            //Make modifications
            var ModidiedJohn = new Patient()
            {
                Id = new Guid("37445af2-c0d4-4277-8724-7aec1857c87d"),
                FirstName = "John",
                LastName = "Doe",
                Age = 55,
                Diagnosis = "PTSD",
                DateAdmitted = new DateTime(2032, 5, 17),
                OnTreatment = true
            };

            //See whether you can edit the contact or not
            var testContactStringContent_2 = new StringContent(JsonConvert.SerializeObject(ModidiedJohn), Encoding.UTF8, "application/json");
            var putResponse = await client.PutAsync($"/api/Patients/{JohnDoe.Id}", testContactStringContent_2);
            putResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}