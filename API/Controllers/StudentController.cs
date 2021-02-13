using API.Data;
using API.DTOs;
using API.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public StudentController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("new")]
        public ActionResult New()
        {
            return Ok("This is a message");
        }

        [HttpPost("new")]
        public ActionResult New(NewStudentDto newStudentDto)
        {
            Student student = mapper.Map<Student>(newStudentDto);

            return Ok("Data Saved");
        }
    }
}
