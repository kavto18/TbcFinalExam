using DAL.Data;
using DAL.Interface;
using DataContract.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    public class PhysicalPersonRepository : IPhysicalPersonRepository
    {
        private bool _disposed;
        private readonly TbcFinalExamContext _context;

        public PhysicalPersonRepository(TbcFinalExamContext context)
        {
            _context = context;
        }

        public IQueryable<PhysicalPerson> GetPhysicalPeople(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return _context.PhysicalPersons.
                    Where(x => x.Name.Contains(searchString) || x.LastName.Contains(searchString) || x.Id.ToString().Contains(searchString) || x.PersonalId.Contains(searchString) || x.TelephoneNumber.Contains(searchString)).
                    Include(p => p.City).Include(p => p.Gender).Include(p => p.TelephoneType);
            }
            return _context.PhysicalPersons.Include(p => p.City).Include(p => p.Gender).Include(p => p.TelephoneType);
        }

        public async Task<PhysicalPerson> GetPhysicalPersonDetailById(long id)
        {
            return await _context.PhysicalPersons
                .Include(p => p.City)
                .Include(p => p.Gender)
                .Include(p => p.TelephoneType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
