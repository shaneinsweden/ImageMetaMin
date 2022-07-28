using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMetaData
{
   public partial class MetaDataManager
   {

      public static void UpdateFolderImagesMetadata(string imageFolder, string keyWordFile)
      {
         JpegMetadataAdapter jpeg = null;

         if (!Directory.Exists(imageFolder))
            throw new DirectoryNotFoundException(imageFolder);
         if (!File.Exists(keyWordFile))
            throw new DirectoryNotFoundException(keyWordFile);

         List<string> imagesFilename = GetJpgFilenamesInFolder(imageFolder);

         List<string> metadataRows = File.ReadAllLines(keyWordFile).ToList();

         foreach (string metaRow in metadataRows.Skip(1))
         {
            if (string.IsNullOrEmpty(metaRow))
            {
               continue;
            }
            MetaDataRow row = new MetaDataRow(metaRow, imageFolder);

            string fullPath = Path.Combine(imageFolder, row.Filename);

            if (!File.Exists(fullPath))
               continue;

            jpeg = new JpegMetadataAdapter(fullPath);

            ReadOnlyCollection<string> newKeyWords = new ReadOnlyCollection<string>(row.KeyWords.Take(49).ToList());
            string description = row.Description;
            description = description.Replace(",", " ");
            description = description.Replace("  ", " ");
            jpeg.Metadata.Keywords = newKeyWords;

            jpeg.Metadata.Title = description;
            List<string> authorList = new List<string>() { "The Author" };
            jpeg.Metadata.Author = new ReadOnlyCollection<string>(authorList);
            jpeg.Save();            
            jpeg = null;

         }
      }

      public static List<string> GetJpgFilenamesInFolder(string folderPath)
      {
         if (!Directory.Exists(folderPath))
            throw new InvalidDataException("folder " + folderPath + " does not exist");
         DirectoryInfo d = new DirectoryInfo(folderPath);
         FileInfo[] Files = d.GetFiles("*.jpg");
         List<string> filenameList = new List<string>();
         foreach (FileInfo file in Files)
         {
            filenameList.Add(file.Name);
         }
         return filenameList;
      }

   }
}
