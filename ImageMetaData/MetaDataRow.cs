using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageMetaData
{
   public partial class MetaDataManager
   {
      public class MetaDataRow
      {
         public string RawText { get; set; }
         public string Filename { get; set; }
         public string Description { get; set; }
         public List<string> KeyWords { get; set; }

         public MetaDataRow(string rawText, string imageFolder)
         {
            RawText = rawText;

            string[] bits = rawText.Split('"');

            KeyWords = bits[1].Split(',').ToList();

            string[] nonKeywords = bits[0].Split(',');

            Filename = nonKeywords[0];
            Description = nonKeywords[1];

            string fullFilePath = Path.Combine(imageFolder, Filename);
         }
      }

   }
}
