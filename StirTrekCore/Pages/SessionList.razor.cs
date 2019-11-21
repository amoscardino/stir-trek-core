using Microsoft.AspNetCore.Components;
using StirTrekCore.Models;
using StirTrekCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StirTrekCore.Pages
{
    public partial class SessionList
    {
        [Inject]
        public StirTrekService StirTrekService { get; set; }

        private List<TimeSlotModel> Schedule { get; set; } = new List<TimeSlotModel>();

        public bool ShowSavedSessionsOnly { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Schedule = await StirTrekService.GetFullScheduleAsync();
        }

        private List<TimeSlotModel> FilteredSchedule()
        {
            if (ShowSavedSessionsOnly)
            {
                return Schedule
                    .Select(x => new TimeSlotModel
                    {
                        Time = x.Time,
                        Sessions = x.Sessions.Where(y => y.IsSaved).ToList()
                    })
                    .Where(x => x.Sessions.Any())
                    .ToList();
            }

            return Schedule;
        }

        private async Task ToggleSavedStateAsync(SessionModel session)
        {
            session.IsSaved = !session.IsSaved;

            if (session.IsSaved)
                await StirTrekService.SaveSessionAsync(session.Id);
            else
                await StirTrekService.RemoveSavedSessionAsync(session.Id);
        }
    }
}
