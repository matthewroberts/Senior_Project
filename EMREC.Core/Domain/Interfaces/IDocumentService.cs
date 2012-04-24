using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Models;

namespace EMREC.Core.Domain.Interfaces
{
    public interface IDocumentService
    {
        List<Document> GetDocumentsByPatientId(int patientId);
        List<Document> GetOrphanedDocuments();
        Document GetDocumentById(int id);
    }
}
