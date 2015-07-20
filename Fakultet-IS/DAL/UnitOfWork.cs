using Fakultet_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakultet_IS.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private FakultetEntities context = new FakultetEntities();
        private IFakultetRepository<Students> studentsRepository;

        public IFakultetRepository<Students> FakultetRepository
        {
            get
            {
                if (studentsRepository == null)
                {
                    this.studentsRepository = new FakultetRepository<Students>(context);
                }
                return studentsRepository;
            }

            set
            {
                this.studentsRepository = value;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}