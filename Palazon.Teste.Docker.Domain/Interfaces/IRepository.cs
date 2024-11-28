using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palazon.Teste.Docker.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Update(T entity);
        bool Delete(int id);
    }
}
