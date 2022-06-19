//Implement the needed libraries
using PatientManager.Shared;
using System.Text;
using System.Text.Json;

namespace PatientManager.Blazor.Services
{
    //PatientService will implement methods from IPatientService
    public class PatientService : IPatientService
    {
        //An HttpClient which will be injected through constructor injection
        private readonly HttpClient _httpClient;

        //Create a constructor to use HttpClient
        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //This method will add new patients using PatientManager.API
        public async Task<PatientResponseDTO> AddPatient(PatientResponseDTO patient)
        {
            //Seriliaze the patient
            var patientJson =
                new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");

            //Post the patient to route of CreatePatientEndpoint
            var response = await _httpClient.PostAsync("/patients", patientJson);

            //Otherwise return as null
            return null;
        }

        //This method will delete the patient from database using PatientManager.API
        public async Task DeletePatient(Guid patientId)
        {
            await _httpClient.DeleteAsync($"/patients/{patientId}");
        }

        //This method will get all of the patients from PatientManager.API
        public async Task<IEnumerable<PatientResponseDTO>> GetAllPatients()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<PatientResponseDTO>>
                (await _httpClient.GetStreamAsync($"/patients"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        //This method will get the patient by id from PatientManager.API
        public async Task<PatientResponseDTO> GetPatientDetails(Guid patientId)
        {
            return await JsonSerializer.DeserializeAsync<PatientResponseDTO>
                (await _httpClient.GetStreamAsync($"/patients/repodb/{patientId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        //This method will edit the patient using PatientManager.API
        public async Task UpdatePatient(PatientResponseDTO patient)
        {

            //Find the patient
            var patientJson =
                new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");

            //Put the patient into system
            await _httpClient.PutAsync($"/patients/edit", patientJson);


        }
    }
}
