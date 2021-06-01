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
        public async Task<ActionResult<Course>> New(Course course)
        {
            var courseToAdd = await _context.Courses.FindAsync(course.CourseId);
            if (courseToAdd != null)
            {
                return BadRequest("Course ID already exists. Please choose a new course");
            }
            
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(string id)
        {
            return await _context.Courses.FirstOrDefaultAsync(course => course.CourseId == id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            return await _context.Courses.ToListAsync();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Course>> Delete(string id)
        {
            var courseToDelete = await _context.Courses.FindAsync(id);
            if (courseToDelete == null) return BadRequest("Course doesn't exist.");
            _context.Courses.Remove(courseToDelete);
            await _context.SaveChangesAsync();
            return courseToDelete;
        }

        [HttpPut("update")]
        public async Task<ActionResult<Course>> Update(CourseUpdateDto courseUpdateDto)
        {
            var courseToUpdate = await _context.Courses.FindAsync(courseUpdateDto.CourseId);
            if (courseToUpdate == null) return BadRequest("Course doesn't exist");
            if (courseUpdateDto.CourseId != courseUpdateDto.NewPrimaryKey)
            {
                courseToUpdate.CourseId = courseUpdateDto.NewPrimaryKey;
                courseToUpdate.CourseName = courseUpdateDto.CourseName;
                courseToUpdate.CourseDescription = courseUpdateDto.CourseDescription;
                courseToUpdate.Credit = courseToUpdate.Credit;
            }
            else
            {
                courseToUpdate.CourseName = courseUpdateDto.CourseName;
                courseToUpdate.CourseDescription = courseUpdateDto.CourseDescription;
                courseToUpdate.Credit = courseUpdateDto.Credit;
            }

            await _context.SaveChangesAsync();
            return courseToUpdate;
        }
        
        // DISCUSSION BOARD
        [HttpPost("question/new")]
        public async Task<ActionResult<Question>> NewQuestion(PostQuestionDto postQuestionDto)
        {
            Question newQuestion = new Question()
            {
                Query = postQuestionDto.Query,
                CourseId = postQuestionDto.CourseId,
                StudentId = postQuestionDto.StudentId
            };
            _context.Questions.Add(newQuestion);

            await _context.SaveChangesAsync();
            return newQuestion;
        }

        [HttpGet("question/{id}")]
        public async Task<ActionResult<List<Question>>> GetQuestions(string id)
        {
            return await _context.Questions.Where(question => question.CourseId == id).ToListAsync();
        }

        [HttpPut("question/update")]
        public async Task<ActionResult<Question>> UpdateQuestion(UpdateQuestionDto updateQuestionDto)
        {
            Question questionToUpdate = await _context.Questions.FindAsync(updateQuestionDto.QuestionId);
            questionToUpdate.Query = updateQuestionDto.Query;
            await _context.SaveChangesAsync();
            return questionToUpdate;
        }

        [HttpDelete("question/delete/{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            Question questionToDelete = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(questionToDelete);
            await _context.SaveChangesAsync();
            return questionToDelete;
        }

        [HttpPost("question/reply/new")]
        public async Task<ActionResult<Reply>> NewReply(NewReplyDto newReplyDto)
        {
            Reply newReply = new Reply()
            {
                Answer = newReplyDto.Answer,
                QuestionId = newReplyDto.QuestionId,
                StudentId = newReplyDto.StudentId
            };

            _context.Replies.Add(newReply);
            await _context.SaveChangesAsync();

            return newReply;
        }

        [HttpGet("question/replies/{id}")]
        public async Task<ActionResult<List<Reply>>> GetReplies(int id)
        {
            return await _context.Replies.Where(x => x.QuestionId == id).ToListAsync();
        }

        [HttpDelete("question/reply/delete/{id}")]
        public async Task<ActionResult<Reply>> DeleteReply(int id)
        {
            Reply replyToDelete = await _context.Replies.FindAsync(id);
            _context.Replies.Remove(replyToDelete);
            await _context.SaveChangesAsync();
            return replyToDelete;
        }

        [HttpPut("question/reply/update")]
        public async Task<ActionResult<Reply>> UpdateReply(UpdateReplyDto updateReplyDto)
        {
            Reply replyToUpdate = await _context.Replies.FindAsync(updateReplyDto.ReplyId);
            replyToUpdate.Answer = updateReplyDto.Answer;
            await _context.SaveChangesAsync();
            return replyToUpdate;
        }
        
        // Give Feedback
        [HttpPost("givefeedback")]
        public async Task<ActionResult<Enrollment>> GiveFeedback(GiveFeedbackDto giveFeedbackDto)
        {
            Enrollment enrollmentToEdit = await _context.Enrollments.FindAsync(giveFeedbackDto.EnrollmentId);
            enrollmentToEdit.Marks = giveFeedbackDto.Marks;
            await _context.SaveChangesAsync();
            return enrollmentToEdit;
        }
    }
}