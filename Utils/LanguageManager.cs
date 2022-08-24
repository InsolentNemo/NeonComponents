using System.Drawing;
using System.Resources;

namespace InsolentNemo.NeonComponents.Utils
{
    public class LanguageManager
    {
        public static ResourceManager ResourceManager { get; set; }

        private static Image flagImage;

        public static string Translate(string key)
        {
            string text = ResourceManager.GetString(key);

            return text;
        }

        public static Image GetBackgroundImage()
        {
            if (flagImage == null)
            {
                try { flagImage = (Image)ResourceManager.GetObject("FlagImage"); }
                catch { return null; }
            }

            return flagImage;
        }
    }
}
