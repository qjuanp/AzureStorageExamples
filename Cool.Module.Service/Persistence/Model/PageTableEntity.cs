using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Cool.Module.Service.Persistence.Model
{
    public class PageTableEntity : TableEntity
    {
        public DateTime Day { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
    }
}