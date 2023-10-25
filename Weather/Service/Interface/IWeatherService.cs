namespace Weather.Service.Interface
{
    public interface IWeatherService
    {
        Task CreateWeather(Model.WeatherPersist data);

    }
}
