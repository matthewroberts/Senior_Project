using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Services;
using EMREC.Core.Infrastructure.Repositories;

namespace EMREC.Core.DependencyInjection
{
    public class DI
    {
        public static IPhysicianRepository CreatePhysicanRepository()
        {
            return new PhysicianRepository(WebConfig.EMRECConnectionString);
        }

        public static IPatientRepository CreatePatientRepository()
        {
            return new PatientRepository(WebConfig.EMRECConnectionString);
        }

        public static IDocumentRepository CreateDocumentRepository()
        {
            return new DocumentRepository(WebConfig.EMRECConnectionString);
        }

        public static IGeneralRepository CreateGeneralrepository()
        {
            return new GeneralRepository(WebConfig.EMRECConnectionString);
        }

        public static IDocumentService CreateDocumentService()
        {
            return new DocumentService();
        }

        public static ISearchService CreateSearchService()
        {
            return new SearchService();
        }

        public static IQCService CreateQCService()
        {
            return new QCService();
        }
    }
}
