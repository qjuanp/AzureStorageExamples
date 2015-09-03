using System;
using System.Threading.Tasks;
using System.Web.Http;
using Cool.Module.Service.Model;
using Cool.Module.Service.Persistence.TableStorage;

namespace Cool.Module.Service.Controller
{
    public class PagesController : ApiController
    {
        [Route("api/page/")]
        [HttpPost]
        public async Task<IHttpActionResult> SaveASinglePage(Page page)
        {
            var persistence = new PagePersistence();

            page.Day = DateTime.Today;

            await persistence.Save(page);

            return Ok(page);
        }
    }
}
