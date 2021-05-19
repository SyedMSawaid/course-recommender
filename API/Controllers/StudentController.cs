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
        [HttpPost("new")]
        public async Task<ActionResult> New(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok("Student Saved");
        }

        [HttpGet("")]
        public async Task<ActionResult> Get()
        {
            var role = await _context.Roles.Where(role => role.Name == "Student").SingleOrDefaultAsync();
            return Ok(_context.Users.Where(user => user.UserRoles.Any(r => r.RoleId == role.Id)));
        }

        [HttpGet("{id}")]
        public async Task<AppUser> Get(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var studentToDelete = await _context.Users.FindAsync(id);
            if (studentToDelete == null) return BadRequest("Student doesn't exist.");
            _context.Users.Remove(studentToDelete);
            await _context.SaveChangesAsync();
            return Ok("Student Successfully Deleted");
        }

        [HttpPut("update")]
        public async Task<ActionResult<NewStudentDto>> Update(NewStudentDto newStudentDto)
        {
            var studentToUpdate = await _context.Users.FindAsync(newStudentDto.Id);
            if (studentToUpdate == null)
                return BadRequest("Student doesn't exists");

            studentToUpdate.Email = newStudentDto.Email;
            studentToUpdate.FullName = newStudentDto.FullName;

            await _context.SaveChangesAsync();
            return Ok(studentToUpdate);
        }

        // Enrollment methods
        [HttpGet("list-of-courses/{studentId}")]
        public async Task<List<Course>> ListOfCourses(int studentId)
        {
            List<Course> coursesList = await _context.Courses.ToListAsync();
            List<Course> enrollments =
                await _context.Enrollments.Where(x => x.StudentId == studentId).Select(p => new Course()
                {
                    CourseId = p.CourseId
                }).ToListAsync();
            foreach (var enrollment in enrollments)
            {
                coursesList.RemoveAll(x => x.CourseId == enrollment.CourseId);
            }
            
            return coursesList;
        }
        
        [HttpPost("enroll")]
        public async Task<ActionResult> Enroll(EnrollDto enrollDto)
        {
            if (_context.Enrollments.Any(enrollment =>
                enrollment.CourseId == enrollDto.CourseId && enrollment.StudentId == enrollDto.StudentId)) return BadRequest("Enrollment already exists");
            
            _context.Enrollments.Add(new Enrollment()
            {
                StudentId = enrollDto.StudentId,
                CourseId = enrollDto.CourseId,
                Marks = enrollDto.Marks
            });
            await _context.SaveChangesAsync();
            return Ok("Student successfully enrolled");
        }

        [HttpGet("enrollment/{id}")]
        public async Task<Enrollment> GetSingleEnrollment(int id)
        {
            return await _context.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentId == id);
        }

        [HttpGet("enrollments/{id}")]
        public async Task<ActionResult> GetEnrollment(int id)
        {
            return Ok(await _context.Enrollments.Where(x => x.StudentId == id).ToListAsync());
        }

        [HttpDelete("enrollment/delete/{id}")]
        public async Task<ActionResult> DeleteEnrollment(int id)
        {
            Enrollment enrollmentToDelete = await _context.Enrollments.FindAsync(id);
            _context.Enrollments.Remove(enrollmentToDelete);
            await _context.SaveChangesAsync();
            return Ok(enrollmentToDelete);
        }

        [HttpPut("enrollment/update")]
        public async Task<ActionResult> UpdateEnrollment(GiveFeedbackDto giveFeedbackDto)
        {
            Enrollment enrollmentToEdit = await _context.Enrollments.FindAsync(giveFeedbackDto.EnrollmentId);
            enrollmentToEdit.Marks = giveFeedbackDto.Marks;
            await _context.SaveChangesAsync();
            return Ok(enrollmentToEdit);
        }

        // Questions and Replies
        [HttpGet("question/{id}")]
        public async Task<ActionResult> Question(int id)
        {
            return Ok(await _context.Questions.Where(x => x.StudentId == id).ToListAsync());
        }

        [HttpGet("replies/{id}")]
        public async Task<ActionResult> Replies(int id)
        {
            return Ok(await _context.Replies.Where(x => x.StudentId == id).ToListAsync());
        }
    }
}
