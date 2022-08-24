using InsolentNemo.NeonComponents.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonForm : Form, INeonComponent
    {
        public NeonPanel MainPanel { get; set; }
        public NeonWindowPanel WindowPanel { get; set; }

        private string title;
        private bool closable = true;
        private bool maximizable = true;
        private bool minimizable = true;
        private NeonPanel actualInsidePanel;
        private Bitmap renderBmp;

        public NeonForm()
        {
            Size = new Size(500, 350);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;

            CreateWindowPanel();
            CreateMainPanel();
        }

        private void CreateWindowPanel()
        {
            WindowPanel = new NeonWindowPanel(this);
            Title = "My NeonForm";
            Controls.Add(WindowPanel);
        }

        private void CreateMainPanel()
        {
            MainPanel = new NeonPanel();
            MainPanel.Size = new Size(Width, Height - WindowPanel.Height);
            MainPanel.Location = new Point(0, WindowPanel.Height);
            MainPanel.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            Controls.Add(MainPanel);
        }

        /// <summary>
        /// Changes the window state. <br/>
        /// If the FormWindowState is FormWindowState.Maximizd, then the FormWindowState sets to FormWindowSTate.Normal. <br/>
        /// Otherwise the FormWindowState sets to FormWindowState.Maximized.
        /// </summary>
        public void ChangeWindowState()
        {
            if (WindowState == FormWindowState.Maximized) WindowState = FormWindowState.Normal;
            else
            {
                MaximumSize = Screen.FromHandle(Handle).WorkingArea.Size;
                WindowState = FormWindowState.Maximized;
            }
        }

        public virtual void RefreshTheme()
        {
            BackColor = ThemeManager.GetColor("NeonForm.BackColor");

            WindowPanel.RefreshTheme();
        }

        public virtual void RefreshLanguage() { }

        /// <summary>
        /// Returns and sets the title label text.
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                WindowPanel.Title = value;
            }
        }

        /// <summary>
        /// Returns and sets the Closable attribute from this NeonForm.
        /// If Closable is true, then the NeonForm gets a close button.
        /// </summary>
        public bool Closable
        {
            get { return closable; }
            set
            {
                NeonWindowStateButton closeButton = WindowPanel.GetWindowStateButtonByName("CloseButton");
                closable = value;

                if (closable) WindowPanel.AddWindowStateButton(closeButton);
                else WindowPanel.RemoveWindowStateButton(closeButton);
            }
        }

        /// <summary>
        /// Returns and sets the Maximizable attribute from this NeonForm.
        /// If Maximizable is true, then the NeonForm gets a maximize button.
        /// Additionally the NeonForm is maximizable by a double-click on the window panel.
        /// </summary>
        public bool Maximizable
        {
            get { return maximizable; }
            set
            {
                NeonWindowStateButton maximizeButton = WindowPanel.GetWindowStateButtonByName("MaximizeButton");
                maximizable = value;

                if (maximizable) WindowPanel.AddWindowStateButton(maximizeButton);
                else WindowPanel.RemoveWindowStateButton(maximizeButton);
            }
        }

        /// <summary>
        /// Returns and sets the Minimizable attribute from this NeonForm.
        /// If Minimizable is true, then the NeonForm gets a minimize button.
        /// </summary>
        public bool Minimizable
        {
            get { return minimizable; }
            set
            {
                NeonWindowStateButton minimizeButton = WindowPanel.GetWindowStateButtonByName("MinimizeButton");
                minimizable = value;

                if (minimizable) WindowPanel.AddWindowStateButton(minimizeButton);
                else WindowPanel.RemoveWindowStateButton(minimizeButton);

            }
        }

        /// <summary>
        /// Hides the ActualInsidePanel and shows the new one.
        /// </summary>
        public NeonPanel ActualInsidePanel
        {
            get { return actualInsidePanel; }
            set
            {
                if (actualInsidePanel != null) actualInsidePanel.Visible = false;

                actualInsidePanel = value;
                actualInsidePanel.Visible = true;
            }
        }

        /// <summary>
        /// Fixing render problems caused by the background image.
        /// </summary>
        public override Image BackgroundImage
        {
            set
            {
                Image baseImage = value;

                renderBmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(renderBmp);
                g.DrawImage(baseImage, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                g.Dispose();
            }
            get { return renderBmp; }
        }

        /// <summary>
        /// Shades the form.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ClassStyle = 0x20000;
                return createParams;
            }
        }

        /// <summary>
        /// Fixing the wrong calculation of width caused by BoderStyle.None.
        /// </summary>
        public new int Width
        {
            get { return base.Width - 16; }
        }

        /// <summary>
        /// Fixing the wrong calculation of height caused by BoderStyle.None.
        /// </summary>
        public new int Height
        {
            get { return base.Height - 39; }
        }
    }
}