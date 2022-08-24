using InsolentNemo.NeonComponents.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonButton : Button, INeonComponent
    {
        public NeonButton()
        {
            Size = new Size(100, 50);
            FlatAppearance.BorderSize = 0;
            FlatAppearance.BorderColor = Color.FromArgb(255, 127, 127, 127);
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Trebuchet MS", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        }

        public virtual void RefreshTheme()
        {
            ForeColor = ThemeManager.GetColor("NeonButton.ForeColor");
            BackColor = ThemeManager.GetColor("NeonButton.BackColor");
        }

        public virtual void RefreshLanguage() { }
    }
}