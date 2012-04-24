using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMREC.Core.Domain.Models
{
    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
