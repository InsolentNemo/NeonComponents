using InsolentNemo.NeonComponents.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonLabel : Label, INeonComponent
    {
        public NeonLabel()
        {
            AutoSize = true;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        public virtual void RefreshTheme()
        {
            ForeColor = ThemeManager.GetColor("NeonLabel.ForeColor");
        }

        public virtual void RefreshLanguage() { }
    }
}
