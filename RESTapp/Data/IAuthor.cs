using RESTapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTapp.Data
{
    public interface IAuthor : ICrud<Author>
    {
        
        Task<IEnumerable<Author>> GetByName(string name);
    }
}
