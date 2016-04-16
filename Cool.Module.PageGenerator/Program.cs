using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cool.Module.Service.Model;
using Newtonsoft.Json;

namespace Cool.Module.PageGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            var pages = GenerateRandomPages(200);

            var publisher = new Publisher();

            var task = new Task(async () => await publisher.PublishPages(pages));
            task.RunSynchronously();

            Console.ReadLine();
        }

        private static IList<Page> GenerateRandomPages(int quantity)
        {
            var pages = new List<Page>(quantity);
            var random = new Random();

            for (int i = 0; i < quantity; i++)
            {
                var value = random.Next(10, 10000);
                pages.Add(new Page
                {
                    Uri = new Uri(string.Format("http://random.org/{0}", value)),

                    Title = string.Format("Title {0}", value),
                    Description = string.Format("Description {0}", value),
                    Tags = string.Format("Tags {0}", value),
                });
            }

            return pages;
        }

        class Publisher
        {
            public async Task PublishPages(IList<Page> pages)
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri("http://localhost:50685/");

                    var content = new StringContent(
                        JsonConvert.SerializeObject(pages),
                        Encoding.UTF8,
                        "application/json"
                        );

                    var response = await http.PostAsync("/api/page/multiples", content);

                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
            }
        }
    }


}
