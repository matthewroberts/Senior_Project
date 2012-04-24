using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Models;

namespace EMREC.Core.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        Document GetDocumentById(int documentId);
        List<Document> GetDocumentsByPatientId(int patientId);
        List<Document> GetOrphanedDocuments();
        void SaveDocument(Document document);
    }
}
