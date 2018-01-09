using Microsoft.Extensions.Options;

namespace Microex.LogServer.Service
{
    public class LoggingServiceOptions:IOptions<LoggingServiceOptions>
    {
        public string ConnectionString { get; set; }
        /// <summary>
        /// The default configured TOptions instance, equivalent to Get(string.Empty).
        /// </summary>
        public LoggingServiceOptions Value => this;
    }
}
