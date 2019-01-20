using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recorder.Service.Dto;
using Recorder.Service.Entities;
using Recorder.Service.Services;

namespace Recorder.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamerasController: ControllerBase
    {
        private readonly CameraService _cameraService;

        public CamerasController(CameraService cameraService)
        {
            _cameraService = cameraService;
        }

        // GET api/cameras
        [HttpGet]
        public ActionResult<Camera[]> GetAllCameras()
        {
            return Ok(_cameraService.GetAllCameras());
        }

        // GET api/cameras/actual/5
        [HttpGet("actual/{delaySeconds}")]
        public ActionResult<Camera[]> GetActualCameras(int delaySeconds)
        {
            return Ok(_cameraService.GetActualCameras(TimeSpan.FromSeconds(delaySeconds)));
        }

        // GET api/cameras/1
        // GET api/cameras/2
        // GET api/cameras/3 - where number is id of a camera
        [HttpGet("{id}")]
        public ActionResult<Camera> GetCamera(int id)
        {
            try
            {
                return Ok(_cameraService.GetCamera(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/cameras
        //
        [HttpPost]
        public ActionResult CreateCamera([FromBody]Camera camera)
        {
            try
            {
                _cameraService.CreateCamera(camera);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //PUT api/cameras
        [HttpPut]
        public ActionResult UpdateCamera(Camera camera)
        {
            try
            {
                _cameraService.UpdateCamera(camera);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //DELETE api/cameras/1
        [HttpDelete("{id}")]
        public ActionResult DeleteCamera(int id)
        {
            try
            {
                _cameraService.DeleteCamera(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
