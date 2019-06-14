using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bdp.Models
{
    public partial class Result<T>
    {
        public int status { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }

    public partial class ResultData<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
    }
}
