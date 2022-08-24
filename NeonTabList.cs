using InsolentNemo.NeonComponents.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InsolentNemo.NeonComponents
{
    public enum TabListStyles
    {
        Vertical,
        Horizontal
    }

    public class NeonTabList : NeonPanel, INeonComponent
    {
        public NeonPanel ScrollPanel { get; set; }

        private TabListStyles style;

        private List<NeonTab> tabs = new List<NeonTab>();

        private int tabOffset = 0;

        public NeonTabList()
        {
            Space = 0;
            Offset = 0;
            
            BorderStyle = BorderStyle.FixedSingle;
            Resize += new EventHandler(OnResize);
            MouseWheel += new MouseEventHandler(OnMouseWheel);

            CreateScrollPanel();
        }

        private void CreateScrollPanel()
        {
            ScrollPanel = new NeonPanel();
            ScrollPanel.Space = 0;
            ScrollPanel.Offset = 0;
            ScrollPanel.Location = new Point(0, 0);
            ScrollPanel.Size = new Size(0, Height);
            ScrollPanel.MouseWheel += new MouseEventHandler(OnMouseWheel);
            Controls.Add(ScrollPanel);
        }

        private void OnResize(object sender, EventArgs e)
        {
            if (Style == TabListStyles.Vertical) ScrollPanel.Location = new Point(ScrollPanel.Location.X, 0);
            else ScrollPanel.Location = new Point(0, ScrollPanel.Location.Y);
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            int offset = e.Delta / 15;

            if (offset > 0) ScrollUp(offset);
            else ScrollDown(offset);
        }

        private void ScrollUp(int offset)
        {
            if (Style == TabListStyles.Vertical)
            {

            }
            else
            {
                if (ScrollPanel.Location.X + offset > 0) ScrollPanel.Location = new Point(0, ScrollPanel.Location.Y);
                else ScrollPanel.Location = new Point(ScrollPanel.Location.X + offset, ScrollPanel.Location.Y);
            }
        }

        private void ScrollDown(int offset)
        {
            if (Style == TabListStyles.Vertical)
            {

            }
            else
            {
                if (ScrollPanel.Width < Width) return;

                if (ScrollPanel.Location.X + offset < (Width - ScrollPanel.Width)) ScrollPanel.Location = new Point(Width - ScrollPanel.Width, ScrollPanel.Location.Y);
                else ScrollPanel.Location = new Point(ScrollPanel.Location.X + offset, ScrollPanel.Location.Y);
            }
        }

        /// <summary>
        /// Adds a new NeonTab to the Tabs.
        /// </summary>
        public void Add(NeonTab tab)
        {
            tab.MouseWheel += new MouseEventHandler(OnMouseWheel);
            Tabs.Add(tab);
            ScrollPanel.Controls.Add(tab);

            RefreshTabs();
        }

        /// <summary>
        /// Removes a NeonTab from the Tabs.
        /// </summary>
        public void Remove(NeonTab tab)
        {
            Tabs.Remove(tab);
            ScrollPanel.Controls.Remove(tab);

            RefreshTabs();
        }

        private void RefreshTabs()
        {
            ScrollPanel.ResetAllPoints();
           
            if (Style == TabListStyles.Vertical)
            {
                ScrollPanel.Size = new Size(Width, 0);

                foreach (NeonTab tab in Tabs)
                {
                    tab.Location = new Point(0, ScrollPanel.TopSpace);
                    tab.Size = new Size(ScrollPanel.Width, tab.Height);
                    tab.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                    ScrollPanel.TopSpace += tab.Height + tabOffset;
                    ScrollPanel.Size = new Size(ScrollPanel.Width, ScrollPanel.Width + tab.Height + tabOffset);
                }
            }
            else
            {
                ScrollPanel.Size = new Size(0, Height);

                foreach (NeonTab tab in Tabs)
                {
                    tab.Location = new Point(ScrollPanel.LeftSpace, 0);
                    tab.Size = new Size(tab.Width, ScrollPanel.Height);
                    tab.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;

                    ScrollPanel.LeftSpace += tab.Width + tabOffset;
                    ScrollPanel.Size = new Size(ScrollPanel.Width + tab.Width + tabOffset, ScrollPanel.Height);
                }
            }
        }

        public void Select(NeonTab selectedTab)
        {
            foreach (NeonTab tab in Tabs)
            {
                if (tab.Equals(selectedTab)) tab.Selected = true;
                else tab.Selected = false;
            }
        }

        public virtual new void RefreshTheme()
        {
            base.RefreshTheme();

            BackColor = ThemeManager.GetColor("NeonTabList.BackColor");
            ScrollPanel.BackColor = ThemeManager.GetColor("NeonTabList.ScrollPanel.BackColor");
        }

        public virtual new void RefreshLanguage()
        {
            base.RefreshLanguage();
        }

        /// <summary>
        /// Sets the Style for tab-handling
        /// </summary>
        public TabListStyles Style 
        {
            get { return style; }
            set 
            { 
                style = value; 
                
                if (style == TabListStyles.Vertical) ScrollPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
                else ScrollPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// Defines the offset size between every tab.
        /// </summary>
        public int TabOffset
        {
            get { return tabOffset; }
            set { tabOffset = value; }
        }

        /// <summary>
        /// Returns the NeonTab-list.
        /// </summary>
        public List<NeonTab> Tabs
        {
            get { return tabs; }
        }
    }
}
