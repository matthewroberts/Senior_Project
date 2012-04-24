using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Core.Infrastructure.Repositories
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly EMRECDataContext _db;

        public GeneralRepository(string connstring)
        {
            _db = new EMRECDataContext(connstring);
        }

        public IList<DocumentType> GetDocumentTypes()
        {
            var types = new List<DocumentType>();
            using (_db)
            {
                types =
                    _db.tlbDocumentTypes.Where(t => t.Id != 99).Select(
                        t => new DocumentType {Type = t.Type, TypeId = t.Id}).ToList();
            }
            return types;
        }

        public IList<Visit> GetVisitDatesByPatient(int patientId)
        {
            var visits = new List<Visit>();
            using (_db)
            {
                visits =
                    _db.tblVisits.Where(v => v.PatientId == patientId).Select(
                        v => new Visit {Date = v.Date.ToShortDateString(), VisitId = v.Id}).ToList();
            }
            return visits;
        }

        public tblConfigPath FileStoreDirectory()
        {
            tblConfigPath directory;
            using (_db)
            {
                directory = _db.tblConfigPaths.First(d => d.pathType == "Server_Store");
            }
            return directory;
        }

        public tblConfigPath ServerDriveLetter()
        {
            tblConfigPath drive;
            using (_db)
            {
                drive = _db.tblConfigPaths.First(d => d.pathType == "Server_Drive");
            }
            return drive;
        }

        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();
        }
    }
}
