namespace Weather.Service.Interface
{
    public interface IWeatherService
    {
        Task UpdateWeather(Model.WeatherPersist data);
        Task DeleteWeather(Model.Weather data);

    }
}
