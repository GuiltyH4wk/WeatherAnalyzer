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
		String staticUrl = "http://localhost:5240/weather";

		public Form1()
		{
			InitializeComponent();
			timer1.Interval = 20 *1000;
			timer1.Start();

			serialPort = new SerialPort("COM5", 9600);

			try
			{
				serialPort.Open();
			}
			catch
			{
				Console.WriteLine("Unbale to Open COM port - check it's not in use.");
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (serialPort.IsOpen) // Check if the serial port is open
			{
				serialPort.Close(); // Close the serial port
				Console.WriteLine("Serial port closed.");
			}
			base.OnFormClosing(e); // Call the base class method
		}

		private async void timer1_Tick(object sender, EventArgs e)
		{
			Console.WriteLine("Timer tick at: " + DateTime.UtcNow);

			Weather weather = this.ReadWeatherData();

			textBox1.Text += "Temperature is: " + weather.Temperature.Value + " C" + Environment.NewLine;
			textBox1.Text += "Humidity is: " + weather.Humidity.Value + " C" + Environment.NewLine;

			var serializer = JsonSerializer.Serialize<Weather>(weather);

			using (var HttpClient = new HttpClient())
			{
				try
				{
					var content = new StringContent(serializer, Encoding.UTF8, "application/json");

					HttpResponseMessage response = await HttpClient.PostAsync(staticUrl + "/update", content);


					if (response.IsSuccessStatusCode)
					{
						Console.WriteLine("\nData sent successfully. At time : " + DateTime.UtcNow + '\n');
					}
					else
					{
						Console.WriteLine("\nFailed to send data. Status code: " + response.StatusCode + '\n');
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("/nAn error occurred: " + ex.Message + '\n');
				}
			}
			Console.WriteLine("Finished processing at: " + DateTime.UtcNow);
		}
		private Weather ReadWeatherData()
		{

			String data = "";
			DateTime startTIme = DateTime.UtcNow;


			Weather weather = new Weather();

			while (!weather.Temperature.HasValue || !weather.Humidity.HasValue)
			{
				if (!serialPort.IsOpen) throw new ApplicationException("Error couldnt find the port");
				data = serialPort.ReadLine();
				if (!String.IsNullOrEmpty(data))
				{
					Console.WriteLine(data);
					if (data.StartsWith("H:"))
					{
						data.Substring(2);
						if (float.TryParse(data.Substring(2), out float humidity))
						{
							Console.WriteLine("Humidity = " + humidity);
							weather.Humidity = humidity;
						}

					}
					else if (data.StartsWith("T:"))
					{

						if (float.TryParse(data.Substring(2), out float temperature))
						{
							Console.WriteLine("Temperature = " + temperature);
							weather.Temperature = temperature;
						}
					}

				}
			}

			return weather;
		}
	}



	public class Weather
	{
		public float? Temperature { get; set; }
		public float? Humidity { get; set; }
	}
}
