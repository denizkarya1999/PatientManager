namespace PatientManager.Shared
{
    public class PatientResponseDTO
    {
        //Implement same constructors from Patient class except Gender and Sexual Orientation (because we do not want our patients to be discriminated)
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Diagnosis { get; set; }
        public DateTime? DateAdmitted { get; set; }
        public bool OnTreatment { get; set; }
    }
}