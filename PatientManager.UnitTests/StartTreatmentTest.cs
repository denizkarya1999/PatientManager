using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PatientManager.API.Models;
using PatientManager.Api;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace PatientManager.UnitTests
{
    public class StartTreatmentTest
    {
        [Fact]
        public void TestStartTreatment()
        {
            //Create a new patient
            Patient patient = new Patient()
            {
                Id = new Guid("b1630f3f-36b4-4a25-a7ee-9b254fb6c08f"),
                FirstName = "John",
                LastName = "Doe",
                Age = 22,
                Diagnosis = "Gonorrhea",
                DateAdmitted = null,
                OnTreatment = false
            };

            //Trigger StartTreatment Function
            patient.StartTreatment();

            //Make sure whether DateAdmitted changed to today and OnTreatment
            Assert.NotNull(patient.DateAdmitted);
            Assert.Equal(patient.OnTreatment, true);

            //Cannot set up the time to today because it keep changing
        }
    }
}