using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result Add(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaAdd(alumnoMateria.Alumno.IdAlumno, alumnoMateria.Materia.IdMateria);

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha registrado la materia del alumno";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }

            return result;
        }

        public static ML.Result GetAlumnoMateriaByIdAlumno(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    var MateriasQuery = context.AlumnoMateriaGetByIdAlumno(IdAlumno).ToList();

                    result.Objects = new List<object>();

                    if (MateriasQuery != null)
                    {
                        
                     
                        result.Objects = new List<object>();
                        foreach(var obj in MateriasQuery)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Alumno = new ML.Alumno();

                            alumnoMateria.IdAlumnoMateria = obj.IdAlumnoMateria;
                            alumnoMateria.Alumno.Nombre = obj.AlumnoNombre;
                            alumnoMateria.Alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumnoMateria.Alumno.ApellidoMaterno = obj.ApellidoMaterno;

                            alumnoMateria.Materia.IdMateria = obj.IdMateria.Value;
                            alumnoMateria.Materia.Nombre = obj.MateriaNombre;
                            alumnoMateria.Materia.Costo = obj.Costo.Value;


                            result.Objects.Add(alumnoMateria);


                        }
                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }
            return result;
        }


        public static ML.Result GetSinAsignarByIdAlumno(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    var materiasSinAsignar = context.MateriasGetNoAsignadas(IdAlumno).ToList();

                    result.Objects = new List<object>();

                    if(materiasSinAsignar != null)
                    {
                        foreach(var obj in materiasSinAsignar)
                        {
                            ML.Materia materia = new ML.Materia();

                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Costo = obj.Costo.Value;

                            result.Objects.Add(materia);    
                        }

                        result.Correct = true;
                    }


                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }
            return result;
        }


        public static ML.Result EliminarMateria(int IdAlumno,int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaDelete(IdAlumno, IdMateria);

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha eliminado la meteria del usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }

            return result;
        }

    }
}
