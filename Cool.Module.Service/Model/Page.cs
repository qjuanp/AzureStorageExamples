using System;

namespace Cool.Module.Service.Model
{
    public class Page
    {
        public Uri Uri { get; set; }
        public DateTime Day { get; set; }
        public string Domain { get { return Uri != null ? Uri.Host : string.Empty; } }
        public string Path { get { return Uri != null ? Uri.AbsolutePath : string.Empty; } }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
    }
}