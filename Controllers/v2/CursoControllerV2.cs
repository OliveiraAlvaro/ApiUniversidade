using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using apiUniversidade.Model;
using apiUniversidade.Context;
using Microsoft.AspNetCore.Authorization;


namespace apiUniversidade.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/curso")]

    
    public class CursoControllerV2 : ControllerBase
    {
        private readonly ILogger<CursoController> _logger;
        private readonly apiUniversidadeContext _context;

         public CursoControllerV2(ILogger<CursoController> logger, apiUniversidadeContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Curso>> Get()
        {
            var cursos = _context.Cursos.ToList();
            if(cursos is null)
                return NotFound();  

            return cursos;
        }

        [HttpPost]
        public ActionResult Post(Curso curso){
            _context.Cursos.Add(curso);
            _context.SaveChanges();

            return new CreatedAtRouteResult ("GetCurso", new{ id = curso.ID}, curso);
        }

        [HttpGet ("{id:int}", Name ="GetCurso")]
        public ActionResult<Curso> Get(int id)
        {
            var curso = _context.Cursos.FirstOrDefault(p => p.ID == id);
            if(curso is null)
                return NotFound("Curso não encontado.");

                return curso;
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Curso curso){
            if(id != curso.ID)
                return BadRequest();

            _context.Entry(curso).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(curso);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete (int id){
            var curso = _context.Cursos.FirstOrDefault(p => p.ID == id);

            if(curso is null)
            return NotFound();

            _context.Cursos.Remove(curso);
            _context.SaveChanges();

            return Ok(curso);
        }



    }
}
//odeio versionamento