//Implement EntityFrameworkCore
using Microsoft.EntityFrameworkCore;

namespace PatientManager.API.Models
{
    public class PatientDBContext : DbContext
    {
        //The configuration of PatientDBContext
        public PatientDBContext(DbContextOptions<PatientDBContext> options)
            : base(options)
        {
        }

        //Create a DBSet property that will represent the collection of Patients
        public DbSet<Patient> Patients { get; set; }
    }
}
