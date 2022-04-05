using System;
using CQRSApiTemplate.Application.Interfaces;

namespace CQRSApiTemplate.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}
