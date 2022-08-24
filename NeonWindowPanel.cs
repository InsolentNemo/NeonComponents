using InsolentNemo.NeonComponents.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonWindowPanel : NeonPanel, INeonComponent
    {
        public NeonLabel TitleLabel { get; set; }
        public NeonForm Form { get; set; }

        private List<NeonWindowStateButton> windowStateButtons = new List<NeonWindowStateButton>();
        private Point dragOffset = Point.Empty;
        private bool normalizing = false;

        public NeonWindowPanel(NeonForm form)
        {
            Form = form;

            Offset = 0;
            Space = 25;

            Dock = DockStyle.Top;
            Size = new Size(form.Width, 30);
            BorderStyle = BorderStyle.FixedSingle;
            MouseDown += new MouseEventHandler(OnMouseDown);
            MouseMove += new MouseEventHandler(OnMouseMove);
            MouseUp += new MouseEventHandler(OnMouseUp);

            CreateTitleLabel();
            CreateWindowStateButtons();
        }

        private void CreateTitleLabel()
        {
            TitleLabel = new NeonLabel();
            TitleLabel.AutoSize = true;
            TitleLabel.Location = new Point(3, 3);
            TitleLabel.Font = new Font("", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TitleLabel.MouseMove += new MouseEventHandler(OnMouseMove);
            TitleLabel.MouseDown += new MouseEventHandler(OnMouseDown);
            TitleLabel.MouseUp += new MouseEventHandler(OnMouseUp);
            Controls.Add(TitleLabel);
        }

        private void CreateWindowStateButtons()
        {
            NeonWindowStateButton closeButton = new NeonWindowStateButton(this, WindowStateButton.Close);
            windowStateButtons.Add(closeButton);

            NeonWindowStateButton maximizeButton = new NeonWindowStateButton(this, WindowStateButton.Maximize);
            windowStateButtons.Add(maximizeButton);

            NeonWindowStateButton minimizeButton = new NeonWindowStateButton(this, WindowStateButton.Minimize);
            windowStateButtons.Add(minimizeButton);

            for (int i = 0; i < windowStateButtons.Count; i++) Controls.Add(windowStateButtons[i]);

            RefreshWindowStateButtons();
        }

        /// <summary>
        /// Adds a NeonWindowStateButton to the window panel.
        /// </summary>
        /// <param name="button"></param>
        public void AddWindowStateButton(NeonWindowStateButton button)
        {
            if (windowStateButtons.Contains(button)) button.Visible = true;
            else
            {
                windowStateButtons.Add(button);
                Controls.Add(button);
            }

            RefreshWindowStateButtons();
        }

        /// <summary>
        /// Removes a NeonWindowStateButton from the window panel.
        /// </summary>
        /// <param name="button"></param>
        public void RemoveWindowStateButton(NeonWindowStateButton button)
        {
            if (!windowStateButtons.Contains(button)) return;
            else button.Visible = false;

            RefreshWindowStateButtons();
        }

        private void RefreshWindowStateButtons()
        {
            RightSpace = 0;
            
            NeonWindowStateButton closeButton = GetWindowStateButtonByName("CloseButton");
            NeonWindowStateButton maximizeButton = GetWindowStateButtonByName("MaximizeButton");
            NeonWindowStateButton minimizeButton = GetWindowStateButtonByName("MinimizeButton");

            if (Form.Closable)
            {
                RightSpace += closeButton.Width;
                Point location = new Point(Width - RightSpace, 0);
                closeButton.Location = location;
            }

            if (Form.Maximizable)
            {
                RightSpace += maximizeButton.Width;
                Point location = new Point(Width - RightSpace, 0);
                maximizeButton.Location = location;
            }

            if (Form.Minimizable)
            {
                RightSpace += minimizeButton.Width;
                Point location = new Point(Width - RightSpace, 0);
                minimizeButton.Location = location;
            }

            for (int i = 0; i < windowStateButtons.Count; i++)
            {
                if (windowStateButtons[i] == closeButton) continue;
                if (windowStateButtons[i] == maximizeButton) continue;
                if (windowStateButtons[i] == minimizeButton) continue;

                RightSpace += windowStateButtons[i].Width;
                Point location = new Point(Width - RightSpace, 0);
                windowStateButtons[i].Location = location;
            }

            RightSpace += Space;
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) SetMouseOffset(e.X, e.Y);
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            MoveForm(e.X, e.Y);
        }

        public void OnMouseUp(object sender, MouseEventArgs e)
        {
            dragOffset = Point.Empty;
            normalizing = false;

            if (Form.WindowState != FormWindowState.Normal) return;
            if (Form.Location.Y >= 0) return;
            if (Form.Maximizable)
            {
                NeonWindowStateButton maximizeButton = GetWindowStateButtonByName("MaximizeButton");
                maximizeButton.ChangeWindowState();
            }
        }

        private void SetMouseOffset(int x, int y)
        {
            dragOffset = new Point(x, y);
        }

        private void MoveForm(int x, int y)
        {
            if (dragOffset == Point.Empty) return;
            if (AutoChangeWindowState(y)) return;

            Point location;

            if (normalizing)
            {
                location = Form.Location;
                location.X += x - (Width / 2);
                location.Y += y - (Height / 2);
            }
            else
            {
                location = Form.Location;
                location.X += x - dragOffset.X;
                location.Y += y - dragOffset.Y;
            }

            Form.Location = location;
        }

        private bool AutoChangeWindowState(int y)
        {
            if (Form.WindowState != FormWindowState.Maximized || !Form.Maximizable) return false;
            if (y > Height)
            {
                NeonWindowStateButton maximizeButton = GetWindowStateButtonByName("MaximizeButton");
                maximizeButton.ChangeWindowState();
                normalizing = true;
            }

            return true;
        }

        /// <summary>
        /// Returns a WindowStateButton by the button name as parameter. <br/>
        /// If button is not avaiable, then it returns null.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NeonWindowStateButton GetWindowStateButtonByName(string name)
        {
            foreach (NeonWindowStateButton windowStateButton in windowStateButtons)
            {
                if (windowStateButton.Name.Equals(name)) return windowStateButton;
            }

            return null;
        }

        public new virtual void RefreshTheme()
        {
            base.RefreshTheme();

            BackColor = ThemeManager.GetColor("NeonWindowPanel.BackColor");
            TitleLabel.ForeColor = ThemeManager.GetColor("NeonWindowPanel.TitleLabel.ForeColor");

            foreach (NeonWindowStateButton windowStateButton in windowStateButtons)
            {
                windowStateButton.RefreshTheme();
            }
        }

        public new virtual void RefreshLanguage() 
        {
            base.RefreshLanguage();
        }

        /// <summary>
        /// Returns and sets the title label text.
        /// </summary>
        public string Title
        {
            get { return TitleLabel.Text; }
            set
            {
                Form.Text = value;
                TitleLabel.Text = value;
                LeftSpace = Offset + TitleLabel.Width + 3 + Space;
            }
        }

        /// <summary>
        /// Returns all WindowStateButtons as array.
        /// </summary>
        public NeonWindowStateButton[] WindowStateButtons
        {
            get { return windowStateButtons.ToArray(); }
        }
    }
}
