using Core.Interfaces.Service;
using Core.Common.Enums;
using Core.Common.Helpers;
using Core.Entities;
using Core.Interfaces;
using MaxMind.GeoIP2.Model;

namespace Application.Events.Handlers
{
    internal class LogEventHandler : IEventHandler<LogEvent>
    {
        private readonly ILogService _logService;

        public LogEventHandler(ILogService logService)
        {
            _logService = logService;
        }
        public async Task HandleAsync(LogEvent @event)
        {

            if (@event.Type == LogType.Authorize)
            {
                var location = await IpHelper.GetLocationFromIpAsync(@event.Author) ?? "Belirsiz";

                await _logService.AddAsync(new Log
                {
                    Message = $"{location} Konumundan {@event.Message}",
                    Type = (int)LogType.Authorize,
                });

                return;
            }

            await _logService.AddAsync(new Log
            {
                Message = $"{@event.Author} Tarafından {@event.Message}",
                Type = (int)@event.Type,
            });
        }
    }
}
