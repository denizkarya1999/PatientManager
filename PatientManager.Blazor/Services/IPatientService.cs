//Implement the needed library
using PatientManager.Shared;

namespace PatientManager.Blazor.Services
{
    //This is the interface of the PatientService
    interface IPatientService
    {

        //Methods for executing operations (GetAll, GetByID, Add, Update and Delete)
        Task<IEnumerable<PatientResponseDTO>> GetAllPatients();
        Task<PatientResponseDTO> GetPatientDetails(Guid patientId);
        Task<PatientResponseDTO> AddPatient(PatientResponseDTO patient);
        Task UpdatePatient(PatientResponseDTO patient);
        Task DeletePatient(Guid patientId);
    }
}
