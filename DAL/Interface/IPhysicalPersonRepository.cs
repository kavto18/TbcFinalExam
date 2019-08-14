using DataContract.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IPhysicalPersonRepository : IDisposable
    {
        IQueryable<PhysicalPerson> GetPhysicalPeople(string searchString);

        Task<PhysicalPerson> GetPhysicalPersonDetailById(long id);

        new void Dispose();
    }
}
