using System.Collections.Generic;
using Cool.Module.Service.Model;

namespace Cool.Module.Service.Results
{
    public class ContinousResult
    {
        public string Token { get; set; }
        public IEnumerable<Page> Content { get; set; }
    }
}