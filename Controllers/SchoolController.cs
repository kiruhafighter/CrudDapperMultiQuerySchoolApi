using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrudDapperMultiQuerySchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {

        private ISchoolService _oSchoolService;

        public SchoolController(ISchoolService oSchoolService)
        {
            _oSchoolService = oSchoolService;
        }



        // GET: api/<SchoolController>
        [HttpGet]
        public object Get()
        {
            return _oSchoolService.Gets();
        }

        // GET api/<SchoolController>/5
        [HttpGet("{studentId}/{teacherId}")]
        public object Get(int studentId, int teacherId)
        {
            return _oSchoolService.Get(studentId, teacherId);
        }

        // POST api/<SchoolController>
        [HttpPost]
        public object Post([FromBody] School oSchool)
        {
            if(ModelState.IsValid)
            {
                return _oSchoolService.Save(oSchool.Student, oSchool.Teacher);
            }
            return null;
        }



        // DELETE api/<SchoolController>/5
        [HttpDelete("{studentId}/{teacherId}")]
        public string Delete(int studentId, int teacherId)
        {
             return _oSchoolService.Delete(studentId, teacherId);
        }
    }
}
