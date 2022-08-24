using InsolentNemo.NeonComponents.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public enum WindowStateButton
    {
        Close,
        Maximize,
        Minimize,
        Custom
    }

    public class NeonWindowStateButton : NeonButton, INeonComponent
    {
        private WindowStateButton type;
        private NeonForm form;

        public NeonWindowStateButton(NeonWindowPanel panel, WindowStateButton type)
        {
            this.type = type;
            form = panel.Form;
            Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            Font = new Font("", 13F, FontStyle.Regular, GraphicsUnit.Point);
            Size = new Size((int)(panel.Height * 1.4), panel.Height - 2);

            switch (type)
            {
                case WindowStateButton.Close:
                    Name = "CloseButton";
                    Text = "✕";
                    MouseClick += new MouseEventHandler(CloseButton_OnClick);
                    break;

                case WindowStateButton.Maximize:
                    Name = "MaximizeButton";

                    if (form.WindowState == FormWindowState.Normal) Text = "☐";
                    else Text = "❐";

                    MouseClick += new MouseEventHandler(MaximizeButton_OnClick);
                    break;

                case WindowStateButton.Minimize:
                    Name = "MinimizeButton";
                    Text = "—";
                    MouseClick += new MouseEventHandler(MinimizeButton_OnClick);
                    break;

                case WindowStateButton.Custom:
                    break;
            }
        }

        private void CloseButton_OnClick(object sender, MouseEventArgs e)
        {
            form.Close();
        }

        private void MaximizeButton_OnClick(object sender, MouseEventArgs e)
        {
            ChangeWindowState();
        }

        private void MinimizeButton_OnClick(object sender, MouseEventArgs e)
        {
            form.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Calls the forms ChangeWindowState method and updates the maximize icon.
        /// </summary>
        public void ChangeWindowState()
        {
            form.ChangeWindowState();

            if (form.WindowState == FormWindowState.Normal) Text = "☐";
            else Text = "❐";
        }

        public virtual new void RefreshTheme()
        {
            base.RefreshTheme();

            ForeColor = ThemeManager.GetColor("NeonWindowStateButton.ForeColor");
            BackColor = ThemeManager.GetColor("NeonWindowStateButton.BackColor");
        }

        public virtual new void RefreshLanguage()
        {
            base.RefreshLanguage();
        }
    }
}
