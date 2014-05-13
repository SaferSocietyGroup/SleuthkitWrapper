using System;
using System.Text;
using System.IO;

namespace SleuthKit
{
    /// <summary>
    /// Presents sleuthkit file content as a .NET BCL Stream.  Handy for interop with other libraries and the BCL itself.  
    /// </summary>
    public class FileStream : BaseStream
    {
        private File file;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="file"></param>
        public FileStream(File file)
        {
            this.file = file;
            this.length = file.Size;
            this.canRead = true;
            this.canSeek = true;
            this.canWrite = false;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int br = 0;
            if (offset == 0)
            {
                br = file.ReadBytes(this.position, buffer, count);
            }
            else
            {
                byte[] somebuf = new byte[count];
                br = file.ReadBytes(this.position, somebuf, count);
                Buffer.BlockCopy(somebuf, 0, buffer, offset, br);
            }
            
            this.position += br;

            return br;
        }
    }

    /// <summary>
    /// Presents sleuthkit disk content as a .NET BCL Stream.  Handy for interop with other libraries and the BCL itself.  
    /// </summary>
    public class DiskImageStream : BaseStream
    {
        private DiskImage disk;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="disk"></param>
        public DiskImageStream(DiskImage disk)
        {
            this.disk = disk;
            this.length = disk.Size;
            this.canRead = true;
            this.canSeek = true;
            this.canWrite = false;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int br = 0;
            if (offset == 0)
            {
                br = disk.ReadBytes(this.position, buffer, count);
            }
            else
            {
                byte[] somebuf = new byte[count];
                br = disk.ReadBytes(this.position, somebuf, count);
                Buffer.BlockCopy(somebuf, 0, buffer, offset, br);
            }

            this.position += br;

            return br;
        }
    }

    /// <summary>
    /// Base class for other streams
    /// </summary>
    public class BaseStream : Stream
    {
        /// <summary>
        /// can be set by subclasses
        /// </summary>
        protected bool canRead = false;

        /// <summary>
        /// can be set by subclasses
        /// </summary>
        protected bool canWrite = false;

        /// <summary>
        /// can be set by subclasses
        /// </summary>
        protected bool canSeek = false;

        /// <summary>
        /// can be set by subclasses
        /// </summary>
        protected long length = 0;

        /// <summary>
        /// can be set by subclasses
        /// </summary>
        protected long position = 0;

        /// <summary>
        /// Gets a value indicating whether this instance can read.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can read; otherwise, <c>false</c>.
        /// </value>
        public override bool CanRead
        {
            get { return canRead; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can seek.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can seek; otherwise, <c>false</c>.
        /// </value>
        public override bool CanSeek
        {
            get { return canSeek; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can write.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can write; otherwise, <c>false</c>.
        /// </value>
        public override bool CanWrite
        {
            get { return canWrite; }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public override long Length
        {
            get { return length; }
        }


        /// <summary>
        /// Flush this instance.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public override long Position
        {
            get
            {
                return position;
            }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// Read the specified buffer, offset and count.
        /// </summary>
        /// <param name='buffer'>
        /// Buffer.
        /// </param>
        /// <param name='offset'>
        /// Offset.
        /// </param>
        /// <param name='count'>
        /// Count.
        /// </param>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return 0;
        }

        /// <summary>
        /// Seek the specified offset and origin.
        /// </summary>
        /// <param name='offset'>
        /// Offset.
        /// </param>
        /// <param name='origin'>
        /// Origin.
        /// </param>
        /// <exception cref='ArgumentException'>
        /// Is thrown when an argument passed to a method is invalid.
        /// </exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            long newpos = 0;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    newpos = offset;
                    break;
                case SeekOrigin.Current:
                    newpos = offset + position;
                    break;
                case SeekOrigin.End:
                    newpos = offset + length;
                    break;
            }
            position = newpos;

            if (position < 0)
            {
                throw new ArgumentException("cant seek to before the start of the stream.");
            }

            return position;
        }

        /// <summary>
        /// Sets the length.
        /// </summary>
        /// <param name='value'> Value.</param> 
        public override void SetLength(long value)
        {
        }

        /// <summary>
        /// Write the specified buffer, offset and count.
        /// </summary>
        /// <param name='buffer'>
        /// Buffer.
        /// </param>
        /// <param name='offset'>
        /// Offset.
        /// </param>
        /// <param name='count'>
        /// Count.
        /// </param>
        public override void Write(byte[] buffer, int offset, int count)
        {
        }
    }
}
