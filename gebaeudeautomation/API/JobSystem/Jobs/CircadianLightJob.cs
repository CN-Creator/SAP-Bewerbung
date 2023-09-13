using Newtonsoft.Json;
using Quartz;
using MQTTnet;
using MQTTnet.Client;
using System;

namespace API.JobSystem.Jobs
{
    //this class handles the MQTT messaging internally maybe in future versions we should use a handler class that
    //should be used whenever somewhere mqtt is required
    public class CircadianLightJob : IJob
    {
        private IMqttClient _mqttClient;
        private MqttClientOptions _mqttOptions;

        public async Task Execute(IJobExecutionContext context)
        {

            DateTimeOffset date = context.FireTimeUtc;
            int hour = date.Hour + 2;

            string jsonContent = File.ReadAllText("./JobSystem/Jobs/CircadianLight.json");
            LightSettings settings = JsonConvert.DeserializeObject<LightSettings>(jsonContent);

            var timeData = settings.CircadianLight.Find(d => d.Time == $"{hour}:00");
            //map from max and min value to a range of [0,254]
            var mappedBrightness = (int)MathF.Round((float)(timeData.Brightness / 2000f * 254f));
            _mqttClient = new MqttFactory().CreateMqttClient();
            _mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost") // Use appropriate hostname/IP and port
                .Build();
            try
            {
                MqttClientConnectResult clientConnectResult =
                    await _mqttClient.ConnectAsync(_mqttOptions, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to MQTT broker: " + ex.Message);
                return;
            }

            if (!_mqttClient.IsConnected)
            {
                Console.WriteLine("No Connection to MQTT Broker established");
                return;
            }

            var payload = $"{{\"brightness\": {timeData.Brightness}, \"color_temp\": \"{timeData.ColorTemperature}\", \"transition\": 5}}";

            var jobData = context.JobDetail.JobDataMap;
            var aktorId = jobData.GetString("aktorId");

            var message = new MqttApplicationMessageBuilder()
                .WithTopic($"zigbee2mqtt/{aktorId}/set")
                .WithPayload(payload)
                .Build();

            MqttClientPublishResult publishResult = await _mqttClient.PublishAsync(message, CancellationToken.None);

            if (!publishResult.IsSuccess)
            {
                Console.WriteLine("Error publishing MQTT message");
            }
        }
    }

    public class LightSetting
    {
        public string Time { get; set; }
        public int ColorTemperature { get; set; }
        public double Brightness { get; set; }
    }

    public class LightSettings
    {
        public List<LightSetting> CircadianLight { get; set; }
    }

}