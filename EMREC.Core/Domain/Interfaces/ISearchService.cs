using System;
using System.Collections.Generic;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Core.Domain.Interfaces
{
    public interface ISearchService
    {
        IList<Physician> GetAllPhysicians();
        IList<Patient> GetPatientByPhysican(Physician physician);
        IList<DocumentType> GetDocumentTypes();
        IList<Patient> SearchPatientsByName(string lastName, string firstname = null);
        IList<Visit> GetPatientVisits(int patientId);
        String GetPatientChart(string patientId);
        tblPatient GetPatient(int id);
    }
}
