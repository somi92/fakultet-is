using Fakultet_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakultet_IS.DAL
{
    public class UnitOfWork
    {
        private FakultetEntities context = new FakultetEntities();
        private IFakultetRepository<Students> studentsRepository;
        private IFakultetRepository<Ispits> ispitsRepository;
        private IFakultetRepository<Prijavas> prijavasRepository;

        public IFakultetRepository<Students> StudentsRepository
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

        public IFakultetRepository<Ispits> IspitsRepository
        {
            get
            {
                if(ispitsRepository == null)
                {
                    this.ispitsRepository = new FakultetRepository<Ispits>(context);
                }
                return ispitsRepository;
            }

            set
            {
                this.ispitsRepository = value;
            }
        }

        public IFakultetRepository<Prijavas> PrijavasRepository
        {
            get
            {
                if (prijavasRepository == null)
                {
                    this.prijavasRepository = new FakultetRepository<Prijavas>(context);
                }
                return prijavasRepository;
            }

            set
            {
                this.prijavasRepository = value;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
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