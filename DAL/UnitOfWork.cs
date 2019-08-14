using DAL.Data;
using DataContract.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {

        private bool disposed = false;
        private readonly TbcFinalExamContext _context;
        private GenericRepository<City> cityRepository;
        private GenericRepository<Gender> genderRepository;
        private GenericRepository<TelephoneType> telephoneTypeRepository;
        private GenericRepository<PhysicalPerson> physicalPersonRepository;
        private GenericRepository<PhysicalPersonConnection> physicalPersonConnectionRepository;
        private GenericRepository<PhysicalPersonConnectionType> physicalPersonConnectionTypeRepository;

        public UnitOfWork(TbcFinalExamContext context)
        {
            _context = context;
        }

        public GenericRepository<City> CityRepository
        {
            get
            {
                if (cityRepository == null)
                {
                    cityRepository = new GenericRepository<City>(_context);
                }
                return cityRepository;
            }
        }

        public GenericRepository<Gender> GenderRepository
        {
            get
            {
                if (genderRepository == null)
                {
                    genderRepository = new GenericRepository<Gender>(_context);
                }
                return genderRepository;
            }
        }

        public GenericRepository<TelephoneType> TelephoneTypeRepository
        {
            get
            {
                if (telephoneTypeRepository == null)
                {
                    telephoneTypeRepository = new GenericRepository<TelephoneType>(_context);
                }
                return telephoneTypeRepository;
            }
        }

        public GenericRepository<PhysicalPerson> PhysicalPersonRepository
        {
            get
            {
                if (physicalPersonRepository == null)
                {
                    physicalPersonRepository = new GenericRepository<PhysicalPerson>(_context);
                }
                return physicalPersonRepository;
            }
        }

        public GenericRepository<PhysicalPersonConnection> PhysicalPersonConnectionRepository
        {
            get
            {
                if (physicalPersonConnectionRepository == null)
                {
                    physicalPersonConnectionRepository = new GenericRepository<PhysicalPersonConnection>(_context);
                }
                return physicalPersonConnectionRepository;
            }
        }

        public GenericRepository<PhysicalPersonConnectionType> PhysicalPersonConnectionTypeRepository
        {
            get
            {
                if (physicalPersonConnectionTypeRepository == null)
                {
                    physicalPersonConnectionTypeRepository = new GenericRepository<PhysicalPersonConnectionType>(_context);
                }
                return physicalPersonConnectionTypeRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
