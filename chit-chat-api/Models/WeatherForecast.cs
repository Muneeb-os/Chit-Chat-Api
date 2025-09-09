namespace chit_chat_api.Models
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        // commit here 

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
