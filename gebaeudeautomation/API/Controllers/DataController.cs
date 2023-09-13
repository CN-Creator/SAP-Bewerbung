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
    public class DataController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DataController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{sensorId}/get-last-day")]
        public async Task<ActionResult<AllSensorsDto>> GetLastDay([FromRoute] string sensorId)
        {
            IEnumerable<SensorData> sensorData = await _unitOfWork.SensorDataRepository.GetLastDay(sensorId);

            return Ok(sensorData);
        }
    }
}