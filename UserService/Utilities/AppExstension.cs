using System.Text;
using System.Text.Json;

namespace UserService.Utilities
{
    public class AppExstension
    {
        public static T FromBase64<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return default;
            }

            var bytes = Convert.FromBase64String(data);
            var parsedString = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<T>(parsedString);
        }
    }
}
