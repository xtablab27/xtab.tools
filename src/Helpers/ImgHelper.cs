using System;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace xTab.Tools.Helpers
{
    public static class ImgHelper
    {
        public static Bitmap LimitSize(Image Image, Size LimitSize)
        {
            if (LimitSize.Width == 0 && LimitSize.Height != 0)
                if (Image.Size.Height > LimitSize.Height)
                    return DrawBitmap(Image, GetProportionalWidth(Image.Size, LimitSize.Height));
                else
                    return new Bitmap(Image);
            else if (LimitSize.Height == 0 && LimitSize.Width != 0)
                if (Image.Size.Width > LimitSize.Width)
                    return DrawBitmap(Image, GetProportionalHeight(Image.Size, LimitSize.Width));
                else
                    return new Bitmap(Image);
            else
                if (Image.Size.Width > LimitSize.Width || Image.Size.Height > LimitSize.Height)
                    return DrawBitmap(Image, GetProportionalSize(Image.Size, LimitSize));
                else
                    return new Bitmap(Image);
        }

        public static Bitmap Resize(Image Image, Size NewSize, Boolean IsCut)
        {
            if (!IsCut)                
                return DrawBitmap(Image, NewSize);
            else
                if (Image.Size.Width * NewSize.Height >= Image.Size.Height * NewSize.Width)
                    using (Bitmap TempBitmap = DrawBitmap(Image, GetProportionalWidth(Image.Size, NewSize.Height)))
                        return TempBitmap.Clone(new Rectangle(TempBitmap.Width / 2 - NewSize.Width / 2, 0, NewSize.Width, NewSize.Height), TempBitmap.PixelFormat);
                else
                    using (Bitmap TempBitmap = DrawBitmap(Image, GetProportionalHeight(Image.Size, NewSize.Width)))
                        return TempBitmap.Clone(new Rectangle(0, TempBitmap.Height / 2 - NewSize.Height / 2, NewSize.Width, NewSize.Height), TempBitmap.PixelFormat);
        }

        public static Bitmap Crop(Image Image, Rectangle CropArea)
        {
            using (Bitmap TempBitmap = new Bitmap(Image))
                return TempBitmap.Clone(CropArea, TempBitmap.PixelFormat);
        }

        public static Bitmap WaterMark(Image Image, Image WaterImage, WaterMarkPosition WaterMarkPosition = WaterMarkPosition.Center) 
        {
            switch (WaterMarkPosition)
            {
                case WaterMarkPosition.Right:
                    WaterImage = LimitSize(WaterImage, new Size(Image.Width / 3, 0));

                    using (var g = Graphics.FromImage(Image))
                    {
                        g.DrawImage(WaterImage, Image.Width - (WaterImage.Width + 20), Image.Height - (WaterImage.Height + 20));
                    }

                    break;

                case WaterMarkPosition.Left:
                    WaterImage = LimitSize(WaterImage, new Size(Image.Width / 3, 0));

                    using (var g = Graphics.FromImage(Image))
                    {
                        g.DrawImage(WaterImage, 20, Image.Height - (WaterImage.Height + 20));
                    }

                    break;
                case WaterMarkPosition.Center:
                    WaterImage = LimitSize(WaterImage, new Size((Image.Width / 3) * 2, 0));
                    using (var g = Graphics.FromImage(Image))
                    {
                        g.DrawImage(WaterImage, (Image.Width - WaterImage.Width) / 2, (Image.Height / 3) * 2);
                    }
                
                    break;
            }
 
            return (Bitmap)Image;
        }

        public static Bitmap Greyscale(Bitmap Image)
        {
            PixelFormat pxf = PixelFormat.Format24bppRgb;
            Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);
            BitmapData ImageData = Image.LockBits(rect, ImageLockMode.ReadWrite, pxf);
            IntPtr ptr = ImageData.Scan0;

            int numBytes = ImageData.Stride * Image.Height;
            int widthBytes = ImageData.Stride;
            byte[] rgbValues = new byte[numBytes];
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                int value = rgbValues[counter] + rgbValues[counter + 1] + rgbValues[counter + 2];
                byte color_b = 0;

                color_b = Convert.ToByte(value / 3);
                rgbValues[counter] = color_b;
                rgbValues[counter + 1] = color_b;
                rgbValues[counter + 2] = color_b;
            }

            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            Image.UnlockBits(ImageData);

            return Image;
        }


        public static void Save(Bitmap SourceBitmap, String Path, Int32 Quality = 95, String Format = "", Boolean Rotate90 = false)
        {
            ImageCodecInfo[] Encoders = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo CodecInfo =  Encoders.First(x => x.MimeType == GetImageContentType(Path));
            EncoderParameters EncoderParameters = new EncoderParameters(Rotate90 ? 2 : 1);
            EncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Quality);
            if (Rotate90) EncoderParameters.Param[1] = new EncoderParameter(Encoder.Transformation, (long)EncoderValue.TransformRotate90);

            SourceBitmap.Save(CurrentServer.MapPath(Path), CodecInfo, EncoderParameters);
        }

        private static Bitmap DrawBitmap(Object SourceImage, Size NewSize)
        {
            var result = new Bitmap(NewSize.Width, NewSize.Height);
         
            using (var g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage((Image)SourceImage, 0, 0, result.Width, result.Height);
            }

            return result;
        }


        public static Size GetProportionalSize(Size CurentSize, Size BoundedSize)
        {
            if (CurentSize.Width >= CurentSize.Height)
                return new Size(BoundedSize.Width, (CurentSize.Height * BoundedSize.Width) / CurentSize.Width);
            else
                return new Size((CurentSize.Width * BoundedSize.Height) / CurentSize.Height, BoundedSize.Height);
        }

        public static Size GetProportionalWidth(Size CurentSize, Int32 Height)
        {
            return new Size((CurentSize.Width * Height) / CurentSize.Height, Height);
        }

        public static Size GetProportionalHeight(Size CurentSize, Int32 Width)
        {
            return new Size(Width, (CurentSize.Height * Width) / CurentSize.Width);
        }

        public static String GetImageContentType(String FileName)
        {
            switch (FileName.Substring(FileName.LastIndexOf(".") + 1).ToLower())
            {
                case ("jpg"):  return "image/jpeg";
                case ("jpeg"): return "image/jpeg";
                case ("png"):  return "image/png";
                case ("webp"): return "image/webp";
                case ("bmp"):  return "image/bmp";
                case ("gif"):  return "image/gif";
                default:       return String.Empty;
            }
        }

        private static HttpServerUtility CurrentServer
        {
            get { return HttpContext.Current.Server; }
        }

        
    }

    public enum WaterMarkPosition { Center, Left, Right }
}