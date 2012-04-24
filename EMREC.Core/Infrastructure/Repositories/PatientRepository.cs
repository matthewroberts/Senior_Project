using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Core.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly EMRECDataContext _db;
        public PatientRepository(string connstring)
        {
            _db = new EMRECDataContext(connstring);
        }

        public IList<Patient> GetPatientsByPhysician(Physician physician)
        {
            return _db.tblPatients.Where(p => p.tblPhysician.PrescriberID == physician.PrescriberId && p.Active == true).Select(p => new Patient{Active = true, FirstName = p.PatientFirstName, LastName = p.PatientLastName, PatientId = p.PatientID, FullName = p.PatientLastName + ", " + p.PatientFirstName}).ToList();
        }

        public IList<Patient> GetPatientsByName(string lastName, string firstname)
        {
            if (firstname == null)
                return _db.tblPatients.Where(p => p.PatientLastName == lastName).Select(p => new Patient { Active = true, FirstName = p.PatientFirstName, LastName = p.PatientLastName, PatientId = p.PatientID, FullName = p.PatientLastName + ", " + p.PatientFirstName }).ToList();
            else
                return _db.tblPatients.Where(p => p.PatientLastName == lastName && p.PatientFirstName == firstname).Select(p => new Patient { Active = true, FirstName = p.PatientFirstName, LastName = p.PatientLastName, PatientId = p.PatientID, FullName = p.PatientLastName + ", " + p.PatientFirstName }).ToList();
        }

        public string GetPatientsChart(int patientId)
        {
            var chartId = "";
            using (_db)
            {
                chartId = _db.tblPatients.Where(p => p.PatientID == patientId).Select(p => p.ChartID).First();
            }
            return chartId;
        }

        public tblPatient GetPatientById(int id)
        {
            tblPatient patient;
            using (_db)
            {
                patient = _db.tblPatients.FirstOrDefault(p => p.PatientID == id);
            }
            return patient;
        }
    }
}
