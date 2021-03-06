﻿using System;
using System.ComponentModel.DataAnnotations;
using Recorder.Service.Attributes;
using Recorder.Service.Dto;

namespace Recorder.Service.Entities
{
    public class Record
    {
        /// <summary>
        /// Record Id
        /// </summary>
        [Key]
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
        /// Records current status
        /// </summary>
        [Required]
        public RecordStatus Status { get; set; }

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
