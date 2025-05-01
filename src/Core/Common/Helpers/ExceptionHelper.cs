using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Core.Common.Helpers
{
    public static class ExceptionHelper
    {
        public static string Catch(Exception ex)
        {
            return ex switch
            {
                SqlException sqlEx => sqlEx.Number switch
                {
                    2601 => "Hata: Aynı değeri tekrar ekleyemezsiniz.",
                    2627 => "Hata: Birincil anahtar tekrar kullanılamaz.",
                    18456 => "Hata: Giriş başarısız.",
                    547 => "Hata: Yabancı anahtar ihlali.",
                    1205 => "Hata: Deadlock oluştu.",
                    18452 => "Hata: SQL Server bağlantısı başarısız.",
                    4060 => "Hata: Veritabanı açılamıyor.",
                    11001 => "Hata: SQL Server'a ulaşılamıyor.",
                    _ => $"Bilinmeyen SQL hatası: {sqlEx.Message} (Kodu: {sqlEx.Number})"
                },

                DbUpdateException dbEx when dbEx.InnerException is SqlException sqlEx => sqlEx.Number switch
                {
                    2601 => "Hata: Aynı değeri tekrar ekleyemezsiniz.",
                    2627 => "Hata: Birincil anahtar tekrar kullanılamaz.",
                    547 => "Hata: Yabancı anahtar ihlali.",
                    1205 => "Hata: Deadlock oluştu.",
                    4060 => "Hata: Veritabanı açılamıyor.",
                    18456 => "Hata: Giriş başarısız.",
                    _ => $"Bilinmeyen SQL hatası: {sqlEx.Message} (Kodu: {sqlEx.Number})"
                },

                _ => $"Hata: {ex.GetType().Name}"
            };

        }
    }

}
