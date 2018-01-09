using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microex.Common.Abstractions.Entity;
using Microex.Common.Extensions;
using Newtonsoft.Json.Linq;

namespace Microex.LogServer.Database.Entities
{
    public class LoggingEntity:IEntity
    {
        [NotMapped]
        public JObject StatusData { get; set; }
        [NotMapped]
        public Dictionary<string,string> Tags { get; set; }

        public string _Tags
        {
            get => this.Tags?.ToJson();
            set => this.Tags = value?.ToObject<Dictionary<string, string>>();
        }

        public string _StatusData
        {
            get => this.StatusData?.ToJson();
            set => this.StatusData = value?.ToObject<JObject>();
        }

        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        [Obsolete]
        [NotMapped]
        DateTime? IEntity.LastUpdateTime { get; set; }
    }
}
