namespace LazySerializer
{
    public static class SerializerExtension
    {
        /// <summary>
        /// Save Settings
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="path">Path</param>
        public static void SaveSettings(this object obj, string path) => Serializer.Write(path, obj);
    }
}
