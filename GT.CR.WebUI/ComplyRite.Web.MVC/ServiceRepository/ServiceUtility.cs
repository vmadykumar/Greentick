using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ComplyRite.Web.MVC.ServiceRepository
{
    public class ServiceUtility : IServiceUtility
    {        
        /// Author : Sharath K M
        /// <summary>
        /// Method to get the data from external service
        /// </summary>
        /// <typeparam name="TObject">Model type of the data</typeparam>
        /// <param name="URL">URL to be accessed</param>
        /// <param name="serviceName">The key of the service url present in the config file</param>
        /// <param name="ConFigPath">Path of the config file from which the service key is matched</param>
        /// <returns></returns>
        public async Task<TObject> GetDataFromService<TObject>(string URL, string serviceName, string ConFigPath = "~/PrivateSettings.config") where TObject : new()
        {
            try
            {
                TObject result = default(TObject);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ConFigPath).AppSettings.Settings[serviceName].Value.ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ConFigPath).AppSettings.Settings[serviceName].Value.ToString() + URL);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsAsync<TObject>();
                    }
                    else
                    {
                        var ex = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(ex);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TObject> GetImgDataFromService<TObject>(string URL, string serviceName, string ConFigPath = "~/PrivateSettings.config") where TObject : new()
        {
            try
            {
                TObject result = default(TObject);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ConFigPath).AppSettings.Settings[serviceName].Value.ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ConFigPath).AppSettings.Settings[serviceName].Value.ToString() + URL);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsAsync<TObject>();
                    }
                    else
                    {
                        var ex = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(ex);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string ConvertImageToArray(Image imageIn)
        {
            Image img = imageIn;
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }
            var base64 = Convert.ToBase64String(arr);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
            return imgSrc;
        } 

        /// Author : Sharath K M
        /// <summary>
        /// Method to post the data to external service
        /// </summary>
        /// <typeparam name="TObject">Model type of the data</typeparam>
        /// <param name="URL">URL to be accessed</param>
        /// <param name="serviceName">The key of the service url present in the config file</param>
        /// <param name="data">The data that is to be posted to the service</param>
        /// <param name="ConFigPath">Path of the config file from which the service key is matched</param>
        /// <returns></returns>
        public async Task<TObject> PostDataToService<TObject>(string URL, string serviceName, object data, string ConFigPath = "~/PrivateSettings.config") where TObject : new()
        {
            try
            {
                TObject result = default(TObject);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ConFigPath).AppSettings.Settings[serviceName].Value.ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                  
                    var response = await client.PostAsJsonAsync(URL, data);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsAsync<TObject>();
                    }
                    else
                    {
                        var ex = response.Content.ReadAsStringAsync().Result;
                        throw new Exception(ex);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}