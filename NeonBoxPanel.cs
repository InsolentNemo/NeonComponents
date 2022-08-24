using InsolentNemo.NeonComponents.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonBoxPanel : NeonPanel, INeonComponent
    {
        public NeonPanel WindowPanel { get; set; }
        public NeonLabel TitleLabel { get; set; }
        public NeonButton CloseButton { get; set; }
        public NeonPanel MainPanel { get; set; }

        private NeonPanel panel;
        private bool closable = true;

        public NeonBoxPanel(NeonPopupBoxPanel panel)
        {
            this.panel = panel;

            Size = new Size(400, 250);
            Location = new Point((panel.Width / 2) - (Width / 2), (panel.Height / 2) - (Height / 2));
            Anchor = AnchorStyles.None;
            BorderStyle = BorderStyle.FixedSingle;

            CreateWindowPanel();
            CreateMainPanel();
        }

        private void CreateWindowPanel()
        {
            WindowPanel = new NeonPanel();
            WindowPanel.Size = new Size(Width, 30);
            WindowPanel.Location = new Point(0, 0);
            WindowPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            Controls.Add(WindowPanel);

            CreateTitleLabel();
            CreateCloseButton();
        }

        private void CreateTitleLabel()
        {
            TitleLabel = new NeonLabel();
            TitleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Regular);
            TitleLabel.Location = new Point(4, 3);
            WindowPanel.Controls.Add(TitleLabel);
        }

        private void CreateCloseButton()
        {
            CloseButton = new NeonButton();
            CloseButton.Text = "✕";
            CloseButton.Size = new Size(20, 20);
            CloseButton.Location = new Point(WindowPanel.Width - CloseButton.Width - 6, 5);
            CloseButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            CloseButton.Font = new Font("", 8F, FontStyle.Regular, GraphicsUnit.Point);
            CloseButton.MouseClick += new MouseEventHandler(CloseButton_OnClick);
            WindowPanel.Controls.Add(CloseButton);
        }

        private void CreateMainPanel()
        {
            MainPanel = new NeonPanel();
            MainPanel.Size = new Size(Width, Height - WindowPanel.Height);
            MainPanel.Location = new Point(0, WindowPanel.Height);
            MainPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Controls.Add(MainPanel);
        }

        private void CloseButton_OnClick(object sender, MouseEventArgs e)
        {
            panel.Hide();
        }

        public virtual new void RefreshTheme()
        {
            base.RefreshTheme();

            BackColor = ThemeManager.GetColor("NeonBoxPanel.BackColor");
            WindowPanel.BackColor = ThemeManager.GetColor("NeonBoxPanel.WindowPanel.BackColor");
            TitleLabel.ForeColor = ThemeManager.GetColor("NeonBoxPanel.TitleLabel.ForeColor");
            CloseButton.ForeColor = ThemeManager.GetColor("NeonBoxPanel.CloseButton.ForeColor");
            CloseButton.BackColor = ThemeManager.GetColor("NeonBoxPanel.CloseButton.BackColor");
        }

        public virtual new void RefreshLanguage() 
        {
            base.RefreshLanguage();
        }

        /// <summary>
        /// Resizes the NeonBoxPanel in the center.
        /// </summary>
        public new Size Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;
                Location = new Point((panel.Width / 2) - (Width / 2), (panel.Height / 2) - (Height / 2));
            }
        }

        /// <summary>
        /// Shows or hides the close button.
        /// </summary>
        public bool Closable
        {
            get { return closable; }
            set
            {
                closable = value;
                CloseButton.Visible = value;
            }
        }
    }
}
