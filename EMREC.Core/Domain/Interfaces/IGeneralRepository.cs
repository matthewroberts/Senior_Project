using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Core.Domain.Interfaces
{
    public interface IGeneralRepository : IDisposable
    {
        IList<DocumentType> GetDocumentTypes();
        IList<Visit> GetVisitDatesByPatient(int patientId);
        tblConfigPath FileStoreDirectory();
        tblConfigPath ServerDriveLetter();
    }
}
