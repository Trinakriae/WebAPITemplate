using Newtonsoft.Json;
using System;
using System.IO;


namespace WebAPITemplate.Shared.Utils
{
    public static class FileUtils
    {
        public static void WriteJSONFile(string path, string fileName, object jsonObject)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.WriteAllText($"{path}\\{fileName}", JsonConvert.SerializeObject(jsonObject));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
