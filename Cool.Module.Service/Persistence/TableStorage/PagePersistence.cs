using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Cool.Module.Service.Model;
using Cool.Module.Service.Persistence.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.Queryable;


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

        public async Task<Page> Get(DateTime day, Uri url)
        {
            var table = await GetTable();

            var retrieveOperation = TableOperation
                                        .Retrieve<PageTableEntity>( // Shame on you Microsoft!! Part 2
                                            day.Ticks.ToString(),
                                            HttpUtility.UrlEncode(url.AbsoluteUri));

            var tableResult = await table.ExecuteAsync(retrieveOperation).ConfigureAwait(false);
            var result = tableResult.Result as PageTableEntity;

            if (result == null) return null;

            return ToPage(result);
        }

        public async Task<IEnumerable<Page>> ListByDay(DateTime day)
        {
            var table = await GetTable();

            var queryOperation = table.CreateQuery<PageTableEntity>()
                .Where(p => p.PartitionKey == day.Ticks.ToString())
                .AsTableQuery();

            var tableResults = table.ExecuteQuery(queryOperation);

            if (tableResults == null || !tableResults.Any()) return null;

            return tableResults.Select(ToPage).ToList();
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

        private Page ToPage(PageTableEntity pageTableEntity)
        {
            return new Page
            {
                Day = pageTableEntity.Day,

                Uri = new Uri(pageTableEntity.Url),

                Title = pageTableEntity.Title,
                Description = pageTableEntity.Description,
                Tags = pageTableEntity.Tags
            };
        }


    }
}