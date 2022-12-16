using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WEBAPI.Controllers
{
    public class MateriaController : ApiController
    {
        // GET: api/Materia
        [Route("api/Materia/GetAll")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            ML.Result result = BL.Materia.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // GET: api/Materia/5

        [Route("api/Materia/GetById/{idmateria}")]
        [HttpGet]
        public IHttpActionResult get(int idmateria)
        {
            ML.Result result = BL.Materia.GetById(idmateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

     

        [Route("api/Materia/Add")]
        public IHttpActionResult Post([FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.Add(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        // POST: api/Materia
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Materia/5
        [Route("api/Materia/Update/{idmateria}")]
        public IHttpActionResult Put(int idmateria, [FromBody] ML.Materia materia)
        {
            materia.IdMateria = idmateria;
            ML.Result result = BL.Materia.Update(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        // DELETE: api/Materia/5
        [Route("api/Materia/Delete/{idmateria}")]
        public IHttpActionResult Delete(int idmateria)
        {
            ML.Result result = BL.Materia.Delete(idmateria);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
    }
}
