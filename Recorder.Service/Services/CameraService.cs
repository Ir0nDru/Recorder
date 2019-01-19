using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recorder.Service.Dto;
using Recorder.Service.Entities;
using Recorder.Service.Helpers;
using Remotion.Linq.Clauses;

namespace Recorder.Service.Services
{
    public class CameraService
    {
        private readonly RecordService _recordService;
        private readonly AppDatabaseContext _ctx;

        public CameraService(AppDatabaseContext ctx, RecordService recordService)
        {
            _ctx = ctx;
            _recordService = recordService;
        }

        public Camera[] GetAllCameras()
        {
            return _ctx.Cameras.Include(c => c.Records).ToArray();
        }

        /// <summary>
        /// Returns all cameras with actual records to play
        /// </summary>
        /// <param name="actualTimeSpan">Timespan of actuality of a record</param>
        /// <returns></returns>
        public Camera[] GetActualCameras(TimeSpan actualTimeSpan)
        {
            var delayTime = DateTime.Now + actualTimeSpan;

            var cameras = _ctx.Cameras.Select(c => c);
            var records = _ctx.Records.Where(r => r.StartTime <= delayTime && r.EndTime > delayTime && r.Status == RecordStatus.Awaits);
            foreach (var camera in cameras)
            {
                camera.Records = records.Where(r => r.CameraId == camera.Id).ToList();
            }
            return cameras.ToArray();
        }

        public Camera GetCamera(int id)
        {
            var camera = _ctx.Cameras.Include(c => c.Records).FirstOrDefault(c => c.Id == id);

            if (camera == null)
                throw new ArgumentException($"Camera with id: {id} not found.");

            return camera;
        }

        public void CreateCamera(Camera camera)
        {
            var existing = _ctx.Cameras.FirstOrDefault(c => c.IpAddress == camera.IpAddress);

            if (existing != null)
                throw new ArgumentException($"Camera with IP: {existing.IpAddress} already exists");

            camera.Id = 0;
            camera.Status = CameraStatus.Offline;

            _ctx.Add(camera);
            _ctx.SaveChanges();
        }

        public void UpdateCamera(Camera camera)
        {
            var existing = _ctx.Cameras.Find(camera.Id);

            if (existing == null)
                throw new ArgumentException($"Camera with id: {camera.Id} not found.");

            existing.IpAddress = camera.IpAddress;
            existing.MacAddress = camera.MacAddress;
            existing.Status = (camera.Status != CameraStatus.Offline && camera.Status != CameraStatus.Online) ? CameraStatus.Offline : camera.Status;
            existing.Description = camera.Description;            

            _ctx.SaveChanges();            
        }

        public void DeleteCamera(int id)
        {
            var existing = _ctx.Cameras.Find(id);

            if (existing == null)
                throw new ArgumentException($"Camera with id: {id} not found.");            
            //_ctx.Records.RemoveRange(_recordService.GetRecordsByCameraId(id));
            _ctx.Cameras.Remove(existing);
            _ctx.SaveChanges();
        }
    }
}
