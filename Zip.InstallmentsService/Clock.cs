using System;

namespace Zip.InstallmentsService;

public class Clock
{
    public virtual DateTime UtcNow()
    {
        var now = DateTime.UtcNow;
        return new DateTime(now.Year, now.Month, now.Day,
            now.Hour, now.Minute, now.Second, DateTimeKind.Utc);
    }
}