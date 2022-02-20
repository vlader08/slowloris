// See https://aka.ms/new-console-template for more information

using System.Text.Json;

string host = "localhost";
int port = 80;
string ep = "/WeatherForecast/post";
var json = JsonSerializer.Serialize(new { Content = "my text" });

var packet_delay = TimeSpan.FromMilliseconds(500);  

NetworkHelper.SendSlowPost(host, port, ep, json, packet_delay);