using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.Wrappers
{
    public class DataTableRequest
    {
        public int Draw { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public Search Search { get; set; }

        public IEnumerable<Column> Columns {get;set;}

        public IEnumerable<Order> Order {get;set;}

        public IDictionary<string, object> AdditionalParameters { get; set; }

        public string Data { get; set; }

        public string Sort => Order == null || Order.Count() == 0 ? null : 
                    Columns?.Take(Order.FirstOrDefault().Column + 1)?.LastOrDefault()?.Name;
        public bool IsDescending  => Order == null || Order.Count() == 0 ? false : (Order.FirstOrDefault().Dir == "desc");
    }

    public class Search
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }        
    }

    public class Order {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public enum OrderDirection
    {
        Ascendant = 0,
        Descendant = 1
    }
}

