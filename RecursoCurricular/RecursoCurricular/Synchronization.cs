using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecursoCurricular
{
    public class Synchronization
    {
        public void SyncUp(object o)
        {
            Random random = new Random();

            int indice = random.Next(0, 4);

            string[] claves = System.Configuration.ConfigurationManager.AppSettings["PalabrasClave"].Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            string urlSincronizacion = (o as RecursoCurricular.PassingObject.BaseObject).UrlSincronizacion;

            foreach (RecursoCurricular.Sistema sistema in RecursoCurricular.Sistema.GetAll(true))
            {
                string output = string.Empty;

                try
                {
                    output = JsonConvert.SerializeObject(o, Formatting.Indented);

                    string url = string.Format("{0}{1}", sistema.Url, urlSincronizacion);

                    byte[] data = UTF8Encoding.UTF8.GetBytes(output);

                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                    request.Timeout = 10 * 1000;
                    request.Method = "POST";
                    request.ContentLength = data.Length;
                    request.ContentType = "application/json; charset=utf-8";

                    request.Headers.Add("token", token);

                    Stream postStream = request.GetRequestStream();

                    postStream.Write(data, 0, data.Length);

                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string resp = reader.ReadToEnd();

                    // Cerramos los streams
                    reader.Close();

                    postStream.Close();

                    response.Close();

                    using (RecursoCurricular.Context context = new RecursoCurricular.Context())
                    {
                        new RecursoCurricular.Sincronizacion
                        {
                            SistemaId = sistema.Id,
                            EstadoSincronizacionCodigo = resp.Equals("\"OK\"") ? RecursoCurricular.EstadoSincronizacion.Sincronizado.Codigo : RecursoCurricular.EstadoSincronizacion.NoSincronizado.Codigo,
                            Tipo = o.GetType().FullName.Replace(".PassingObject.", "."),
                            Objeto = output,
                            Detalle = resp.Equals("\"OK\"") ? string.Format("Objeto sincronizado a las {0}", DateTime.Now) : string.Format("Objeto no sincronizado a las {0}, detalle {1}", DateTime.Now, resp)
                        }.Save(context);

                        context.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    using (RecursoCurricular.Context context = new RecursoCurricular.Context())
                    {
                        new RecursoCurricular.Sincronizacion
                        {
                            SistemaId = sistema.Id,
                            EstadoSincronizacionCodigo = RecursoCurricular.EstadoSincronizacion.NoSincronizado.Codigo,
                            Tipo = o.GetType().FullName,
                            Objeto = output,
                            Detalle = string.Format("Objeto no sincronizado a las {0}, detalle {1}", DateTime.Now, ex.Message)
                        }.Save(context);

                        context.SubmitChanges();
                    }
                }
            }
        }
    }
}