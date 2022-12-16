using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    var materias = context.MateriaGetAll().ToList();

                    result.Objects = new List<object>();

                    if(materias != null)
                    {
                        foreach(var objMateria in materias)
                        {
                            ML.Materia materia = new ML.Materia();

                            materia.IdMateria = objMateria.IdMateria;
                            materia.Nombre = objMateria.Nombre;
                            materia.Costo = objMateria.Costo.Value;

                            result.Objects.Add(materia); ;
                        }
                        result.Correct = true;
                    }

                }
                
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }
            return result;
        }

        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    int query = context.MateriaAdd(materia.Nombre, materia.Costo);
                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha agregado la materia";
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

        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    int query =context.MateriaUpdate(materia.IdMateria, materia.Nombre, materia.Costo);

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha actualizado la materia";

                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }

            return result;
        }


        public static ML.Result GetById(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    var objMateria = context.MateriaGetById(IdMateria).SingleOrDefault();

                    if(objMateria != null)
                    {
                        ML.Materia materia = new  ML.Materia();

                        materia.IdMateria = objMateria.IdMateria;
                        materia.Nombre = objMateria.Nombre;
                        materia.Costo = objMateria.Costo.Value;

                        result.Object = materia;
                    }

                    result.Correct = true;
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }

            return result;
        }

        public static ML.Result Delete(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.LSantosControlEscolarEntities context = new DL_EF.LSantosControlEscolarEntities())
                {
                    int query = context.MateriaDelete(IdMateria);

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se ha eliminado la materia";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Error: " + result.Ex;
            }
            return result;
        }
    }
}
