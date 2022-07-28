using System.IO;
using System.Windows.Media.Imaging;

namespace ImageMetaData
{
   public partial class MetaDataManager
   {
      public class JpegMetadataAdapter
      {
         private readonly string path;
         private BitmapFrame frame;
         public readonly BitmapMetadata Metadata;

         public JpegMetadataAdapter(string path)
         {
            this.path = path;
            frame = getBitmapFrame(path);
            Metadata = (BitmapMetadata)frame.Metadata.Clone();
         }

         public JpegMetadataAdapter(string path, bool noMemory)
         {
            this.path = path;
            MemoryStream memoryStream = new MemoryStream();

            byte[] fileBytes = File.ReadAllBytes(this.path);
            memoryStream.Write(fileBytes, 0, fileBytes.Length);
            memoryStream.Position = 0;
            BitmapSource img = BitmapFrame.Create(memoryStream);
            Metadata = (BitmapMetadata)img.Metadata.Clone();
            frame = getBitmapFrame(path);
         }
         public void Save()
         {
            SaveAs(path);
         }

         public void SaveAs(string path)
         {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(frame, frame.Thumbnail, Metadata, frame.ColorContexts));
            using (Stream stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite))
            {
               encoder.Save(stream);
            }
         }

         private BitmapFrame getBitmapFrame(string path)
         {
            BitmapFrame imageFrame = null;
            BitmapDecoder decoder = null;
            using (Stream stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
               decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
               imageFrame = decoder.Frames[0];
               decoder = null;
            }
            return imageFrame;
         }
      }

   }
}
