using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Models;

namespace EMREC.Core.Domain.Interfaces
{
    public interface IPhysicianRepository
    {
        IList<Physician> GetAllPhysicians();
    }
}
