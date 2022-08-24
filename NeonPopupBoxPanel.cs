using InsolentNemo.NeonComponents.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonPopupBoxPanel : NeonInsidePanel, INeonComponent
    {
        public NeonBoxPanel BoxPanel { get; set; }

        private string title;

        public NeonPopupBoxPanel(NeonPanel panel) : base(panel)
        {
            Size = panel.Size;
            Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            BackColor = Color.FromArgb(127, 0, 0, 0);
            Visible = false;

            CreateBoxPanel();
        }

        private void CreateBoxPanel()
        {
            BoxPanel = new NeonBoxPanel(this);
            Title = "My NeonComponent BoxPanel";
            Controls.Add(BoxPanel);
        }

        /// <summary>
        /// Sets the title of the BoxPanel.
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                BoxPanel.TitleLabel.Text = title;
            }
        }

        public virtual new void RefreshTheme()
        {
            base.RefreshTheme();

            BackColor = ThemeManager.GetColor("NeonPopupBoxPanel.BackColor");

            BoxPanel.RefreshTheme();
        }

        public virtual new void RefreshLanguage()
        {
            base.RefreshLanguage();
        }
    }
}
