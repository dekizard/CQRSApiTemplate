namespace CQRSApiTemplate.Application.Interfaces;

public interface IDateTimeService
{
    DateTimeOffset Now { get; }
}
