using AutoMapper;
using API.Data.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using System.Diagnostics;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class AktorController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _mqttOptions;

        public AktorController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _mqttClient = new MqttFactory().CreateMqttClient();
            _mqttOptions = new MqttClientOptionsBuilder()
                .WithTcpServer("localhost") // Use appropriate hostname/IP and port
                .Build();
        }

        [HttpPost("register")]
        public async Task<ActionResult<String>> RegisterAktor(AktorCreateDTO aktorCreateDTO)
        {
            if (await _unitOfWork.AktorRepository.DoesAktorWithIdExist(aktorCreateDTO.AktorId))
            {
                return BadRequest("Sensor already existing");
            }

            Aktor aktor = new Aktor
            {
                AktorId = aktorCreateDTO.AktorId,
                AktorName = aktorCreateDTO.AktorName,
                Description = aktorCreateDTO.Description ?? "",
                LastSeen = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),
                AktorType = aktorCreateDTO.AktorType
            };

            _unitOfWork.AktorRepository.AddAktorToDatabase(aktor);

            if (!await _unitOfWork.Complete())
            {
                return BadRequest("Error saving Aktor to database");
            }

            return Ok("Registering of Aktor complete");
        }

        [HttpGet("get-aktors")]
        public async Task<ActionResult<AllAktorsDTO>> GetAllAktors([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageSize = (size ?? 20);
            int pageNumber = (page ?? 1);

            IEnumerable<Aktor> aktors = await _unitOfWork.AktorRepository.GetAktorsAsync(pageNumber, pageSize);

            return Ok(new AllAktorsDTO
            {
                Items = aktors,
                TotalItems = aktors.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpPut("change-value")]
        public async Task<ActionResult> ChangeValueAktor([FromQuery] string aktorId, [FromQuery] string valueToChange, [FromQuery] string newValue)
        {
            Aktor aktor = await _unitOfWork.AktorRepository.GetAktorByIdAsync(aktorId);

            if (aktor == null)
            {
                return BadRequest("Aktor does not exist");
            }

            try
            {
                MqttClientConnectResult clientConnectResult = await _mqttClient.ConnectAsync(_mqttOptions, CancellationToken.None);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error while connecting to MQTT broker: " + ex.Message);
            }

            if (!_mqttClient.IsConnected)
            {
                return BadRequest("MQTT not connected!");
            }

            var payload = $"{{\"{valueToChange}\":\"{newValue}\"}}";


            var message = new MqttApplicationMessageBuilder()
                .WithTopic($"zigbee2mqtt/{aktorId}/set")
                .WithPayload(payload)
                .Build();

            MqttClientPublishResult publishResult = await _mqttClient.PublishAsync(message, CancellationToken.None);

            if (!publishResult.IsSuccess)
            {
                return BadRequest("Error publishing MQTT message");
            }

            return Ok("Aktor value changed successfully");

            // string command = $"mosquitto_pub -t 'zigbee2mqtt/{aktorId}/set' -m '{{\"{valueToChange}\":\"{newValue}\"}}'";
            // var processInfo = new ProcessStartInfo("bash", "-c " + command)
            // {
            //     RedirectStandardOutput = true,
            //     UseShellExecute = false,
            //     CreateNoWindow = false
            // };

            // var process = Process.Start(processInfo);

            // process.WaitForExit();

            // string output = process.StandardOutput.ReadToEnd();
            // Console.WriteLine(output);
        }
    }
}