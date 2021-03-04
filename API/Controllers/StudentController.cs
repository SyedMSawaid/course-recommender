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

        [HttpGet("")]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Students.ToListAsync());
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
        
        // Recommend Course
        // [HttpGet("recommendcourse/{id}")]
        // public async Task<ActionResult> RecommendCourse(int id)
        // {
        //     
        // }
        
        // TODO: Implement Recommend Courses API.
    }
}
