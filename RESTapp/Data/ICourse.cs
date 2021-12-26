using RESTapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTapp.Data
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetCoursesByAuthor(int id);
        Task<IEnumerable<Course>> GetByName(string title);

       

    }
}
