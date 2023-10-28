namespace Weather.Service.Interface
{
    public interface IWeatherService
    {
        Task<Model.Weather> GetWeatherById(Guid id);
        Task UpdateWeather(Model.WeatherPersist data);
        Task DeleteWeather(Model.Weather data);

    }
}
