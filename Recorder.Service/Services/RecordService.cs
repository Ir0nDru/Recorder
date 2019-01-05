using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recorder.Service.Entities;

namespace Recorder.Service.Services
{
    public class RecordService
    {
        private readonly CameraService _cameraService;
        private readonly AppDatabaseContext _ctx;

        public RecordService(AppDatabaseContext ctx, CameraService cameraService)
        {
            _ctx = ctx;
            _cameraService = cameraService;
        }

        public Record[] GetRecordsByCameraId(int camId)
        {
            return _ctx.Records.Where(r => r.CameraId == camId).ToArray();
        }

        public void CreateRecord(Record record)
        {
            var camera = _cameraService.GetCamera(record.CameraId);

            if (camera == null)
                throw new ArgumentException($"Corresponding to record camera with id: {record.CameraId} not found.");

            _ctx.Records.Add(record);
            _ctx.SaveChanges();
        }

        public void UpdateRecord(Record record)
        {
            var existing = _ctx.Records.Find(record.Id);
            var camera = _cameraService.GetCamera(record.CameraId);

            if (existing == null)
                throw new ArgumentException($"Record with id: {record.Id} not found.");
            if (camera == null)
                throw new ArgumentException($"Corresponding to record camera with id: {record.CameraId} not found.");

            existing.StartTime = record.StartTime;
            existing.EndTime = record.EndTime;
            existing.CameraId = record.CameraId;
            existing.Description = record.Description;
            
            _ctx.SaveChanges();
        }

        public void DeleteRecord(int id)
        {
            var existing = _ctx.Records.Find(id);

            if (existing == null)
                throw new ArgumentException($"Record with id: {id} not found.");            

            _ctx.Records.Remove(existing);
            _ctx.SaveChanges();
        }
    }
}
