using AutoMapper;
using API.Data.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using System.Xml.Serialization;
using CsvHelper;
using OfficeOpenXml;
using System.Text;
using System.Xml;
using CsvHelper.Configuration;
using System.Globalization;

namespace API.Controllers
{
    public class SensorController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SensorController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<String>> RegisterNewSensor(SensorCreateDTO sensorCreateDTO)
        {
            if (await _unitOfWork.SensorRepository.DoesSensorWithNameExist(sensorCreateDTO.SensorId))
            {
                return BadRequest("Sensor already existing");
            }

            Sensor sensor = new Sensor
            {
                SensorId = sensorCreateDTO.SensorId.ToLower(),
                Description = sensorCreateDTO.Description ?? "",
                DisplayOnDashboard = sensorCreateDTO.DisplayOnDashboard,
                DisplayTitle = sensorCreateDTO.DisplayTitle,
                ShortDescription = sensorCreateDTO.ShortDescription,
                ThumbnailPath = sensorCreateDTO.ThumbnailPath,
                SensorModel = sensorCreateDTO.SensorModel,
                LastSeen = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(),

            };

            _unitOfWork.SensorRepository.AddSensorToDatabase(sensor);

            if (!await _unitOfWork.Complete())
            {
                return BadRequest("Error saving Sensor to database");
            }

            return Ok("Registering of sensor complete");
        }

        [HttpGet("get-time")]
        public ActionResult<double> GetTime()
        {
            return Ok(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds());
        }


        [HttpGet("{sensorId}/download/{format}")]
        public async Task<IActionResult> DownloadAllData([FromRoute] string sensorId, string format)
        {
            List<SensorData> data = await _unitOfWork.SensorDataRepository.GetDataDownload(sensorId);

            switch (format?.ToLower())
            {
                case "json":
                    return Ok(data);
                case "xlsx":
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("SensorData");
                        worksheet.Cells["A1"].LoadFromCollection(data, PrintHeaders: true);

                        byte[] fileBytes = package.GetAsByteArray();
                        return File(fileBytes,
                                     "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                     "SensorData.xlsx");
                    }
                case "csv":
                    var builder = new StringBuilder();
                    var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
                    using (var csvWriter = new CsvWriter(new StringWriter(builder), configuration))
                    {
                        var formattedData = data.Select(d => new
                        {
                            SensorId = d.SensorId,
                            Value = d.Value,
                            Time = d.Time.ToString("yyyy-MM-dd HH:mm:ss")
                        });
                        csvWriter.WriteRecords(formattedData);
                    }
                    return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "SensorData.csv");
                case "xml":
                    var serializer = new XmlSerializer(typeof(List<SensorDataFormatted>));
                    using (var sww = new StringWriter())
                    {
                        using (XmlWriter writer = XmlWriter.Create(sww))
                        {
                            var formattedData = data.Select(d => new SensorDataFormatted
                            {
                                SensorId = d.SensorId,
                                Value = d.Value,
                                Time = d.Time.ToString("yyyy-MM-dd HH:mm:ss")
                            }).ToList();
                            serializer.Serialize(writer, formattedData);
                            return File(Encoding.UTF8.GetBytes(sww.ToString()), "application/xml", "SensorData.xml");
                        }
                    }
                default:
                    return BadRequest("Invalid format.");
            }
        }
        public class SensorDataFormatted
        {
            public string SensorId { get; set; }
            public double Value { get; set; }
            public string Time { get; set; }
        }


        [HttpGet("get-sensors")]
        public async Task<ActionResult<AllSensorsDto>> GetAllSensors([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageSize = (size ?? 20);
            int pageNumber = (page ?? 1);

            IEnumerable<Sensor> sensors = await _unitOfWork.SensorRepository.GetSensorsAsync(pageNumber, pageSize);

            return Ok(new AllSensorsDto
            {
                Items = sensors,
                TotalItems = sensors.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("{sensorId}")]
        public async Task<ActionResult<Sensor>> GetSensor([FromRoute] string sensorId)
        {
            Sensor sensor = await _unitOfWork.SensorRepository.GetSensorByIdAsync(sensorId);

            return Ok(sensor);
        }

        [HttpPut("{sensorId}/update/{attr}")]
        public async Task<ActionResult<string>> UpdateSensor([FromRoute] string sensorId, [FromRoute] string attr, [FromBody] UdateSensorDTO udateSensorDTO)
        {
            Sensor sensor = await _unitOfWork.SensorRepository.GetSensorByIdAsync(sensorId);

            if (sensor == null)
            {
                return BadRequest("No Sensor found to update");
            }

            switch (attr)
            {
                case "Description":
                    sensor.Description = udateSensorDTO.newValue;
                    break;
                case "DisplayOnDashboard":
                    sensor.DisplayOnDashboard = udateSensorDTO.newValue.Contains("TRUE") ? true : false;
                    break;
                case "DisplayTitle":
                    sensor.DisplayTitle = udateSensorDTO.newValue;
                    break;
                case "ShortDescription":
                    sensor.ShortDescription = udateSensorDTO.newValue;
                    break;
                case "ThumbnailPath":
                    sensor.ThumbnailPath = udateSensorDTO.newValue;
                    break;
                case "SensorModel":
                    sensor.SensorModel = udateSensorDTO.newValue;
                    break;
                default:
                    break;
            }

            _unitOfWork.SensorRepository.Update(sensor);

            if (!await _unitOfWork.Complete())
            {
                return BadRequest("Error saving Sensor to database");
            }

            return Ok();
        }

        [HttpGet("get-displayed-sensors")]
        public async Task<ActionResult<AllSensorsDto>> GetDisplayedSensors([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageSize = (size ?? 20);
            int pageNumber = (page ?? 1);

            IEnumerable<Sensor> sensors = await _unitOfWork.SensorRepository.GetDisplayedSensorsAsync(pageNumber, pageSize);

            return Ok(new AllDisplayedSensorsDto
            {
                Items = sensors,
                TotalItems = sensors.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpPost("add-data")]
        public async Task<ActionResult<string>> AddSensorData([FromBody] AddSensorDataDto addSensorDataDto)
        {
            // get all sensors
            IEnumerable<Sensor> sensors = await _unitOfWork.SensorRepository.GetSensorsAsync(1, 1000);

            foreach (SensorDataDto sensorDataDto in addSensorDataDto.SensorsData)
            {
                Sensor sensor = sensors.FirstOrDefault(s => s.SensorId == sensorDataDto.SensorId);
                if (sensor == null) continue;

                SensorData sensorDataToSave = new SensorData
                {
                    SensorId = sensorDataDto.SensorId,
                    Value = double.Parse(sensorDataDto.Value),
                    Time = DateTimeOffset.FromUnixTimeMilliseconds((long)double.Parse(addSensorDataDto.Timestamp))
                };

                if (!await _unitOfWork.SensorDataRepository.AddSensorDataToDatabase(sensorDataToSave)) continue;

                sensor.LastSeen = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            }

            if (!await _unitOfWork.Complete())
            {
                return BadRequest("Error saving sensor data");
            }

            return Ok("Success saving sensor data");
        }


    }
}