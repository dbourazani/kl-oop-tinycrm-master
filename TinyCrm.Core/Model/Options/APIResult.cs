using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public class APIResult<T>
    {
        public string ErrorText { get; set; }
        public T Data { get; set; }
        public StatusCode ErrorCode { get; set; }
    }
}
