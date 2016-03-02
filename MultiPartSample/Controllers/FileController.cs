using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MultiPartSample.Controllers
{
    public class FileController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Multipart()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var streamProvider = await Request.Content.ReadAsMultipartAsync();
            try
            {
                var provider = streamProvider.Contents.First();
                var bytes = await provider.ReadAsByteArrayAsync();
                //do something with the bytes
                var aux = @"~/Content/Files/" + DateTime.Now.Ticks + ".png";
                var res = Url.Content(aux);
                var path = HttpContext.Current.Server.MapPath(aux);
                System.IO.File.WriteAllBytes(path,bytes);
                return Request.CreateResponse(HttpStatusCode.Accepted, res);
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> bson()
        {
            try
            {
                byte[] bytes = await Request.Content.ReadAsByteArrayAsync();
                //do something with the bytes
                var aux = @"~/Content/Files/" + DateTime.Now.Ticks + ".png";
                var res = Url.Content(aux);
                var path = HttpContext.Current.Server.MapPath(aux);
                System.IO.File.WriteAllBytes(path, bytes);
                return Request.CreateResponse(HttpStatusCode.Accepted, res);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
