using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            ML.Result result = BL.Alumno.AlumnoGetAll();
            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error en mostrar los alumnos";
            }

            return View(alumno);
        }

        [HttpGet]
        public ActionResult MateriasAsignadas(int IdAlumno)
        {
            decimal total = 0;
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            ML.Result result = BL.AlumnoMateria.GetAlumnoMateriaByIdAlumno(IdAlumno);

            if (result.Correct)
            {
                alumnoMateria.AlumnoMaterias = result.Objects;
            }

            alumnoMateria.Alumno = new ML.Alumno();

            ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno);
            if (resultAlumno.Correct)
            {
                alumnoMateria.Alumno = (ML.Alumno)resultAlumno.Object;
            }

            if(alumnoMateria.AlumnoMaterias.Count > 0)
            {
                foreach(ML.AlumnoMateria materiacosto in alumnoMateria.AlumnoMaterias)
                {
                    total = total + materiacosto.Materia.Costo;
                }
            }

            ViewBag.Correct = true;
            ViewBag.Total = total;

            return View(alumnoMateria);

        }

        [HttpGet]
        public ActionResult AsignarMateria(int IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

            ML.Result result = BL.AlumnoMateria.GetSinAsignarByIdAlumno(IdAlumno);
            if (result.Correct)
            {
                alumnoMateria.AlumnoMaterias = result.Objects;
            }
            

            ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno);

            if (resultAlumno.Correct)
            {
                alumnoMateria.Alumno = (ML.Alumno)resultAlumno.Object;
            }

            

            return View(alumnoMateria);
        }


        [HttpPost]
        public ActionResult Asignar(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result(); 
            if(alumnoMateria.AlumnoMaterias != null)
            {
                foreach(string IdMataria in alumnoMateria.AlumnoMaterias)
                {
                    ML.AlumnoMateria alumnoMateriaItem = new ML.AlumnoMateria();

                    alumnoMateriaItem.Alumno = new ML.Alumno();

                    alumnoMateriaItem.Alumno.IdAlumno = alumnoMateria.Alumno.IdAlumno;

                    alumnoMateriaItem.Materia = new ML.Materia();

                    alumnoMateriaItem.Materia.IdMateria = int.Parse(IdMataria);

                    ML.Result resulAdd = BL.AlumnoMateria.Add(alumnoMateriaItem);
                }

                result.Correct = true;
                ViewBag.Message = "Se ha asignado materia(s)";
                ViewBag.MateriasAsigandas = true;
                ViewBag.IdAlumno = alumnoMateria.Alumno.IdAlumno;
            }
            else
            {
                result.Correct = false;
                ViewBag.Message = "No se asigno la materia(s)";
                ViewBag.MateriasAsigandas = false;
                ViewBag.IdAlumno = alumnoMateria.Alumno.IdAlumno;
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Eliminar(int IdAlumno, int IdMateria)
        {
            ML.Result result = BL.AlumnoMateria.EliminarMateria(IdAlumno, IdMateria);
            if (result.Correct)
            {
                result.Correct = true;
                ViewBag.Message = result.Message;
                ViewBag.MateriasAztualizadas = true;
                ViewBag.IdAlumno = IdAlumno;
            }
            else
            {
                result.Correct = false;
                ViewBag.Message = result.Message;
                ViewBag.MateriasAztualizadas = false;
                ViewBag._IdAlumno = IdAlumno;
            }

            return PartialView("Modal");
        }
    
    }
}