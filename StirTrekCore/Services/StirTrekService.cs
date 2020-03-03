using Microsoft.AspNetCore.Components;
using StirTrekCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Extensions.Storage.Interfaces;

namespace StirTrekCore.Services
{
    public class StirTrekService
    {
        private const string SESSIONS_URL = "https://raw.githubusercontent.com/stirtrek/stirtrek.github.io/source/source/_data/sessions2019.json";
        private const string SCHEDULE_URL = "https://raw.githubusercontent.com/stirtrek/stirtrek.github.io/source/source/_data/schedule2019.json";

        private HttpClient _httpClient;
        private ILocalStorage _localStorage;

        private List<TimeSlotModel> _fullSchedule;

        public StirTrekService(HttpClient httpClient, ILocalStorage localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;

            _fullSchedule = new List<TimeSlotModel>();
        }

        public async Task<List<TimeSlotModel>> GetFullScheduleAsync()
        {
            if (!_fullSchedule.Any())
                await GetFullScheduleFromApiAsync();

            return _fullSchedule;
        }

        public async Task SaveSessionAsync(long id)
        {
            var session = _fullSchedule.SelectMany(x => x.Sessions).Where(x => x.Id == id).FirstOrDefault();

            if (session == null)
                throw new ArgumentException("Invalid ID");

            session.IsSaved = true;

            var savedIds = await _localStorage.GetItem<List<long>>("ST-SavedSessions") ?? new List<long>();

            if (!savedIds.Contains(id))
                savedIds.Add(id);

            await _localStorage.SetItem("ST-SavedSessions", savedIds);
        }

        public async Task RemoveSavedSessionAsync(long id)
        {
            var session = _fullSchedule.SelectMany(x => x.Sessions).Where(x => x.Id == id).FirstOrDefault();

            if (session == null)
                throw new ArgumentException("Invalid ID");

            session.IsSaved = false;

            var savedIds = await _localStorage.GetItem<List<long>>("ST-SavedSessions") ?? new List<long>();

            if (savedIds.Contains(id))
                savedIds.Remove(id);

            await _localStorage.SetItem("ST-SavedSessions", savedIds);
        }

        private async Task GetFullScheduleFromApiAsync()
        {
            var scheduleTask = _httpClient.GetJsonAsync<ScheduleData>(SCHEDULE_URL);
            var sessionsTask = _httpClient.GetJsonAsync<SessionData>(SESSIONS_URL);

            await Task.WhenAll(scheduleTask, sessionsTask);

            var schedule = scheduleTask.Result;
            var sessions = sessionsTask.Result;

            var savedIds = await _localStorage.GetItem<List<long>>("ST-SavedSessions") ?? new List<long>();

            _fullSchedule = schedule.ScheduledSessions
                .SelectMany(x => x.TimeSlots)
                .Select(x => new TimeSlotModel
                {
                    Time = x.Time,
                    Sessions = (from timeSlotSession in x.Sessions
                                join session in sessions.Sessions on timeSlotSession.Id.ToString() equals session.Id
                                select new SessionModel
                                {
                                    Id = timeSlotSession.Id,
                                    Title = session.Title,
                                    Description = session.Description,
                                    ScheduledRoom = timeSlotSession.ScheduledRoom,
                                    Speakers = (from speakerId in session.Speakers
                                                join speaker in sessions.Speakers on speakerId equals speaker.Id
                                                select speaker).ToList(),
                                    Track = sessions.Categories
                                                    .Where(y => y.Title.ToLower() == "track")
                                                    .SelectMany(y => y.Items)
                                                    .Where(y => session.CategoryItems?.Contains(y.Id) ?? false)
                                                    .OrderBy(y => y.Sort)
                                                    .Select(y => y.Name)
                                                    .FirstOrDefault(),
                                    Theatres = GetTheatres(timeSlotSession.ScheduledRoom),
                                    IsSaved = savedIds.Contains(timeSlotSession.Id)
                                }).ToList()
                })
                .ToList();
        }

        private List<Theatre> GetTheatres(string room)
        {
            switch (room?.ToLower() ?? string.Empty)
            {
                case "hmb":
                    return new List<Theatre>
                    {
                        new Theatre { TheatreName = "1" },
                        new Theatre { TheatreName = "2" },
                        new Theatre { TheatreName = "3", Speaker = true }
                    };

                case "leading edje":
                    return new List<Theatre>
                    {
                        new Theatre { TheatreName = "4", Speaker = true },
                        new Theatre { TheatreName = "5" },
                        new Theatre { TheatreName = "6" },
                        new Theatre { TheatreName = "7" },
                        new Theatre { TheatreName = "8" },
                        new Theatre { TheatreName = "9" }
                    };

                case "oclc":
                    return new List<Theatre>
                    {
                        new Theatre { TheatreName = "10" },
                        new Theatre { TheatreName = "11" },
                        new Theatre { TheatreName = "15", Speaker = true }
                    };

                case "mpw":
                    return new List<Theatre>
                    {
                        new Theatre { TheatreName = "12" },
                        new Theatre { TheatreName = "13" },
                        new Theatre { TheatreName = "14", Speaker = true}
                    };

                case "sogeti":
                    return new List<Theatre>
                    {
                        new Theatre { TheatreName = "16", Speaker = true },
                        new Theatre { TheatreName = "20" },
                        new Theatre { TheatreName = "21" }
                    };

                case "pillar":
                    return new List<Theatre>
                    {
                        new Theatre { TheatreName = "17", Speaker = true },
                        new Theatre { TheatreName = "18" },
                        new Theatre { TheatreName = "19" }
                    };

                case "manifest":
                    return new List<Theatre>
                    {
                        new Theatre { TheatreName = "23" },
                        new Theatre { TheatreName = "24" },
                        new Theatre { TheatreName = "25" },
                        new Theatre { TheatreName = "26" },
                        new Theatre { TheatreName = "27", Speaker = true}
                    };

                case "root":
                    return new List<Theatre>
                    {
                        new Theatre { TheatreName = "28", Speaker = true },
                        new Theatre { TheatreName = "29" },
                        new Theatre { TheatreName = "30" }
                    };

                default:
                    return new List<Theatre>();
            }
        }
    }
}
