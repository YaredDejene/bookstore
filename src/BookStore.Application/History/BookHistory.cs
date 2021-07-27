using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BookStore.Domain.Core.Events;

namespace BookStore.Application.History
{
    public static class BookHistory
    {
        public static IList<BookHistoryData> HistoryData { get; set; }

        public static IList<BookHistoryData> ToBookHistory(IEnumerable<StoredEvent> storedEvents)
        {
            HistoryData = new List<BookHistoryData>();
            BookHistoryDeserializer(storedEvents);

            var ordered = HistoryData.OrderBy(b => b.Timestamp);
            var list = new List<BookHistoryData>();
            BookHistoryData latestHistoryData = null;

            foreach(var ord in ordered)
            {
                var bookHistoryData = new BookHistoryData
                {                    
                    Action = string.IsNullOrWhiteSpace(ord.Action) ? string.Empty : ord.Action,
                    Timestamp = ord.Timestamp,
                    User = ord.User
                };

                if( latestHistoryData != null)
                {
                    bookHistoryData.Id = ( ord.Id == Guid.Empty.ToString() || ord.Id == latestHistoryData.Id) ? string.Empty : ord.Id;
                    if(ord.Title != latestHistoryData.Title)
                        bookHistoryData.Changes.Add($"Title was changed to \"{ord.Title}\"");
                    if(ord.Description != latestHistoryData.Description)
                        bookHistoryData.Changes.Add($"Description was changed to \"{ord.Description}\"");
                    if(ord.PublishDate != latestHistoryData.PublishDate)
                        bookHistoryData.Changes.Add($"Publish Date was changed to \"{ord.PublishDate.Substring(0,10)}\"");
                    if(ord.Authors != latestHistoryData.Authors)
                        bookHistoryData.Changes.Add($"Authors was changed to \"{ord.Authors}\"");
                }

                list.Add(bookHistoryData);
                latestHistoryData = ord;
            }

            return list;
        }

        private static void BookHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach(var storedEvent in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<BookHistoryData>(storedEvent.Data);
                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch(storedEvent.MessageType)
                {
                    case "BookRegisteredEvent":
                        historyData.Action = "Registered";
                        historyData.User = storedEvent.User;
                        break;
                    case "BookUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.User = storedEvent.User;
                        break;
                    case "BookRemovedEvent":
                        historyData.Action = "Removed";
                        historyData.User = storedEvent.User;
                        break;
                    default:
                        historyData.Action = "Unrecognized";
                        historyData.User = storedEvent.User ?? "Anonymous";
                        break;

                }

                HistoryData.Add(historyData);
            }
        }
    }
}