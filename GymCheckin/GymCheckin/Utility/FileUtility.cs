using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Threading.Tasks;

namespace GymCheckin.Utility
{
    public static class FileUtility
    {

        /// <summary>
        /// Save to File
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool SaveToFile(string fileName, byte[] data)
        {
            try
            {
                System.IO.File.WriteAllBytes(GetFullPath(fileName), data);
            }
            catch
            {
                return false;
            }

            return true;
        }        

        /// <summary>
        /// Appends full path to the 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetFullPath(string file)
        {
            return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), file);
        }

        public static void CleanUp(string file)
        {
            try
            {
                System.IO.File.Delete(GetFullPath(file));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Could not clean up file {file}. The app ate the exception: {ex.ToString()}");
            }
        }

    }
}
