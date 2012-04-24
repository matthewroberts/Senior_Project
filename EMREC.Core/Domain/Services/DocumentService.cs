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
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService()
        {
            _documentRepository = DI.CreateDocumentRepository();
        }

        public List<Document> GetDocumentsByPatientId(int patientId)
        {
            return _documentRepository.GetDocumentsByPatientId(patientId);
        }

        public List<Document> GetOrphanedDocuments()
        {
            return _documentRepository.GetOrphanedDocuments();
        }

        public Document GetDocumentById(int id)
        {
            return _documentRepository.GetDocumentById(id);
        }
    }
}
