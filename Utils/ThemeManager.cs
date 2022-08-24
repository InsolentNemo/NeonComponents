using System;
using System.Drawing;
using System.Reflection;
using System.Resources;

namespace InsolentNemo.NeonComponents.Utils
{
    public class ThemeManager
    {
        public static ResourceManager ResourceManager { get; set; }

        private static ResourceManager defaultResourceManager = new ResourceManager("InsolentNemo.NeonComponents.Themes.Default", Assembly.GetExecutingAssembly());
        private static Image backgroundImage;

        public static Color GetColor(string key)
        {
            string colorString;

            if (ResourceManager == null)
            {
                colorString = defaultResourceManager.GetString(key);
            }
            else
            {
                colorString = ResourceManager.GetString(key);

                if (colorString == null) colorString = defaultResourceManager.GetString(key);
            }

            if (colorString == null) return Color.Transparent;

            if (colorString.Equals("Transparent")) return Color.Transparent;

            string[] colorStringArray = colorString.Split(',');
            int[] color = new int[4];

            for (int i = 0; i < color.Length; i++)
            {
                color[i] = Int32.Parse(colorStringArray[i]);
            }

            return Color.FromArgb(color[0], color[1], color[2], color[3]);
        }

        public static Image GetBackgroundImage()
        {
            if (backgroundImage == null)
            {
                string imagesRaw = ResourceManager.GetString("BackgroundImages");

                if (imagesRaw == null) return null;

                string[] images = imagesRaw.Split(',');
                string randomImage = images[new Random().Next(0, images.Length - 1)];
                backgroundImage = GetImage(randomImage);
            }

            return backgroundImage;
        }

        public static Image GetImage(string key)
        {
            Image image;

            try { image = (Image) ResourceManager.GetObject(key); }
            catch { return null; }

            return image;
        }
    }
}
