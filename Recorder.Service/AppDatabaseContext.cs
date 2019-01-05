using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recorder.Service.Entities;

namespace Recorder.Service
{
    public class AppDatabaseContext: DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options): base(options)
        {            
        }

        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}
