using Microsoft.EntityFrameworkCore;
using RESTapp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RESTapp.Data
{
    public class CourseDAL : ICourse
    {
        private ApplicationDbContext _db;

        public CourseDAL(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Delete(string id)
        {
            var result = await GetById(id);
            if (result == null) throw new Exception("Data tidak ditemukan!");

            try
            {
                // Delete command 
                _db.Courses.Remove(result);
                //Commit to Database
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {

                throw new Exception($"Error : {dbEx.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await (from c in _db.Courses
                                 orderby c.Title ascending
                                 select c).ToListAsync();
            return results;
        }

        public async Task<Course> GetById(string id)
        {
            var result = await _db.Courses.Where(s => s.CourseID == Convert.ToInt32(id)).SingleOrDefaultAsync<Course>();
            if (result != null)
                return result;
            else
                throw new Exception("Data tidak ditemukan !");
        }

        public async Task<IEnumerable<Course>> GetByName(string title)
        {
            var results = await(from a in _db.Courses
                                where a.Title.Contains(title.ToLower()) 
                                orderby a.Title ascending
                                select a).ToListAsync();

            if (results == null) throw new Exception($"{title} Tidak ditemukan");

            return results;
        }

        public async Task<Course> Insert(Course obj)
        {
            try
            {
                _db.Courses.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public async Task<Course> Update(string id, Course obj)
        {
            try
            {
                var result = await GetById(id);
                result.AuthorID = obj.AuthorID;
                result.Title = obj.Title;
                result.Description = obj.Description;
                await _db.SaveChangesAsync();
                obj.CourseID = Convert.ToInt32(id);
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesByAuthor(int id)
        {
          /*  var result = await _db.Courses.Where(a => a.AuthorID == Convert.ToInt32(id))
               // .SingleOrDefaultAsync<Course>();
                .ToListAsync();
            if (result != null)
                return result;
            else
                throw new Exception("Data tidak ditemukan !");
          */


            var results = await (from a in _db.Courses
                                 where a.AuthorID == (Convert.ToInt32(id))
                                 orderby a.Title ascending
                                 select a).ToListAsync();

            if (results == null) throw new Exception($"Courses pada Id {id} Tidak ditemukan");

            return results;

        }

   
    }
}
