using Fakultet_IS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakultet_IS.DAL
{
    public interface IStudentsRepository : IDisposable
    {
        IEnumerable<Students> GetStudents();
        Students GetStudent(int id);
        void InsertStudent(Students student);
        void DeleteStudent(int id);
        void UpdateStudent(Students student);
        void Save();
    }
}
