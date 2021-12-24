using RESTapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTapp.Data
{
    public interface ICustomAdd<T>
    {
        Task<T> Insert(T obj);
    }
}
