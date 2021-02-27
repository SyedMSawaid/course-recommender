using API.Data;
using API.DTOs;
using API.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        // Student CRUD
        public async Task<ActionResult> New(Student student)
        {
            var studentToAdd = await _context.Courses.FindAsync(student.Id);
            if (studentToAdd != null)
            {
                return BadRequest("Student ID already exists. Please choose a new student");
            }
            
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok("Student Saved");
        }
        
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var studentToDelete = await _context.Students.FindAsync(id);
            if (studentToDelete == null) return BadRequest("Student doesn't exist.");
            _context.Students.Remove(studentToDelete);
            await _context.SaveChangesAsync();
            return Ok("Student Successfully Deleted");
        }
        
        [HttpPost("update")]
        public async Task<ActionResult<NewStudentDto>> Update(NewStudentDto newStudentDto)
        {
            var studentToUpdate = await _context.Students.FindAsync(newStudentDto.Id);
            if (studentToUpdate == null)
                return BadRequest("Student doesn't exists");

            studentToUpdate.Address = newStudentDto.Address;
            studentToUpdate.Contact = newStudentDto.Contact;

            await _context.SaveChangesAsync();
            return Ok(studentToUpdate);
        }
        
        // Enrollment methods
        [HttpPost("enroll")]
        public async Task<ActionResult> Enroll(EnrollDto enrollDto)
        {
            if (await _context.Students.FindAsync(enrollDto.StudentId) == null)
                return BadRequest("Student Doesn't exist");
            if (await _context.Courses.FindAsync(enrollDto.CourseId) == null) return BadRequest("Course doesn't exist");

            _context.Enrollments.Add(new Enrollment()
            {
                StudentId = enrollDto.StudentId,
                CourseId = enrollDto.CourseId
            });
            await _context.SaveChangesAsync();
            return Ok("Student successfully enrolled");
        }
        
        // TODO: List of questions a student participated in.
    }
}
