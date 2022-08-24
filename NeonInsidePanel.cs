using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonInsidePanel : NeonPanel, INeonComponent
    {
        public NeonInsidePanel(NeonPanel panel)
        {
            Size = panel.Size;
            Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Visible = false;
        }

        public virtual new void RefreshTheme()
        {
            base.RefreshTheme();
        }

        public virtual new void RefreshLanguage()
        {
            base.RefreshLanguage();
        }
    }
}
