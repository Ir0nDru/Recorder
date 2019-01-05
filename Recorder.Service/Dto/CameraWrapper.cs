using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recorder.Service.Entities;

namespace Recorder.Service.Dto
{
    /// <summary>
    /// Contains camera with list of its own records
    /// </summary>
    public class CameraWrapper
    {
        /// <summary>
        /// Camera itself
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// Array of camera records (schedule)
        /// </summary>
        public Record[] Records { get; set; }
    }
}
