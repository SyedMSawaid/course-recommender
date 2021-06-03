using API.Data;
using API.DTOs;
using API.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using CourseRecommendationSystemML.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public StudentController(DataContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        // Student CRUD
        [HttpPost("new")]
        public async Task<ActionResult<UserDto>> New(AppUser student)
        {
            RegisterDto registerDto = new RegisterDto()
            {
                Email = student.Email,
                FullName = student.FullName,
                Password = "Pa$$w0rd",
                Username = student.UserName
            };
            
            if (await UserExists(registerDto.Username)) return BadRequest("User Already Exist");
            if (await _userManager.Users.FirstOrDefaultAsync(x => x.Email == registerDto.Email) != null)
                return BadRequest("Use new Email");
            
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Email = registerDto.Email,
                FullName = registerDto.FullName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result);

            var roleResult = await _userManager.AddToRoleAsync(user, "Student");
            if (!roleResult.Succeeded) return BadRequest(result);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }
        
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpGet("")]
        public async Task<ActionResult<List<AppUser>>> Get()
        {
            var role = await _context.Roles.Where(role => role.Name == "Student").SingleOrDefaultAsync();
            return await _context.Users.Where(user => user.UserRoles.Any(r => r.RoleId == role.Id)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> Get(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<AppUser>> Delete(int id)
        {
            var studentToDelete = await _context.Users.FindAsync(id);
            if (studentToDelete == null) return BadRequest("Student doesn't exist.");
            _context.Users.Remove(studentToDelete);
            await _context.SaveChangesAsync();
            return studentToDelete;
        }

        [HttpPut("update")]
        public async Task<ActionResult<AppUser>> Update(NewStudentDto newStudentDto)
        {
            var studentToUpdate = await _context.Users.FindAsync(newStudentDto.Id);
            if (studentToUpdate == null)
                return BadRequest("Student doesn't exists");

            studentToUpdate.Email = newStudentDto.Email;
            studentToUpdate.FullName = newStudentDto.FullName;

            await _context.SaveChangesAsync();
            return studentToUpdate;
        }

        // Enrollment methods
        // Get list of courses where student isn't enrolled
        [HttpGet("list-of-courses/{studentId}")]
        public async Task<ActionResult<List<Course>>> ListOfCourses(int studentId)
        {
            if (await _context.Users.FirstOrDefaultAsync(x => x.Id == studentId) == null)
                return NotFound("Student doesn't exist");
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
        
        // Courses with no marks
        [HttpGet("dashboard/{studentId}")]
        public async Task<ActionResult<List<Course>>> Dashboard(int studentId)
        {
            List<Course> coursesList = new List<Course>();
            List<Course> enrollments =
                await _context.Enrollments.Where(x => x.StudentId == studentId && x.Marks == 0).Select(p => new Course()
                {
                    CourseId = p.CourseId
                }).ToListAsync();
                
            foreach (var enrollment in enrollments)
            {
                coursesList.Add(await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == enrollment.CourseId));
            }
            
            return coursesList;
        }
        
        [HttpPost("enroll")]
        public async Task<ActionResult<Enrollment>> Enroll(EnrollDto enrollDto)
        {
            if (_context.Enrollments.Any(enrollment =>
                enrollment.CourseId == enrollDto.CourseId && enrollment.StudentId == enrollDto.StudentId)) return BadRequest("Enrollment already exists");
            
            Enrollment enrollment = new Enrollment()
            {
                StudentId = enrollDto.StudentId,
                CourseId = enrollDto.CourseId,
                Marks = enrollDto.Marks
            };
            
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        [HttpGet("enrollment/{id}")]
        public async Task<ActionResult<Enrollment>> GetSingleEnrollment(int id)
        {
            return await _context.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentId == id);
        }

        [HttpGet("enrollments/{id}")]
        public async Task<ActionResult<List<Enrollment>>> GetEnrollment(int id)
        {
            return await _context.Enrollments.Where(x => x.StudentId == id).ToListAsync();
        }

        [HttpDelete("enrollment/delete/{id}")]
        public async Task<ActionResult<Enrollment>> DeleteEnrollment(int id)
        {
            Enrollment enrollmentToDelete = await _context.Enrollments.FindAsync(id);
            if (enrollmentToDelete == null) return NotFound("Enrollment doesn't exist");
            _context.Enrollments.Remove(enrollmentToDelete);
            await _context.SaveChangesAsync();
            return enrollmentToDelete;
        }

        [HttpPut("enrollment/update")]
        public async Task<ActionResult<List<Enrollment>>> UpdateEnrollment(List<EnrollDto> enrollDto)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            foreach (var dto in enrollDto)
            {
                Enrollment enrollmentToUpdate =
                    await _context.Enrollments.FirstOrDefaultAsync(x =>
                        x.CourseId == dto.CourseId && x.StudentId == dto.StudentId);
                
                enrollmentToUpdate.Marks = dto.Marks;
                
                enrollments.Add(enrollmentToUpdate);
            }

            await _context.SaveChangesAsync();
            return enrollments;
        }
        
        [HttpPut("enrollment/singleupdate")]
        public async Task<ActionResult<Enrollment>> UpdateSingleEnrollment(EnrollUpdateDto enrollDto)
        {
            Enrollment enrollmentToUpdate =
                await _context.Enrollments.FirstOrDefaultAsync(x =>
                    x.EnrollmentId == enrollDto.EnrollmentId);
            
            enrollmentToUpdate.Marks = enrollDto.Marks;
            
            await _context.SaveChangesAsync();
            return enrollmentToUpdate;
        }

        // Questions and Replies
        [HttpGet("question/{id}")]
        public async Task<ActionResult<List<Question>>> Question(int id)
        {
            return await _context.Questions.Where(x => x.StudentId == id).ToListAsync();
        }

        [HttpGet("replies/{id}")]
        public async Task<ActionResult<List<Reply>>> Replies(int id)
        {
            return await _context.Replies.Where(x => x.StudentId == id).ToListAsync();
        }

        [HttpPost("getrecommendation")]
        public async Task<ActionResult<List<CourseMarksDto>>> GetRecommendation(CoursesListDto coursesListDto)
        {
            if (await _context.Enrollments.Where(x => x.StudentId == coursesListDto.StudentId).ToListAsync() == null)
                return BadRequest("Please first enroll in courses to get recommendation");
            new MLModelBuilder().CreateModel();
            ConsumeModel model = new ConsumeModel();
            if ((await _context.Users.FirstOrDefaultAsync(x => x.Id == coursesListDto.StudentId)) == null)
                return NotFound("Student Doesn't Exist");
            
            List<CourseMarksDto> CoursesList = new List<CourseMarksDto>();
            foreach (var course in coursesListDto.CoursesList)
            {
                if (await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == course) == null)
                    return NotFound("Course Doesn't Exist");
                ModelInput modelInput = new ModelInput()
                {
                    StudentId = coursesListDto.StudentId,
                    CourseId = course
                };

                float score = 0;
                if (model.Predict(modelInput).Score > 100) {
                    score = 100;
                } else if (model.Predict(modelInput).Score < 0) {
                    score = 0;
                } else {
                    score = model.Predict(modelInput).Score;
                }

                CoursesList.Add(new CourseMarksDto(){
                    CourseId = course,
                    Marks = score
                });
            }

            Console.WriteLine(CoursesList);
            return CoursesList;
        }
    }
}
