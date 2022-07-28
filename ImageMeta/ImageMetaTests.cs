﻿using ImageMetaData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Imaging;
using static ImageMetaData.MetaDataManager;

namespace ImageMetaDataTests
{
   [TestClass]
   public class UnitTest1
   {
      [TestMethod]
      public void UpdateCatalog()
      {
         //arrange
         string imageFolder = @"E:\GitHub\Shaneinsweden\shop\product\ScrabbleKitchen\shutter\Uploaded\french5000words";
         string keyWordFile = @"E:\GitHub\Shaneinsweden\shop\product\ScrabbleKitchen\shutter\Uploaded\french5000words-s1.csv";
 
         //act this method works for approx 80 images at a time then crashes

         MetaDataManager.UpdateFolderImagesMetadata(imageFolder, keyWordFile);
      }


   }
}
