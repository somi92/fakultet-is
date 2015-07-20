using Fakultet_IS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fakultet_IS.DAL
{
    public class StudentsRepository : IStudentsRepository, IDisposable
    {
        private FakultetEntities context;

        public StudentsRepository(FakultetEntities context)
        {
            this.context = context;
        }

        public IEnumerable<Students> GetStudents()
        {
            return context.Students.ToList();
        }

        public Students GetStudent(int id)
        {
            return context.Students.Find(id);
        }

        public void InsertStudent(Students student)
        {
            context.Students.Add(student);
        }

        public void DeleteStudent(int id)
        {
            Students student = context.Students.Find(id);
            context.Students.Remove(student);
        }

        public void UpdateStudent(Students student)
        {
            context.Entry(student).State = EntityState.Modified; 
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
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