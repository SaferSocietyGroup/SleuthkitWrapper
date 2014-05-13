using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SleuthKit
{
    public class NtfsCompressionException : IOException
    {
        public NtfsCompressionException(string message)
            : base(message)
        { }
    }
}
