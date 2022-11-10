using System;

namespace Zip.InstallmentsService;

public class GuidProvider
{
    public virtual Guid NewGuid()
    {
        return Guid.NewGuid();
    }
}