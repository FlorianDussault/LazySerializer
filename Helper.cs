using System.IO;
using System;

namespace LazySerializer
{
    /// <summary>
    /// Helper
    /// </summary>
    internal class Helper
    {
        /// <summary>
        /// Check file name
        /// </summary>
        /// <param name="path">File name</param>
        /// <exception cref="SerializerException">Exception is case of error</exception>
        public static void CheckPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new SerializerException(new ArgumentException(nameof(path)));
            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(path);
            }
            catch (Exception exception)
            {
                throw new SerializerException(exception);
            }
            if (ReferenceEquals(fileInfo, null) || fileInfo.Name.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                throw new SerializerException($"Invalid filename {path}");
            }
        }
    }
}