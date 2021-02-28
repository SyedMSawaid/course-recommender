using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entity;
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
        
        // COURSES CRUD
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
        
        // DISCUSSION BOARD
        [HttpPost("question/new")]
        public async Task<ActionResult> NewQuestion(PostQuestionDto postQuestionDto)
        {
            if (await _context.Students.FindAsync(postQuestionDto.StudentId) == null)
                return BadRequest("Student Doesn't exist");
            if (await _context.Courses.FindAsync(postQuestionDto.CourseId) == null)
                return BadRequest("Course doesn't exist");

            Question newQuestion = new Question()
            {
                Query = postQuestionDto.Query,
                CourseId = postQuestionDto.CourseId,
                StudentId = postQuestionDto.StudentId
            };
            _context.Questions.Add(newQuestion);

            await _context.SaveChangesAsync();
            return Ok(newQuestion);
        }

        [HttpGet("question/{id}")]
        public async Task<ActionResult> GetQuestions(string courseId)
        {
            return Ok(await _context.Questions.Where(questionObject => questionObject.CourseId == courseId).ToListAsync());
        }

        [HttpPut("question/update")]
        public async Task<ActionResult> UpdateQuestion(UpdateQuestionDto updateQuestionDto)
        {
            Question questionToUpdate = await _context.Questions.FindAsync(updateQuestionDto.QuestionId);
            questionToUpdate.Query = updateQuestionDto.Query;
            await _context.SaveChangesAsync();
            return Ok(questionToUpdate);
        }

        [HttpDelete("question/delete/{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            Question questionToDelete = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(questionToDelete);
            await _context.SaveChangesAsync();
            return Ok(questionToDelete);
        }

        [HttpPost("question/reply/new")]
        public async Task<ActionResult> NewReply(NewReplyDto newReplyDto)
        {
            Reply newReply = new Reply()
            {
                Answer = newReplyDto.Answer,
                QuestionId = newReplyDto.QuestionId,
                StudentId = newReplyDto.StudentId
            };

            _context.Replies.Add(newReply);
            await _context.SaveChangesAsync();

            return Ok(newReply);
        }

        [HttpGet("question/replies/{id}")]
        public async Task<ActionResult> GetReplies(int questionId)
        {
            return Ok(await _context.Replies.Where(x => x.QuestionId == questionId).ToListAsync());
        }

        [HttpDelete("question/reply/delete/{id}")]
        public async Task<ActionResult> DeleteReply(int id)
        {
            Reply replyToDelete = await _context.Replies.FindAsync(id);
            _context.Replies.Remove(replyToDelete);
            await _context.SaveChangesAsync();
            return Ok(replyToDelete);
        }

        [HttpPut("question/reply/update")]
        public async Task<ActionResult> UpdateReply(UpdateReplyDto updateReplyDto)
        {
            Reply replyToUpdate = await _context.Replies.FindAsync(updateReplyDto.ReplyId);
            replyToUpdate.Answer = updateReplyDto.Answer;
            await _context.SaveChangesAsync();
            return Ok(replyToUpdate);
        }
        
        // Give Feedback
        [HttpPost("givefeedback")]
        public async Task<ActionResult> GiveFeedback(GiveFeedbackDto giveFeedbackDto)
        {
            Enrollment enrollmentToEdit = await _context.Enrollments.FindAsync(giveFeedbackDto.EnrollmentId);
            enrollmentToEdit.Marks = giveFeedbackDto.Marks;
            await _context.SaveChangesAsync();
            return Ok(enrollmentToEdit);
        }
    }
}