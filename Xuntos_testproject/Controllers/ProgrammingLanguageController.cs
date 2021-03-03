using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Umbraco.Web.WebApi;
using Xuntos_testproject.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
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
      /*  private static List<ProgrammingLanguage> languages = new List<ProgrammingLanguage>()
        {
            new ProgrammingLanguage("C#", "Used during Mediatechnology study and while building Xamarin Apps at previous job"),
            new ProgrammingLanguage("JavaScript", "Used for various applications, with the Angular framework or seperate libraries")
        };*/

        [HttpGet]
        [Route("api/programminglanguages")]
        public IHttpActionResult GetProgrammingLanguages()
        {
            var result = _context.GetProgrammingLanguages();
            return Json(result);
        }

        [HttpPost]
        [Route("api/create/programminglanguage")]
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
    }
}