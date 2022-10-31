using System;
using System.IO;
using System.Xml.Serialization;

namespace LazySerializer
{
    /// <summary>
    /// Serializer
    /// </summary>
    public static class Serializer
    {
        #region Read

        /// <summary>
        /// Load XML
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="path">File path</param>
        /// <returns>Object</returns>
        public static T Read<T>(string path) => Read<T>(path, out _, out _);

        /// <summary>
        /// Load XML
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="path">File path</param>
        /// <param name="createDateTime">Create Date</param>
        /// <param name="lastWriteTime">Last Change Date</param>
        /// <returns>Object</returns>
        /// <exception cref="SerializerException"></exception>
        public static T Read<T>(string path, out DateTime createDateTime, out DateTime lastWriteTime)
        {
            Helper.CheckPath(path);

            try
            {
                FileInfo fileInfo = new(path);

                createDateTime = fileInfo.CreationTime;
                lastWriteTime = fileInfo.LastWriteTime;

                if (!fileInfo.Exists)
                    return default!;

                XmlSerializer xmlSerializer = new(typeof(T));
                using FileStream fileStream = new(path, FileMode.Open, FileAccess.Read);
                return (T)xmlSerializer.Deserialize(fileStream);
            }
            catch (Exception exception)
            {
                throw new SerializerException(exception);
            }
        }

        #endregion

        #region Write

        /// <summary>
        /// Save XML
        /// </summary>
        /// <param name="obj">Object to save</param>
        /// <param name="path">File path</param>
        public static void Write(string path, object obj)
        {
            if (obj == null) throw new SerializerException(new ArgumentException(nameof(obj)));
            Helper.CheckPath(path);

            try
            {
                XmlSerializer xmlSerializer = new(obj.GetType());
                using (StreamWriter streamWriter = new(path))
                {
                    xmlSerializer.Serialize(streamWriter, obj);
                }
            }
            catch (Exception exception)
            {
                throw new SerializerException(exception);
            }
        }

        #endregion

    }
}
