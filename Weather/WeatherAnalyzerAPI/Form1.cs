using System;
using System.IO;
using System.IO.Ports;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WeatherAnalyzerAPI
{
	public partial class Form1 : Form
	{
		private SerialPort serialPort;
		String staticUrl = "http://localhost:7109/api/weather";

		public Form1()
		{
			InitializeComponent();
			timer1.Interval = 2000;
			timer1.Start();

			serialPort = new SerialPort("COM3", 9600);

			try
			{
				serialPort.Open();
			}
			catch
			{
				Console.WriteLine("Unbale to Open COM port - check it's not in use.");
			}
		}

		private async  void timer1_Tick(object sender, EventArgs e)
		{
			string data = "";
			DateTime startTIme = DateTime.UtcNow;

			Weather weather = new Weather();

			while (!weather.Temperature.HasValue || !weather.Humidity.HasValue)
			{
				if (!serialPort.IsOpen) throw new ApplicationException("Error couldnt find the port");
				data = serialPort.ReadLine();
				if (!string.IsNullOrEmpty(data))
				{
					Console.WriteLine(data);
					if(data.StartsWith("H:"))
					{
						data.Substring(2);
						if (float.TryParse(data.Substring(2), out float humidity))
						{
							Console.WriteLine("Humidity = " + humidity);
							weather.Humidity = humidity;
						}

					}else if(data.StartsWith("T:"))
					{
						
						if (float.TryParse(data.Substring(2), out float temperature))
						{
							Console.WriteLine("Temperature = " + temperature);
							weather.Temperature = temperature;
						}
					}

				}
			}
			textBox1.Text += "Temperature is:" + weather.Temperature.Value + " C" + Environment.NewLine;
			textBox1.Text += "Humidity is:" + weather.Humidity.Value + " C" + Environment.NewLine;

			var serializer = JsonSerializer.Serialize<Weather>(weather);

			using (var HttpClient = new HttpClient())
			{
				try
				{
					var content = new StringContent(serializer, Encoding.UTF8, "application/json");

					HttpResponseMessage response = await HttpClient.PostAsync("http://localhost:5240/weather/update", content);


					if (response.IsSuccessStatusCode)
					{
						Console.WriteLine("Data sent successfully.");
					}
					else
					{
						Console.WriteLine("Failed to send data. Status code: " + response.StatusCode);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occurred: " + ex.Message);
				}
			}

		}
	}

	public class Weather
	{
		public float? Temperature { get; set; }
		public float? Humidity { get; set; }
	}
}
