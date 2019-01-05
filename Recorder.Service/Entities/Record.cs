using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Recorder.Service.Attributes;

namespace Recorder.Service.Entities
{
    public class Record
    {
        /// <summary>
        /// Record Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// Record start time
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Record end time
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [DateGreaterThan("StartTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Human readable record description
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// Foreign key to camera        
        /// </summary>
        public int CameraId { get; set; }
        public Camera Camera { get; set; }

    }
}
