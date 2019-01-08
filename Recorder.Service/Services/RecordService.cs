using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recorder.Service.Entities;

namespace Recorder.Service.Services
{
    public class RecordService
    {        
        private readonly AppDatabaseContext _ctx;

        public RecordService(AppDatabaseContext ctx)
        {
            _ctx = ctx;            
        }        

        public void CreateRecord(Record record)
        {
            if (_ctx.Records.Any(r => r.StartTime == record.StartTime && r.EndTime == record.EndTime && r.CameraId == record.CameraId))
                throw new ArgumentException($"Attempted to create duplicate record");
            record.Id = 0;
            _ctx.Records.Add(record);
            _ctx.SaveChanges();
        }

        public void UpdateRecord(Record record)
        {
            var existing = _ctx.Records.Find(record.Id);

            if (existing == null)
                throw new ArgumentException($"Record with id: {record.Id} not found.");
            if (!_ctx.Cameras.Any(c => c.Id == record.CameraId))
                throw new ArgumentException($"Corresponding to record camera with id: {record.CameraId} not found.");
            if (_ctx.Records.Any(r => r.StartTime == record.StartTime && r.EndTime == record.EndTime && r.CameraId == record.CameraId))
                throw new ArgumentException($"Attempted to create duplicate record");

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
