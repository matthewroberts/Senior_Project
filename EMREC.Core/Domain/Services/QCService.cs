using System;
using System.IO;
using EMREC.Core.DependencyInjection;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;

namespace EMREC.Core.Domain.Services
{
    public class QCService : IQCService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentRepository _refreshDocumentRepository;
        private readonly IGeneralRepository _generalRepository;
        private readonly IGeneralRepository _refreshGeneralRepository;
        
        public QCService()
        {
            _documentRepository = DI.CreateDocumentRepository();
            _refreshDocumentRepository = DI.CreateDocumentRepository();
            _generalRepository = DI.CreateGeneralrepository();
            _refreshGeneralRepository = DI.CreateGeneralrepository();

        }

        public void UpdateOrphanDocument(int documentId, Visit visit, string chartId, DocumentType type, string docDescription)
        {
            var document = _documentRepository.GetDocumentById(documentId);
            document.ChartId = chartId;
            document.Visit = visit;
            document.DocumentType = type;
            document.DocumentDate = Convert.ToDateTime(visit.Date);
            document.Description = docDescription;
            var updatedDocument = MoveFile(document);
            _refreshDocumentRepository.SaveDocument(updatedDocument);
        }

        private string GetFileStore()
        {
                return _generalRepository.FileStoreDirectory().value;
        }

        private string GetServerDriveLetter()
        {
                return _refreshGeneralRepository.ServerDriveLetter().value;
        }

        private Document MoveFile(Document document)
        {
            var date = Convert.ToDateTime(document.DocumentDate);
            var serverPath = GetFileStore().Replace("\\","/") + date.Year + "/" + date.Month + "/" + date.Day + "/" + document.DocumentType.Type;
            var webPath = "/FileStore/" + date.Year + "/" + date.Month + "/" + date.Day + "/" +
                          document.DocumentType.Type;
            var fileName = document.DocumentId + "_" + document.DocumentType.Type + Path.GetExtension(document.Name);

            var destFile = serverPath + "/" + fileName;
            var sourceFile = GetServerDriveLetter() + document.ServerPath + "/" + document.Name;

            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            File.Move(sourceFile, destFile);

            document.Name = fileName;
            document.ServerPath = webPath;

            return document;
        }
    }
}
 