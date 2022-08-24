using InsolentNemo.NeonComponents.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonTextBox : NeonPanel
    {
        public TextBox InsideTextBox { get; set; }

        private bool password;

        public NeonTextBox()
        {
            Size = new Size(100, 22);
            BorderStyle = BorderStyle.FixedSingle;

            CreateTextBox();
        }

        private void CreateTextBox()
        {
            InsideTextBox = new TextBox();
            InsideTextBox.Font = new Font("Segoe UI", 9.0F, FontStyle.Regular);
            InsideTextBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            InsideTextBox.Location = new Point(4, 3);
            InsideTextBox.Size = new Size(Size.Width - (InsideTextBox.Location.X * 2), 0);
            InsideTextBox.BorderStyle = BorderStyle.None;

            Controls.Add(InsideTextBox);
        }

        public virtual new void RefreshTheme()
        {
            base.RefreshTheme();

            BackColor = ThemeManager.GetColor("NeonTextBox.BackColor");
            InsideTextBox.ForeColor = ThemeManager.GetColor("NeonTextBox.InsideTextBox.ForeColor");
        }

        public virtual new void RefreshLanguage()
        {
            base.RefreshLanguage();
        }

        /// <summary>
        /// Returns and sets the password attribute. <br/>
        /// If true, the text inside the InsideTextBox is not readable.
        /// </summary>
        public bool Password
        {
            get { return password; }
            set
            {
                password = value;

                if (value) InsideTextBox.PasswordChar = '●';
                else InsideTextBox.PasswordChar = '\0';
            }
        }
    }
}
