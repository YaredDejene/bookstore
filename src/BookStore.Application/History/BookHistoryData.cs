using System.Collections.Generic;

namespace BookStore.Application.History
{
    public class BookHistoryData
    {        
        public string Id { get; set; }      
        public string Authors { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }

        public string Action { get; set; }
        public string Timestamp { get; set; }
        public string User { get; set; }
        public IList<string> Changes { get; set; } = new List<string>();
        public string ChangeText { 
            get {
                return string.Join(" | ", Changes);
            } 
        }
    }
}