using System.Text;

namespace EventManagement.WebRazorPages.Pages.Shared
{
    public static class API
    {
        public const string ClientName = "WebAPI";
        public const string ClientBaseAddress = "http://localhost:5163/";
        public const string AccessTokenKey = "Access-Token";
        public const string RefreshTokenKey = "Refresh-Token";

        public static class Definition
        {
            public static string Create(string name) => $"api/{name}/Create";
            public static string Get(string name, int id) => $"api/{name}/Get/{id}";
            public static string GetPaginated(string name, int pageSize, int pageNumber) => $"api/{name}/GetPaginated/?pageSize={pageSize}&pageNumber={pageNumber}";
            public static string Edit(string name) => $"api/{name}/Edit";
            public static string Delete(string name, int id) => $"api/{name}/Delete/{id}";
            internal static string? GetAll(string name) => $"api/{name}/GetAll";
        }
        public static class User
        {
            public const string Login = "api/Users/Login";
            public const string Register = "api/Users/Register";
            public const string ChangePassword = "api/Users/ChangePassword";
            public const string Organizations = "api/Users/Organizations";
            public const string ParticipedEvents = "api/Users/ParticipEdevents";
            public const string UpcomingEvents = "api/Users/UpcomingEvents";
        }
        public static class Event
        {
            public const string Edit = "api/Events/edit";
            public const string CreateTicketEvent = "api/Events/CreateTicketEvent";
            public const string CreateEvent = "api/Events/CreateEvent";
            public const string GetUnapprovedEvents = "api/Events/GetUnapprovedEvents";
            public static string GetEventForEdit(int id) => $"api/Events/GetEventForEdit/{id}";
            public static string Get(int id) => $"api/Events/Get/{id}";
            public static string Delete(int id) => $"api/Events/Delete/{id}";
            public static string ApproveEvent(int id) => $"api/Events/ApproveEvent/{id}";
            public static string DeclineEvent(int id) => $"api/Events/DeclineEvent/{id}";
            public static string GetPaginated(int pageNumber, int pageSize, IEnumerable<int> categories, IEnumerable<int> cities)
            {
                StringBuilder stringBuilder = new("api/Events/GetPaginatedEvents?");

                stringBuilder.AppendFormat("pageNumber={0}&pageSize={1}", pageNumber, pageSize);

                stringBuilder.AppendJoin(string.Empty, categories.Select(c => $"&categoryIds={c}"));

                stringBuilder.AppendJoin(string.Empty, cities.Select(c => $"&cityIds={c}"));

                return stringBuilder.ToString();
            }
        }
        public static class IntegrationService
        {
            public const string GetAll = "api/IntegrationServices/GetIntegrationServices";
        }
    }
}
