using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microex.Common.Mvc.TypeConverters;
using Microex.LogServer.Service;
using Microex.LogServer.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Microex.LogServer.Api.Controllers
{
    [Route("logs")]
    [Authorize]
    public class LogsController : Controller
    {
        private readonly LoggingService _loggingService;
        public LogsController(LoggingService loggingService)
        {
            _loggingService = loggingService;
        }
        // GET api/values
        [HttpGet("{startTime}/{endTime}")]
        public List<LoggingDto> Get(DateTime startTime, DateTime endTime, [TypeConverter(typeof(JObjectConverter))] JObject tagsJson)
        {
            try
            {
                var tags = tagsJson.ToObject<Dictionary<string, string>>();
                return _loggingService.GetLogs(startTime, endTime, tags);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        
        public LoggingDto Get(Guid id)
        {
            try
            {
                return _loggingService.GetLog(id);
            }
            catch (InvalidOperationException e)
            {
                NotFound();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //// POST api/values
        //[HttpPost("")]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        [HttpPut("")]
        public LoggingDto Put([FromBody]LoggingDto loggingDto)
        {
            try
            {
                var result = _loggingService.UpsertLog(loggingDto);
                Created(Url.Action("Get", new { id = result.Id }), result);
                return result;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            try
            {
                var result = _loggingService.DeleteLog(id);
                if (result)
                {
                    NoContent();
                }
                else
                {
                    NotFound();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}
