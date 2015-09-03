using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using Cool.Module.Service.Model;
using Cool.Module.Service.Persistence.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Cool.Module.Service.Persistence.TableStorage
{
    public class PagePersistence
    {
        public async Task Save(Page page)
        {
            var table = await GetTable();

            var tableEntity = new PageTableEntity
            {

                PartitionKey = page.Day.Ticks.ToString(),
                RowKey = HttpUtility.UrlEncode(page.Uri.AbsoluteUri),

                Day = page.Day,

                Url = page.Uri.AbsoluteUri,
                Domain = page.Domain,
                Path = page.Path,

                Title = page.Title,
                Description = page.Description,
                Tags = page.Tags
            };

            TableOperation insertOperation = TableOperation.Insert(tableEntity);

            await table.ExecuteAsync(insertOperation).ConfigureAwait(false);
        }

        private async Task<CloudTable> GetTable()
        {
            var account = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
            var table = account
                        .CreateCloudTableClient()
                        .GetTableReference("VisitedPages");

            await table.CreateIfNotExistsAsync();

            return table;
        }
    }
}