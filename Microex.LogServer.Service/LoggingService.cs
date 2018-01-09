using System;
using System.Collections.Generic;
using System.Linq;
using Microex.LogServer.Database;
using Microex.LogServer.Service.Dtos;
using Z.EntityFramework.Plus;

namespace Microex.LogServer.Service
{
    public class LoggingService
    {
        private readonly LoggingDbContext _dbContext;

        public LoggingService(LoggingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<LoggingDto> GetLogs(DateTime startTime, DateTime endTime, Dictionary<string, string> tags)
        {
            var rawResult = this._dbContext.LoggingEntities.Where(x => x.CreateTime > startTime && x.CreateTime < endTime).ToList();
            foreach (var pair in tags)
            {
                rawResult = rawResult.Where(x => x.Tags.GetValueOrDefault(pair.Key) == pair.Value.ToString()).ToList();
            }
            return rawResult.Select(x=> LoggingDto.CreateFromEntity(x)).ToList();
        }

        public LoggingDto GetLog(Guid id)
        {
            return LoggingDto.CreateFromEntity(_dbContext.LoggingEntities.First(x => x.Id == id));
        }

        public LoggingDto UpsertLog(LoggingDto dto)
        {
            if (dto.Id == default(Guid))
            {
                dto.Id = Guid.NewGuid();
            }
            var entity = dto.ToEntity();
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return dto;
        }

        public bool DeleteLog(Guid id)
        {
            var count = _dbContext.LoggingEntities.Where(x => x.Id == id).Delete();
            return count != 0;
        }
    }
}
