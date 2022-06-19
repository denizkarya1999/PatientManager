using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManager.API.Models
{
    [Table("Patient")]
    public class Patient
    {
        //A constructor to set patient's (Id, FirstName, LastName, Age, Gender, SexualOrientation, Diagnosis, DateAdmitted and OnTreatment)
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Diagnosis { get; set; }
        public DateTime? DateAdmitted { get; set; }
        public bool OnTreatment { get; set; }

        //Method to start the patient`s treatment
        public void StartTreatment() 
        { 
            DateAdmitted = DateTime.Now; 
            OnTreatment = true; 
        }
    }
}
