using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Core.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly EMRECDataContext _db;
        public DocumentRepository(string connstring)
        {
            _db = new EMRECDataContext(connstring);
        }

        public Document GetDocumentById(int documentId)
        {
            var documents = new Document();
            using (_db)
            {
                documents = _db.tblDocuments.Where(p => p.Id == documentId && p.Active == true).Select(
                     p =>
                     new Document
                     {
                         DocumentId = p.Id,
                         Name = p.Name,
                         ServerPath = p.ServerPath,
                         ServerName = p.ServerName,
                         Active = true,
                         LastUpdated = p.LastUpdated,
                         DocumentType = new DocumentType { TypeId = p.tlbDocumentType.Id, Type = p.tlbDocumentType.Type }
                     }).SingleOrDefault();
            }
            return documents;
        }

        public List<Document> GetDocumentsByPatientId(int patientId)
        {
            List<Document> documents;
            using (_db)
            {
               documents = _db.tblDocuments.Where(p => p.tblVisit.PatientId == patientId && p.Active == true).Select(
                    p =>
                    new Document
                    {
                        DocumentId = p.Id,
                        ChartId = p.ChartId,
                        Description = p.Description,
                        Name = p.Name,
                        ServerPath = p.ServerPath,
                        ServerName = p.ServerName,
                        DocumentDate = p.DocumentDate,
                        Active = true,
                        LastUpdated = p.LastUpdated,
                        Visit = new Visit{Date = p.tblVisit.Date.ToShortDateString(), VisitId = p.tblVisit.Id},
                        DocumentType = new DocumentType { TypeId = p.tlbDocumentType.Id, Type = p.tlbDocumentType.Type }
                    }).ToList();
            }
            return documents;
        }

        public List<Document> GetOrphanedDocuments()
        {
            List<Document> documents;
            using (_db)
            {
                documents = _db.tblDocuments.Where(p => p.TypeId == 99 && p.Active == true).Select(
                     p =>
                     new Document
                     {
                         DocumentId = p.Id,
                         ChartId = null,
                         Description = "",
                         Name = p.Name,
                         ServerPath = p.ServerPath,
                         ServerName = p.ServerName,
                         DocumentDate = null,
                         Active = true,
                         LastUpdated = p.LastUpdated,
                         Visit = new Visit { Date = DateTime.Now.ToShortDateString(), VisitId = 0 },
                         DocumentType = new DocumentType { TypeId = p.tlbDocumentType.Id, Type = p.tlbDocumentType.Type }
                     }).ToList();
            }
            return documents;
        }

        public void SaveDocument(Document document)
        {
            var d = new tblDocument();
            using (_db)
            {
                d = _db.tblDocuments.SingleOrDefault(x => x.Id == document.DocumentId);
                d.ChartId = document.ChartId;
                d.Description = document.Description;
                d.DocumentDate = document.DocumentDate;
                d.LastUpdated = DateTime.Now;
                d.Name = document.Name;
                d.VisitId = document.Visit.VisitId;
                d.TypeId = document.DocumentType.TypeId;
                d.ServerPath = document.ServerPath;
                _db.SubmitChanges();
            }
        }
    }
}
