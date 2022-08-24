using InsolentNemo.NeonComponents.Utils;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonCheckBox : CheckBox, INeonComponent
    {
        public NeonCheckBox()
        {
            AutoSize = true;
            FlatStyle = FlatStyle.Flat;
        }

        public virtual void RefreshTheme()
        {
            ForeColor = ThemeManager.GetColor("NeonCheckBox.ForeColor");
            BackColor = ThemeManager.GetColor("NeonCheckBox.BackColor");
        }

        public virtual void RefreshLanguage() { }
    }
}
