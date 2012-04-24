using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Interfaces;

namespace EMREC.Core.Domain.Models
{
    public class Physician : Person
    {
        public int PrescriberId { get; set; }
    }
}
