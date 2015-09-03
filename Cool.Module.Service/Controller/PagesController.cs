using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
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

        [Route("api/page/")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOnlyOnePage(DateTime day, Uri url)
        {
            var persistence = new PagePersistence();

            var page = await persistence.Get(day, url);

            return Ok(page);
        }

        [Route("api/page/")]
        [HttpGet]
        public async Task<IHttpActionResult> ListPagesByDay(DateTime day)
        {
            var persistence = new PagePersistence();

            var pages = await persistence.ListByDay(day);

            return Ok(pages);
        }
    }
}
