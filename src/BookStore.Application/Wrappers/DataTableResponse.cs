using System.Collections.Generic;

namespace BookStore.Application.Wrappers
{
    public class DataTableResponse<T> where T : class
    {
       public DataTableResponse(int draw, int totalRecords, int totalRecordsFiltered, IEnumerable<T> data, IDictionary<string, object> additionalParameters)
       {
           this.Draw = draw;
           this.RecordsTotal = totalRecords;
           this.RecordsFiltered = totalRecordsFiltered;
           this.Data = data;
           this.AdditionalParameters = additionalParameters;
       }

        public int Draw { get; set; }
        public string Error { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable<T>  Data { get; set; }
        public IDictionary<string, object> AdditionalParameters { get; set; }
    }
}