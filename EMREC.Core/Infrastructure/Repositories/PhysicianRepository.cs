using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Core.Infrastructure.Repositories
{
    public class PhysicianRepository : IPhysicianRepository
    {
        private readonly EMRECDataContext _db;
        public PhysicianRepository(string connstring)
        {
            _db = new EMRECDataContext(connstring);
        }


        public IList<Physician> GetAllPhysicians()
        {
            return _db.tblPhysicians.Select(
                p => new Physician {PrescriberId = p.PrescriberID, FirstName = p.FirstName, LastName = p.LastName, FullName = p.LastName + ", " + p.FirstName})
                .ToList();
        }
    }
}
