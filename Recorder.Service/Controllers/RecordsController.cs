using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recorder.Service.Entities;
using Recorder.Service.Services;

namespace Recorder.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly RecordService _recordService;

        public RecordsController(RecordService recordService)
        {
            _recordService = recordService;
        }
      
        //POST api/records
        //{
        //  "startTime": "2019-01-07T23:25:00",
        //  "endTime": "2019-01-07T23:26:00",
        //  "description": "some text",
        //  "cameraId": 1
        //}    
        [HttpPost]
        public ActionResult CreateRecord(Record record)
        {
            try
            {
                _recordService.CreateRecord(record);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public ActionResult UpdateRecord(Record record)
        {
            try
            {
                _recordService.UpdateRecord(record);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //DELETE api/records/1
        [HttpDelete("{id}")]
        public ActionResult DeleteRecord(int id)
        {
            try
            {
                _recordService.DeleteRecord(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}