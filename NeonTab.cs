using InsolentNemo.NeonComponents.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonTab : NeonButton, INeonComponent
    {
        public NeonButton CloseButton { get; set; }
        public NeonPanel TabPanel { get; set; }

        private NeonTabList tabList;
        private bool closable = false;
        private bool selected = false;

        public NeonTab(NeonTabList tabList)
        {
            this.tabList = tabList;
            Font = new Font("", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Click += new EventHandler(OnClick);

            CreateCloseButton();
        }

        private void CreateCloseButton()
        {
            CloseButton = new NeonButton();
            CloseButton.Size = new Size(11, 11);
            CloseButton.Location = new Point(Width - CloseButton.Width - 8, (Height / 2) - (CloseButton.Height / 2));
            CloseButton.Anchor = (AnchorStyles.Right);
            CloseButton.Click += new EventHandler(CloseButton_OnClick);
            CloseButton.MouseEnter += new EventHandler(CloseButton_OnMouseEnter);
            CloseButton.MouseLeave += new EventHandler(CloseButton_OnMouseLeave);
            Controls.Add(CloseButton);
        }

        private void OnClick(object sender, EventArgs e)
        {
            tabList.Select(this);
        }

        private void CloseButton_OnMouseEnter(object sender, EventArgs e)
        {
            CloseButton.FlatAppearance.BorderSize = 1;
        }

        private void CloseButton_OnMouseLeave(object sender, EventArgs e)
        {
            CloseButton.FlatAppearance.BorderSize = 0;
        }

        private void CloseButton_OnClick(object sender, EventArgs e)
        {
            tabList.Remove(this);

            if (TabPanel == null) return;

            TabPanel.Visible = false;
            TabPanel = null;
        }

        public virtual new void RefreshTheme()
        {
            base.RefreshTheme();

            BackColor = ThemeManager.GetColor("NeonTab.BackColor");
            CloseButton.BackColor = ThemeManager.GetColor("NeonTab.CloseButton.BackColor");
        }

        public virtual new void RefreshLanguage()
        {
            base.RefreshLanguage();
        }

        public bool Closable
        {
            get { return closable; }
            set
            {
                closable = value;

                if (closable) Controls.Add(CloseButton);
                else Controls.Remove(CloseButton);
            }
        }

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;

                if (TabPanel == null) return;
                    
                TabPanel.Visible = selected;

                if (selected) TabPanel.BringToFront();
            }
        }
    }
}
