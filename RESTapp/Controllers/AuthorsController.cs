using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTapp.Data;
using RESTapp.Dtos;
using RESTapp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTapp.Controllers
{
    // Set to  authorize so it can be  access this resource if doesnt have matched token
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthor _author;
        private ICourse _course;
        private IMapper _mapper;

        public AuthorsController(IAuthor author, ICourse course, IMapper mapper)
        {
            _author = author;
            _course = course;
            _mapper = mapper;
           
        }
        // GET: api/<AuthorsController>
        [HttpGet]
        //[Authorize]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorDto>> Get()
        {
            var authors = await _author.GetAll();
            var dtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(dtos);
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            var result = await _author.GetById(id.ToString());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(result));
        }


       

        [HttpGet("byName")]
        public async Task<ActionResult<AuthorDto>> GetByName(string name)
        {
            var authors = await _author.GetByName(name);
            if (authors == null)
            {
                return NotFound();
            }
            var dtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(dtos);
        }

        // Access based on Rule

        [Authorize(Roles ="Admin")]
        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Post([FromBody] AuthorModifyDto authorModifyDto)
        {
            try
            {
                var author = _mapper.Map<Models.Author>(authorModifyDto);
                var result = await _author.Insert(author);

                var authorReturn = _mapper.Map<Dtos.AuthorDto>(result);
                return Ok(authorReturn);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // POST api/<AuthorsController>
        [HttpPost("InsertAuthorAndCourse")]
        public async Task<ActionResult<InsertAuthorAndCourseDto>> PostAuthorAndCourse([FromBody] InsertAuthorAndCourseDto insertAuthorAndCourseDto)
        {
            try
            {
                var author = new Author()
                {
                    FirstName = insertAuthorAndCourseDto.FirstName,
                    LastName = insertAuthorAndCourseDto.LastName,
                    DateOfBirth = insertAuthorAndCourseDto.DateOfBirth,
                    MainCategory = insertAuthorAndCourseDto.MainCategory
                };


                var result1 = await _author.Insert(author);
                var course = new Course()
                {
                    Title = insertAuthorAndCourseDto.Title,
                    Description = insertAuthorAndCourseDto.Description,
                    AuthorID = result1.AuthorID,
                };
                var result2 = await _course.Insert(course);

                return Ok("Test");
             
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

       

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> Put(int id, [FromBody] AuthorModifyDto authorModifyDto)
        {
            try
            {
                var author = _mapper.Map<Models.Author>(authorModifyDto);
                var result = await _author.Update(id.ToString(), author);

                var authorReturn = _mapper.Map<Dtos.AuthorDto>(result);
                return Ok(authorReturn);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _author.Delete(id.ToString());
                return Ok($"Data Author {id} Berhasil di delete");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
