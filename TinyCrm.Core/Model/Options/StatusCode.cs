using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public enum StatusCode
    {
        NotFound=404,
        InterServiceError=500,
        BadRequest = 403,
        Success = 200
    }
}
