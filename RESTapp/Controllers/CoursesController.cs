using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTapp.Data;
using RESTapp.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private ICourse _course;
        private IMapper _mapper;

        public CoursesController(ICourse course, IMapper mapper)
        {
            _course = course;
            _mapper = mapper;
        }
        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<ActionResult<CourseDto>> Get()
        {
            var courses = await _course.GetAll();
            var dtos = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(dtos);
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> Get(int id)
        {
            var result = await _course.GetById(id.ToString());
            if (result == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(result));
        }

        [HttpGet("byName")]
        public async Task<ActionResult<CourseDto>> GetByName(string name)
        {
            var courses = await _course.GetByName(name);
            if (courses == null)
            {
                return NotFound();
            }
            var dtos = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return Ok(dtos);
        }



        // POST api/<CoursesController>
        [HttpPost]
        public async Task<ActionResult<CourseDto>> Post([FromBody] CourseModifyDto courseModifyDto)
        {
            try
            {
                var course = _mapper.Map<Models.Course>(courseModifyDto);
                var result = await _course.Insert(course);

                var courseReturn = _mapper.Map<Dtos.CourseDto>(result);
                return Ok(courseReturn);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> Put(int id, [FromBody] CourseModifyDto courseModifyDto)
        {
            try
            {
                var course = _mapper.Map<Models.Course>(courseModifyDto);
                var result = await _course.Update(id.ToString(), course);

                var courseReturn = _mapper.Map<Dtos.CourseDto>(result);
                return Ok(courseReturn);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _course.Delete(id.ToString());
                return Ok($"Data Course {id} Berhasil di delete");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("coursesByAuthor/{id}")]
        public async Task<ActionResult<CourseDto>> GetCoursesByAuthor(int id)
        {
            var result = await _course.GetCoursesByAuthor(id);
            if (result == null)
            {
                return NotFound();
            }
            var dtos = _mapper.Map<IEnumerable<CourseDto>>(result);
            return Ok(dtos);
        }
    }
}
