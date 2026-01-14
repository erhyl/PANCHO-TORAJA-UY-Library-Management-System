using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project5LMS.Helpers
{
    /// <summary>
    /// Helper class for dynamically sizing panels based on screen/form dimensions
    /// </summary>
    public static class PanelSizeHelper
    {
        /// <summary>
        /// Gets the available width for a form or control
        /// </summary>
        public static int GetAvailableWidth(Control control)
        {
            if (control == null) return Screen.PrimaryScreen.WorkingArea.Width;
            
            if (control.Parent != null && control.Parent is Panel parentPanel)
            {
                return parentPanel.Width;
            }
            
            if (control is Form form)
            {
                return form.Width > 0 ? form.Width : Screen.PrimaryScreen.WorkingArea.Width;
            }
            
            return control.Width > 0 ? control.Width : Screen.PrimaryScreen.WorkingArea.Width;
        }
        
        /// <summary>
        /// Gets the available height for a form or control
        /// </summary>
        public static int GetAvailableHeight(Control control)
        {
            if (control == null) return Screen.PrimaryScreen.WorkingArea.Height;
            
            if (control.Parent != null && control.Parent is Panel parentPanel)
            {
                return parentPanel.Height;
            }
            
            if (control is Form form)
            {
                return form.Height > 0 ? form.Height : Screen.PrimaryScreen.WorkingArea.Height;
            }
            
            return control.Height > 0 ? control.Height : Screen.PrimaryScreen.WorkingArea.Height;
        }
        
        /// <summary>
        /// Calculates panel width based on percentage of available width
        /// </summary>
        /// <param name="control">The parent control</param>
        /// <param name="percentage">Percentage (0.0 to 1.0)</param>
        /// <param name="padding">Padding to subtract</param>
        /// <param name="minWidth">Minimum width</param>
        /// <param name="maxWidth">Maximum width (0 = no limit)</param>
        public static int CalculateWidth(Control control, double percentage, int padding = 48, int minWidth = 200, int maxWidth = 0)
        {
            int availableWidth = GetAvailableWidth(control);
            int usableWidth = availableWidth - padding;
            int calculatedWidth = (int)(usableWidth * percentage);
            
            if (calculatedWidth < minWidth) calculatedWidth = minWidth;
            if (maxWidth > 0 && calculatedWidth > maxWidth) calculatedWidth = maxWidth;
            
            return calculatedWidth;
        }
        
        /// <summary>
        /// Calculates panel height based on percentage of available height
        /// </summary>
        public static int CalculateHeight(Control control, double percentage, int padding = 48, int minHeight = 100, int maxHeight = 0)
        {
            int availableHeight = GetAvailableHeight(control);
            int usableHeight = availableHeight - padding;
            int calculatedHeight = (int)(usableHeight * percentage);
            
            if (calculatedHeight < minHeight) calculatedHeight = minHeight;
            if (maxHeight > 0 && calculatedHeight > maxHeight) calculatedHeight = maxHeight;
            
            return calculatedHeight;
        }
        
        /// <summary>
        /// Distributes panels evenly across available width
        /// </summary>
        /// <param name="container">Container panel</param>
        /// <param name="panelCount">Number of panels to distribute</param>
        /// <param name="spacing">Spacing between panels</param>
        /// <param name="minPanelWidth">Minimum width per panel</param>
        public static void DistributePanelsEvenly(Panel container, int panelCount, int spacing = 16, int minPanelWidth = 180)
        {
            if (container == null || container.Width <= 0) return;
            
            int containerPadding = container.Padding.Left + container.Padding.Right;
            int usableWidth = container.Width - containerPadding;
            int totalSpacing = spacing * (panelCount - 1);
            int panelWidth = Math.Max(minPanelWidth, (usableWidth - totalSpacing) / panelCount);
            
            int xPos = container.Padding.Left;
            foreach (Control control in container.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Width = panelWidth;
                    panel.Location = new Point(xPos, panel.Location.Y);
                    xPos += panelWidth + spacing;
                }
            }
        }
        
        /// <summary>
        /// Gets screen dimensions
        /// </summary>
        public static Size GetScreenSize()
        {
            return Screen.PrimaryScreen.WorkingArea.Size;
        }
        
        /// <summary>
        /// Gets screen width
        /// </summary>
        public static int GetScreenWidth()
        {
            return Screen.PrimaryScreen.WorkingArea.Width;
        }
        
        /// <summary>
        /// Gets screen height
        /// </summary>
        public static int GetScreenHeight()
        {
            return Screen.PrimaryScreen.WorkingArea.Height;
        }
        
        /// <summary>
        /// Calculates optimal form size based on screen size
        /// </summary>
        /// <param name="percentageWidth">Percentage of screen width (0.0 to 1.0)</param>
        /// <param name="percentageHeight">Percentage of screen height (0.0 to 1.0)</param>
        public static Size CalculateOptimalFormSize(double percentageWidth = 0.9, double percentageHeight = 0.9)
        {
            int width = (int)(GetScreenWidth() * percentageWidth);
            int height = (int)(GetScreenHeight() * percentageHeight);
            return new Size(width, height);
        }
    }
}

