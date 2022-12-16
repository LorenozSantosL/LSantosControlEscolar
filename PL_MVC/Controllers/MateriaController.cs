using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);

                    var responseTask = client.GetAsync("Materia/GetAll");

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach(var resultItem in readTask.Result.Objects)
                        {
                            ML.Materia resultItemList  = JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }

                }
            
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }

            if (result.Correct)
            {
                materia.Materias = result.Objects;
                return View(materia);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta ";
                return PartialView("Modal");
            }
            
        }

        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();

            if(IdMateria == null)
            {
                return View(materia);
            }
            else
            {
                ML.Result result = new ML.Result();
                try
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);

                        var responseTask = client.GetAsync("Materia/GetById/" + IdMateria.Value);

                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();

                            readTask.Wait();

                            ML.Materia materiaItem = new ML.Materia();
                            materiaItem = JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                            result.Object = materiaItem;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }

                if (result.Correct)
                {
                    materia = (ML.Materia)result.Object;

                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la materia seleccionada";
                }
                return View(materia);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if(materia.IdMateria == 0)
            {
                ML.Result result = new ML.Result();
                try
                {
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);

                        var postTask = client.PostAsJsonAsync<ML.Materia>("Materia/Add", materia);

                        postTask.Wait();

                        var resultServicio = postTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "Ocurrio un error al agregar la Materia";
                        }
                    }
                }
                catch(Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }

                if (result.Correct)
                {
                    ViewBag.Message ="Se ha agregado la Materia";
                }
                else
                {
                    ViewBag.Messsage = "Error:  " + result.Message;
                }
            }
            else
            {
                ML.Result result = new ML.Result();

                try
                {
                    using(var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);

                        var postask = cliente.PutAsJsonAsync<ML.Materia>("Materia/Update/" + materia.IdMateria, materia);

                        postask.Wait();

                        var resultServicio = postask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "Ocurrio un error al actualizar la materia";
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha actualiado la materia";
                }
                else
                {
                    ViewBag.Message = "Error: " + result.Message;
                }
            }
            return PartialView("Modal");
        }

        public ActionResult Delete(int IdMateria)
        {
            ML.Result result = new ML.Result();

            if(IdMateria > 0)
            {
                try
                {
                    using(var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);

                        var responseTask = client.DeleteAsync("Materia/Delete/" + IdMateria);

                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                    }
                }
                catch(Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha elimnado el Usuario";
                }
            }

            return PartialView("Modal");
        }
    }
}