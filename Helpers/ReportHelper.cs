using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace Project5LMS.Helpers
{
    /// <summary>
    /// Helper methods for report generation and data export
    /// Extracted from AdminReportsForm to reduce duplication and improve maintainability
    /// </summary>
    public static class ReportHelper
    {
        /// <summary>
        /// Escapes CSV field values to handle commas, quotes, and newlines
        /// </summary>
        public static string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return "";
            
            // If field contains comma, quote, or newline, wrap in quotes and escape quotes
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            
            return field;
        }
        
        /// <summary>
        /// Extracts title and value from a card panel by analyzing its label controls
        /// </summary>
        public static (string title, string value) ExtractCardData(Panel cardPanel)
        {
            string title = "", value = "";
            foreach (Control ctrl in cardPanel.Controls)
            {
                if (ctrl is Label lbl)
                {
                    string lblText = lbl.Text ?? "";
                    if (lbl.Name.Contains("Title") || (lbl.Font.Size < 12 && !string.IsNullOrEmpty(lblText) && lblText.Length < Constants.MaxTitleLength))
                        title = string.IsNullOrEmpty(title) ? lblText : title;
                    else if (lbl.Name.Contains("Value") || lbl.Font.Size >= 20 || lbl.Font.Bold)
                        value = string.IsNullOrEmpty(value) ? lblText : value;
                }
            }
            return (title, value);
        }
        
        /// <summary>
        /// Finds a control of specified type by name within a parent control hierarchy
        /// </summary>
        public static T FindControl<T>(Control parent, string name) where T : Control
        {
            foreach (Control control in parent.Controls)
            {
                if (control is T && control.Name == name)
                {
                    return control as T;
                }
                if (control.HasChildren)
                {
                    T found = FindControl<T>(control, name);
                    if (found != null)
                        return found;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Finds all DataGridView controls within a parent control hierarchy
        /// </summary>
        public static void FindDataGridViews(Control parent, List<DataGridView> dataGridViews)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is DataGridView dgv)
                {
                    dataGridViews.Add(dgv);
                }
                else if (control.HasChildren)
                {
                    FindDataGridViews(control, dataGridViews);
                }
            }
        }
        
        /// <summary>
        /// Gets a section name for a DataGridView based on its parent context
        /// </summary>
        public static string GetDataGridViewSectionName(DataGridView dgv)
        {
            // Try to determine section name from parent or context
            Control parent = dgv.Parent;
            while (parent != null)
            {
                if (parent is Panel panel)
                {
                    if (panel.Name.Contains("MostBorrowed") || dgv.Name.Contains("MostBorrowed"))
                        return "Most Borrowed Books";
                    if (panel.Name.Contains("Overdue") || dgv.Name.Contains("Overdue"))
                        return "Overdue Books Report";
                    if (panel.Name.Contains("NewMembers") || dgv.Name.Contains("NewMembers"))
                        return "New Member Registrations";
                    if (panel.Name.Contains("ShelfList") || dgv.Name.Contains("ShelfList"))
                        return "Shelf List";
                }
                parent = parent.Parent;
            }
            return "Report Data";
        }
        
        /// <summary>
        /// Finds a FlowLayoutPanel by name within a control hierarchy
        /// </summary>
        public static FlowLayoutPanel FindFlowLayoutPanel(Control parent, string panelName)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is FlowLayoutPanel flowPanel && flowPanel.Name.Contains(panelName))
                    return flowPanel;
                if (ctrl is Panel mainPanel)
                {
                    foreach (Control subCtrl in mainPanel.Controls)
                    {
                        if (subCtrl is FlowLayoutPanel flowPanel2 && flowPanel2.Name.Contains(panelName))
                            return flowPanel2;
                    }
                }
            }
            return null;
        }
    }
}
