using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public class NeonPanel : Panel, INeonComponent
    {
        public int LeftSpace { get; set; }
        public int RightSpace { get; set; }
        public int TopSpace { get; set; }
        public int BottomSpace { get; set; }

        private NeonInsidePanel actualInsidePanel;

        private int offset = 25;
        private int space = 15;

        public NeonPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            ResetAllPoints();
        }

        /// <summary>
        /// Resets all space points the offset value.
        /// </summary>
        public void ResetAllPoints()
        {
            LeftSpace = offset;
            RightSpace = offset;
            TopSpace = offset;
            BottomSpace = offset;
        }

        /// <summary>
        /// Pops up a NeonPopupBoxPanel.
        /// </summary>
        public void Popup(NeonPopupBoxPanel panel)
        {
            panel.Visible = true;
            panel.BringToFront();
        }

        public virtual void RefreshTheme() 
        {
            BackColor = Color.Transparent;
        }

        public virtual void RefreshLanguage() { }

        /// <summary>
        /// Returns and sets the offset space. Additionally it resets all space points.
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set
            {
                offset = value;
                ResetAllPoints();
            }
        }

        /// <summary>
        /// Returns and sets the component space.
        /// </summary>
        public int Space
        {
            get { return space; }
            set { space = value; }
        }

        /// <summary>
        /// Hides the ActualInsidePanel and shows the new one.
        /// </summary>
        public NeonInsidePanel ActualInsidePanel
        {
            get { return actualInsidePanel; }
            set
            {
                if (actualInsidePanel != null)
                {
                    actualInsidePanel.Enabled = false;
                    actualInsidePanel.Visible = false;
                }

                actualInsidePanel = value;
                actualInsidePanel.Enabled = true;
                actualInsidePanel.Visible = true;
            }
        }
    }
}
