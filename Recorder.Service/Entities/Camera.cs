using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Recorder.Service.Attributes;
using Recorder.Service.Dto;

namespace Recorder.Service.Entities
{
    public class Camera
    {
        /// <summary>
        /// Camera Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// Camera IP-address
        /// </summary>
        [Required, MaxLength(15)]        
        [IpAddress]
        public string IpAddress { get; set; }

        /// <summary>
        /// Camera MAC-address
        /// </summary>
        [MaxLength(17)]
        [MacAddress]
        public string MacAddress { get; set; }

        /// <summary>
        /// Camera current status
        /// </summary>
        [Required]
        public CameraStatus Status { get; set; }

        /// <summary>
        /// Human readable description to camera
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        public List<Record> Records { get; set; }
    }
}
