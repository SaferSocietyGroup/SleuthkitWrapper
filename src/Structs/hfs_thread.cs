using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    public struct hfs_thread
    {
        /// <summary>
        /// == kHFSPlusFolderThreadRecord or kHFSPlusFileThreadRecord
        /// </summary>
        ushort rec_type;
        
        /// <summary>
        /// reserved - initialized as zero
        /// </summary>
        ushort res;

        /// <summary>
        /// parent ID for this catalog node
        /// </summary>
        uint parent_cnid;

        /// <summary>
        /// name of this catalog node (variable length)
        /// </summary>
        hfs_uni_str name;

        public hfs_uni_str Name
        {
            get
            {
                return name;
            }
        }
    }
}
