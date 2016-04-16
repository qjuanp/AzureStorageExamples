using System;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Cool.Module.Service.Util
{
    public class ContinuationTokenResolver
    {
        public TableContinuationToken GetToken(string continueWithToken)
        {
            if (string.IsNullOrEmpty(continueWithToken)) return new TableContinuationToken();
            var bytes = Convert.FromBase64String(continueWithToken);
            var serialized = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<TableContinuationToken>(serialized);
        }

        public string SerializeToken(TableContinuationToken tableContinuationToken)
        {
            if (tableContinuationToken == null) return string.Empty;
            var serialized = JsonConvert.SerializeObject(tableContinuationToken);
            var bytes = Encoding.UTF8.GetBytes(serialized);
            return Convert.ToBase64String(bytes);
        }
    }
}