using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace Xktec.hqfq.Common
{
    public class FileUpload
    {
        public static string Upload(Guid id, HttpPostedFileBase file, string serverMapPath)
        {

            var fileName = id.ToString();
            var fileExtentionName = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var fileFullName = fileName + fileExtentionName;
            if (serverMapPath.Last() != '\\')
                serverMapPath += '\\';
            if (!Directory.Exists(serverMapPath))
            {
                Directory.CreateDirectory(serverMapPath);
            }
            file.SaveAs(serverMapPath + fileFullName);
            return fileFullName;
        }
    }
    public class ImageHandler
    {
      
        public static byte[] GenerateThumbnail(Stream imageStream, int thumbnailWidth = 48, int thumbnailHeight = 48)
        {
            System.Drawing.Image original_image = null;
            original_image = System.Drawing.Image.FromStream(imageStream);
            return DrawingThumbnaila(original_image, thumbnailWidth, thumbnailHeight);
        }
        public static byte[] GenerateThumbnail(string absolutePathOrImageUri, int thumbnailWidth = 48, int thumbnailHeight = 48)
        {
            System.Drawing.Image original_image = null;
            System.Net.WebClient webClinet = new System.Net.WebClient();
            Stream imageStream = null;
            if (string.IsNullOrWhiteSpace(absolutePathOrImageUri)) throw new ArgumentException("地址错误");
            try
            {
                original_image = GetBitMapFromFile(absolutePathOrImageUri);
                return DrawingThumbnaila(original_image, thumbnailWidth, thumbnailHeight);

            }
            finally
            {
                if (original_image != null) original_image.Dispose();
                if (webClinet != null) webClinet.Dispose();
                if (imageStream != null) imageStream.Dispose();
            }

        }
        /// <summary>
        /// 从文件加载图片并以byte[]的形式返回给客户端
        /// </summary>
        /// <param name="absolutePathOrImageUri">文件系统地址或网络地址</param>
        /// <returns></returns>
        public static byte[] GenerateImage(string absolutePathOrImageUri)
        {
            System.Drawing.Image original_image = null;
            System.Net.WebClient webClinet = new System.Net.WebClient();
            Stream imageStream = null;
            MemoryStream ms = new MemoryStream();
            if (string.IsNullOrWhiteSpace(absolutePathOrImageUri)) throw new ArgumentException("地址错误");
            try
            {
                original_image = GetBitMapFromFile(absolutePathOrImageUri);
                original_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            finally
            {
                if (original_image != null) original_image.Dispose();
                if (webClinet != null) webClinet.Dispose();
                if (imageStream != null) imageStream.Dispose();
            }

        }

      
       
    
        public static byte[] CropResizeRotate(string file, int viewPortW, int viewPortH, int imageX, int imageY, int imageW, int imageH, float imageRotate, int selectorX, int selectorY, int selectorW, int selectorH)
        {
            System.Drawing.Image sourceImgTemp =System.Drawing.Image.FromFile(file);
            System.Drawing.Bitmap sourceImg = new System.Drawing.Bitmap(sourceImgTemp);
            sourceImgTemp.Dispose();
            System.Drawing.Image reSizeImg = null;
            System.Drawing.Image viewPort = null;
            System.Drawing.Image finalImg = null;
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = new MemoryStream();
            if (sourceImg == null)
            {
                return null;
            }

            try
            {
                //旋转原图片
                graphic = System.Drawing.Graphics.FromImage(sourceImg);
                // graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // graphic.TranslateTransform(sourceImg.Width/2, sourceImg.Height/2);
                // graphic.RotateTransform(imageRotate,System.Drawing.Drawing2D.MatrixOrder.Append);


                reSizeImg = new System.Drawing.Bitmap(imageW, imageH);
                graphic = System.Drawing.Graphics.FromImage(reSizeImg);
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(sourceImg, new Rectangle(0, 0, imageW, imageH), new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), GraphicsUnit.Pixel);


                viewPort = new System.Drawing.Bitmap(viewPortW, viewPortH);
                graphic = System.Drawing.Graphics.FromImage(viewPort);
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(reSizeImg, new Rectangle(imageX, imageY, imageW, imageH), new Rectangle(0, 0, imageW, imageH), GraphicsUnit.Pixel);


                finalImg = new System.Drawing.Bitmap(selectorW, selectorH);
                graphic = System.Drawing.Graphics.FromImage(finalImg);
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(viewPort, new Rectangle(0, 0, selectorW, selectorH), new Rectangle(selectorX, selectorY, selectorW, selectorH), GraphicsUnit.Pixel);


                finalImg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception("裁剪图片失败！",e);
            }
            finally
            {
                if (finalImg != null) finalImg.Dispose();
                if (graphic != null) graphic.Dispose();
                if (sourceImg != null) sourceImg.Dispose();
                if (viewPort != null) viewPort.Dispose();

            }

        }
        /// <summary>
        /// 绘制缩略图
        /// </summary>
        /// <param name="sourceImg">原图片</param>
        /// <param name="thumbnailWidth">缩略图宽度</param>
        /// <param name="thumbnailHeight">缩略图高度</param>
        /// <returns>缩略图的byte[]</returns>
        private static byte[] DrawingThumbnaila(System.Drawing.Image sourceImg, int thumbnailWidth, int thumbnailHeight)
        {
            System.Drawing.Image thumbnail_image = null;
            System.Drawing.Bitmap final_image = null;
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = null;
            try
            {
                int width = sourceImg.Width;
                int height = sourceImg.Height;
                int target_width = thumbnailWidth;
                int target_height = thumbnailHeight;
                int new_width, new_height;

                float target_ratio = (float)target_width / (float)target_height;
                float image_ratio = (float)width / (float)height;

                if (target_ratio > image_ratio)
                {
                    new_height = target_height;
                    new_width = (int)Math.Floor(image_ratio * (float)target_height);
                }
                else
                {
                    new_height = (int)Math.Floor((float)target_width / image_ratio);
                    new_width = target_width;
                }

                new_width = new_width > target_width ? target_width : new_width;
                new_height = new_height > target_height ? target_height : new_height;


                final_image = new System.Drawing.Bitmap(target_width, target_height);
                graphic = System.Drawing.Graphics.FromImage(final_image);
                graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Transparent), new System.Drawing.Rectangle(0, 0, target_width, target_height));
                int paste_x = (target_width - new_width) / 2;
                int paste_y = (target_height - new_height) / 2;
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                graphic.DrawImage(sourceImg, paste_x, paste_y, new_width, new_height);


                ms = new MemoryStream();
                final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();


            }
            catch
            {

                throw new Exception("生成缩略图失败！");
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (sourceImg != null) sourceImg.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
            }
        }
        private static System.Drawing.Image GetBitMapFromFile(string file)
        {
            System.Drawing.Image original_image = null;
            System.Net.WebClient webClinet = new System.Net.WebClient();
            Stream imageStream = null;
            if (string.IsNullOrWhiteSpace(file)) throw new ArgumentException("地址错误");
            try
            {
                if (file.Contains("\\"))
                {
                    if (File.Exists(file) == true)
                        original_image = System.Drawing.Image.FromFile(file);
                    else
                        throw new Exception("找不到该文件");
                }
                else
                {

                    imageStream = webClinet.OpenRead(file);
                    original_image = System.Drawing.Image.FromStream(imageStream);
                }
                return original_image;

            }
            finally
            {
                if (original_image != null) original_image.Dispose();
                if (webClinet != null) webClinet.Dispose();
                if (imageStream != null) imageStream.Dispose();
            }
        }

    }

}