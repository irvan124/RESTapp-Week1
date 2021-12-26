using Microsoft.EntityFrameworkCore;
using RESTapp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RESTapp.Data
{
    public class AuthorDAL : IAuthor
    {
        private ApplicationDbContext _db;

        public AuthorDAL(ApplicationDbContext db)
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
                _db.Authors.Remove(result);
                //Commit to Database
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {

                throw new Exception($"Error : {dbEx.Message}");
            }
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var results = await(from a in _db.Authors
                                orderby a.FirstName ascending
                                select a).ToListAsync();
            return results;
        }

        public async Task<Author> GetById(string id)
        {
            var result = await _db.Authors.Where(s => s.AuthorID == Convert.ToInt32(id)).SingleOrDefaultAsync<Author>();
            if (result != null)
                return result;
            else
                throw new Exception("Data tidak ditemukan !");
        }

        public async Task<IEnumerable<Author>> GetByName(string name)
        {
            var results = await(from a in _db.Authors
                                where a.FirstName.Contains(name.ToLower()) || a.LastName.Contains(name.ToLower())
                                orderby a.FirstName ascending
                                select a).ToListAsync();

            if (results == null) throw new Exception($"{name} Tidak ditemukan");

            return results;
        }

      

        public async Task<Author> Insert(Author obj)
        {
            try
            {
                _db.Authors.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }
        public async Task<Author> Update(string id, Author obj)
        {
            try
            {
                var result = await GetById(id);
                result.FirstName = obj.FirstName;
                result.LastName = obj.LastName;
                result.DateOfBirth = obj.DateOfBirth;
                result.MainCategory = obj.MainCategory;
                await _db.SaveChangesAsync();
                obj.AuthorID = Convert.ToInt32(id);
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<Author> UpdateCourse(string id, Author obj)
        {
            try
            {
                var result = await GetById(id);
                result.FirstName = obj.FirstName;
                result.LastName = obj.LastName;
                result.DateOfBirth = obj.DateOfBirth;
                result.MainCategory = obj.MainCategory;
                await _db.SaveChangesAsync();
                obj.AuthorID = Convert.ToInt32(id);
                return obj;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }


    }
}
