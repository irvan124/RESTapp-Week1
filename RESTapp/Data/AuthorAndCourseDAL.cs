using System.Threading.Tasks;

namespace RESTapp.Data
{
    public class AuthorAndCourseDAL : ICustomAdd<IAuthorAndCourse>
    {
        public Task<IAuthorAndCourse> Insert(IAuthorAndCourse obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
