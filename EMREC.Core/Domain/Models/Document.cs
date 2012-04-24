using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Interfaces;

namespace EMREC.Core.Domain.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public Visit Visit { get; set; }
        public string ChartId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServerPath { get; set; }
        public string ServerName { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool Active { get; set; }
    }
}
