using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Recorder.Service.Common;
using Recorder.Service.Common.Attributes;

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
        public Status Status { get; set; }

        public List<Record> Records { get; set; }
    }
}
