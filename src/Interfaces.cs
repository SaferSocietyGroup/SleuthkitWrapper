using System.IO;

namespace SleuthKit
{
    /// <summary>
    /// An item that has content.  Size is provided separately because sometimes getting a stream to content is expensive, and it is nice to provide the stream length without opening the stream.  It is worth noting, however, that sometimes IContent implementations will specify a Size that is not consistent with the amount of bytes you can read from the <c>Stream</c> returned by <c>OpenRead</c>.  Sometimes we don't know that content is unavailable until the caller is reading it, so take the <c>Size</c> with a grain of salt.
    /// </summary>
    interface IContent
    {
        /// <summary>
        /// The length of the content, in bytes.  See remarks for the IContent interface for more information about this property.  Basically it is here so that you don't need to call <c>OpenRead</c> just to get the stream length.
        /// </summary>
        long Size { get; }

        /// <summary>
        /// Opens the content for reading.  Since this library is designed for reading disk images, there isn't going to be an <c>OpenWrite</c>.
        /// </summary>
        /// <returns></returns>
        Stream OpenRead();
    }
}
