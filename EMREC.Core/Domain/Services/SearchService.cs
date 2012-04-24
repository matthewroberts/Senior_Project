using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.DependencyInjection;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Core.Domain.Services
{
    public class SearchService : ISearchService
    {
        private readonly IPhysicianRepository _physicianRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IGeneralRepository _generalRepository;

        public SearchService()
        {
            _physicianRepository = DI.CreatePhysicanRepository();
            _patientRepository = DI.CreatePatientRepository();
            _generalRepository = DI.CreateGeneralrepository();
        }

        public IList<Physician> GetAllPhysicians()
        {
            return _physicianRepository.GetAllPhysicians();
        }

        public IList<Patient> GetPatientByPhysican(Physician physician)
        {
            return _patientRepository.GetPatientsByPhysician(physician);
        }

        public IList<DocumentType> GetDocumentTypes()
        {
            return _generalRepository.GetDocumentTypes();
        }

        public IList<Patient> SearchPatientsByName(string lastName, string firstname = null)
        {
            return _patientRepository.GetPatientsByName(lastName, firstname);
        }

        public IList<Visit> GetPatientVisits(int patientId)
        {
            return _generalRepository.GetVisitDatesByPatient(patientId);
        }

        public string GetPatientChart(string patientId)
        {
            var intPatientId = Convert.ToInt32(patientId);
            return _patientRepository.GetPatientsChart(intPatientId);
        }

        public tblPatient GetPatient(int id)
        {
            return _patientRepository.GetPatientById(id);
        }
    }
}
