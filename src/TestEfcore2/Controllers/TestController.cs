using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestEfcore.Data;

namespace TestEfcore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
     
        private readonly ILogger<TestController> _logger;
        private readonly DefaultDbContext _defaultDbContext;

        public TestController(ILogger<TestController> logger,DefaultDbContext defaultDbContext)
        {
            _logger = logger;
            _defaultDbContext = defaultDbContext;
        }


        [HttpGet]
        public string SoftDelete()
        {
            var list = _defaultDbContext.Classrooms.Include(c => c.Students).Where(x => x.IsDeleted == false).ToList();
            return "ok";
        }

        [HttpGet("Attach")]
        public string Attach()
        {
            Classroom classroom = new Classroom
            {
                Id = 1,
                Name = "b",
                Students = new List<Student>
                {
                    new Student()
                    {
                        Name = "aa",
                        Id = 2
                     }
                }
            };

            if (_defaultDbContext.Entry(classroom).State == EntityState.Detached)
            {
                _defaultDbContext.Classrooms.Attach(classroom);
                foreach(var stu in classroom.Students)
                {
                    _defaultDbContext.Students.Attach(stu);
                }
            }

            _defaultDbContext.Entry(classroom).State = EntityState.Modified;
            foreach (var stu in classroom.Students)
            {
                _defaultDbContext.Entry(stu).State  = EntityState.Modified;
            }

            _defaultDbContext.SaveChanges();
            return "ok";
        }

    }
}
