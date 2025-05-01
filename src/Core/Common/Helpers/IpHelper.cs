using System.Text.Json;

namespace Core.Common.Helpers
{
    public static class IpHelper
    {
        public static async Task<string> GetLocationFromIpAsync(string ip)
        {
            if (IsLocalIp(ip))
            {
                return "Yerel Ağ";
            }

            using var client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync($"http://ip-api.com/json/{ip}");
                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                var status = root.GetProperty("status").GetString();
                if (status != "success")
                    return "Bilinmeyen";

                string country = root.GetProperty("country").GetString() ?? "Bilinmiyor";
                string city = root.GetProperty("city").GetString() ?? "Bilinmiyor";

                return $"{country} / {city}";
            }
            catch
            {
                return "Bilinmeyen";
            }
        }



        public static bool IsLocalIp(string ip)
        {
            return ip == "::1" || ip == "127.0.0.1";
        }
    }
}
