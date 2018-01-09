using System;
using System.Collections.Generic;
using Microex.LogServer.Database.Entities;
using Newtonsoft.Json.Linq;

namespace Microex.LogServer.Service.Dtos
{
    public class LoggingDto
    {
        public static LoggingDto CreateFromEntity(LoggingEntity entity)
        {
            return new LoggingDto()
            {
                Tags = entity.Tags,
                Id = entity.Id,
                StatusData = entity.StatusData,
                CreateTime = entity.CreateTime
            };
        }

        public DateTime CreateTime { get; set; }

        public Object StatusData { get; set; }

        public Guid Id { get; set; }

        public Dictionary<string, string> Tags { get; set; }

        public LoggingEntity ToEntity()
        {
            return new LoggingEntity()
            {
                Tags = this.Tags,
                Id = this.Id,
                StatusData = this.StatusData as JObject,
                CreateTime = this.CreateTime
            };
        }
    }
}
