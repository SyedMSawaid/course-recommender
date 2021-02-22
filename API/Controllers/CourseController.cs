using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entity;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourseController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpPost("new")]
        public async Task<ActionResult> New(Course course)
        {
            var courseToAdd = await _context.Courses.FindAsync(course.CourseId);
            if (courseToAdd != null)
            {
                return BadRequest("Course ID already exists. Please choose a new course");
            }
            
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return Ok("Course Saved");
        }

        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
            return await _context.Courses.ToListAsync();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var courseToDelete = await _context.Courses.FindAsync(id);
            if (courseToDelete == null) return BadRequest("Course doesn't exist.");
            _context.Courses.Remove(courseToDelete);
            await _context.SaveChangesAsync();
            return Ok("Course Successfully Deleted");
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var courseToUpdate = await _context.Courses.FindAsync(courseUpdateDto.CourseId);
            if (courseToUpdate == null) return BadRequest("Course doesn't exist");
            if (courseUpdateDto.CourseId != courseUpdateDto.NewPrimaryKey)
            {
                // Giving new primary key
                _context.Courses.Remove(courseToUpdate);
                Course newCourse = new Course()
                {
                    CourseId = courseUpdateDto.NewPrimaryKey,
                    CourseName = courseUpdateDto.CourseName,
                    CourseDescription = courseUpdateDto.CourseDescription
                };
                await _context.Courses.AddAsync(newCourse);
            }
            else
            {
                courseToUpdate.CourseName = courseUpdateDto.CourseName;
                courseToUpdate.CourseDescription = courseUpdateDto.CourseDescription;
            }
            await _context.SaveChangesAsync();
            return Ok("Course updated");
        }
    }
}