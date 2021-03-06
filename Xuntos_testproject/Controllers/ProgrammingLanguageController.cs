using System;
using System.Web.Http;
using Umbraco.Web.WebApi;
using Xuntos_testproject.Models;
using Xuntos_testproject.Database;

namespace Xuntos_testproject.Controllers
{
    public class ProgrammingLanguageController : UmbracoApiController
    {
        private readonly ApplicationDbContext _context;
        public ProgrammingLanguageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/programminglanguages")]
        public IHttpActionResult GetProgrammingLanguages()
        {
            var result = _context.GetProgrammingLanguages();
            return Json(result);
        }

        [HttpPost]
        [Route("api/programminglanguages")]
        public IHttpActionResult PostProgrammingLanguage([FromBody]ProgrammingLanguage language)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.PostProgrammingLanguage(language);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("api/programminglanguages")]
        public IHttpActionResult DeleteProgrammingLanguage([FromBody] ProgrammingLanguage language)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.DeleteProgrammingLanguage(language);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                return BadRequest(ModelState);
            }
        }
    }
}