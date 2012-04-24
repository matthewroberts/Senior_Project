using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Core.Domain.Interfaces
{
    public interface IPatientRepository
    {
        IList<Patient> GetPatientsByPhysician(Physician physician);
        IList<Patient> GetPatientsByName(string lastName, string firstname);
        String GetPatientsChart(int patientId);
        tblPatient GetPatientById(int id);
    }
}
