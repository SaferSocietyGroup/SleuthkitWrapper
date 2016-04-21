using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleuthKit
{
    public class VolumeInformation
    {
        public long Address { get; set; }
        public String Description { get; set; }
        public VolumeFlags Flags { get; set; }
        public bool IsAllocated { get; set; }
        public long Length { get; set; }
        public long Offset { get; set; }
        public long SectorLength { get; set; }
        public long SectorOffset { get; set; }
    }
}
