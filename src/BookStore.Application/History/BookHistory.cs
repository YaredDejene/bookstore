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
            var latestHistoryData = new BookHistoryData();

            foreach(var ord in ordered)
            {
                var bookHistoryData = new BookHistoryData
                {
                    Id = ord.Id == Guid.Empty.ToString() || ord.Id == latestHistoryData.Id ? string.Empty : ord.Id,
                    Title = string.IsNullOrWhiteSpace(ord.Title) || ord.Title == latestHistoryData.Title ? string.Empty : ord.Title,
                    Description = string.IsNullOrWhiteSpace(ord.Description) || ord.Description == latestHistoryData.Description ? string.Empty : ord.Description,
                    PublishDate = string.IsNullOrWhiteSpace(ord.PublishDate) || ord.PublishDate == latestHistoryData.PublishDate ? string.Empty : ord.PublishDate.Substring(0,10),
                    Authors = string.IsNullOrWhiteSpace(ord.Authors) || ord.Authors == latestHistoryData.Authors ? string.Empty : ord.Authors,

                    Action = string.IsNullOrWhiteSpace(ord.Action) ? string.Empty : ord.Action,
                    Timestamp = ord.Timestamp,
                    User = ord.User
                };

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