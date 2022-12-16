using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno

        [HttpGet]
        public ActionResult GetAll()
        {
            ServiceAlumno.AlumnoClient context = new ServiceAlumno.AlumnoClient();

            var result = context.GetAll();
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = new List<object>();

            if (result.Correct)
            {
                foreach(var obj in result.Objects)
                {
                    alumno.Alumnos.Add(obj);
                }
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta";
            }

            return View(alumno);
        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();

            if(IdAlumno == null)
            {
                return View(alumno);
            }
            else
            {
                ServiceAlumno.AlumnoClient context = new ServiceAlumno.AlumnoClient();

                var result = context.GetById(IdAlumno.Value);
                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al obtener al alumno";
                }
                return View(alumno);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            if(alumno.IdAlumno == 0)
            {
                ServiceAlumno.AlumnoClient context = new ServiceAlumno.AlumnoClient();

                var result = context.Add(alumno);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = result.Message;
                }

            }
            else
            {
                ServiceAlumno.AlumnoClient context = new ServiceAlumno.AlumnoClient();

                var result = context.Update(alumno);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = result.Message;
                }
                
            }

            return PartialView("Modal");
        }

        public ActionResult Delete(int IdAlumno)
        {
            if(IdAlumno > 0)
            {
                ServiceAlumno.AlumnoClient context = new ServiceAlumno.AlumnoClient();

                var result = context.Delete(IdAlumno);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = result.Message;
                }
                
            }

            return PartialView("Modal");
        }
    }
}