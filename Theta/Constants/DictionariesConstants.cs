using System;
using System.Collections.Generic;

namespace Theta.Constants
{
    public static class DictionariesConstants
    {
        public static Dictionary<int, string> Priorities { get; } = new Dictionary<int, string>
        {
            { 0, "Very high"},
            { 1, "High" },
            { 2, "Medium" },
            { 3, "Low" },
            { 4, "On occasion" },
            { 5, "Optional" }
        };

        public static Dictionary<int, string> Statuses { get; } = new Dictionary<int, string>
        {
            { 0, "Open" },
            { 1, "Waiting" },
            { 2, "Work in progress" },
            { 3, "Postponed" },
            { 4, "Done" },
            { 5, "Deleted" },
            { 6, "Closed" }
        };

        public static Dictionary<int, string> NodeTypes { get; } = new Dictionary<int, string>
        {
            { 0, "Theme" },
            { 1, "Project" },
            { 2, "Task"},
            { 3, "Comment" },
            { 4, "Working time assignment" },
            { 5, "Question" },
            { 6, "Progress info" }
        };

        public static Dictionary<int, string> Employees { get; } = new Dictionary<int, string>
        {
            { 0, "Jack Nicholson" },
            { 1, "Robert De Niro" },
            { 2, "Al Pacino" },
            { 3, "Tom Hanks" },
            { 4, "Denzel Washington" },
            { 5, "Robin Williams" },
            { 6, "Morgan Freeman" }
        };
    }
}
