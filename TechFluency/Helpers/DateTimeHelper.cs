using System.Runtime.InteropServices;

namespace TechFluency.Helpers
{
    public static class DateTimeHelper
    {
        // Identificador do fuso horário de Brasília (dependendo do SO)
        private static readonly string TimeZoneId =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "E. South America Standard Time"
                : "America/Sao_Paulo";

        // A variável TimeZoneInfo é carregada somente quando necessário
        private static TimeZoneInfo BrasiliaTimeZone => TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);

        // Converte a data UTC para o horário de Brasília
        public static DateTime ToBrasiliaTime(DateTime utcDate)
        {
            if (BrasiliaTimeZone == null)
                throw new InvalidOperationException("Brasília Time Zone not found.");
            return TimeZoneInfo.ConvertTimeFromUtc(utcDate, BrasiliaTimeZone);
        }

        // Converte o horário de Brasília para UTC
        public static DateTime ToUtcTime(DateTime brasiliaDate)
        {
            if (BrasiliaTimeZone == null)
                throw new InvalidOperationException("Brasília Time Zone not found.");
            return TimeZoneInfo.ConvertTimeToUtc(brasiliaDate, BrasiliaTimeZone);
        }

        // Retorna o início do dia em Brasília (00:00)
        public static DateTime StartOfDayBrasilia(DateTime brasiliaDate)
        {
            var brasiliaTime = ToBrasiliaTime(brasiliaDate);
            return new DateTime(brasiliaTime.Year, brasiliaTime.Month, brasiliaTime.Day, 0, 0, 0);
        }

        // Retorna o fim do dia em Brasília (23:59:59)
        public static DateTime EndOfDayBrasilia(DateTime brasiliaDate)
        {
            var brasiliaTime = ToBrasiliaTime(brasiliaDate);
            return new DateTime(brasiliaTime.Year, brasiliaTime.Month, brasiliaTime.Day, 23, 59, 59);
        }

        // Retorna o próximo dia de revisão em Brasília (com base na repetição)
        public static DateTime NextReviewDateBrasilia(DateTime brasiliaDate, int repetitionInterval)
        {
            var brasiliaTime = ToBrasiliaTime(brasiliaDate);
            return brasiliaTime.Date.AddDays(repetitionInterval);
        }
    }
}
