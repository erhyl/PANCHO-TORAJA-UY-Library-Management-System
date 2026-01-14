using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Project5LMS.Services;

namespace Project5LMS.Forms.Admin.Reports
{
    /// <summary>
    /// Helper class for drawing chart visualizations in reports
    /// </summary>
    public static class ReportChartHelper
    {
        public static void DrawCirculationBarChart(Graphics g, Panel panel, Dictionary<string, int> data)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(panel.BackColor);
            int padding = 60;
            int titleHeight = 0;
            int chartWidth = panel.Width - (padding * 2);
            int chartHeight = panel.Height - (padding * 2) - titleHeight;
            int startX = padding;
            int startY = padding;
            int endX = panel.Width - padding;
            int endY = panel.Height - padding - titleHeight;
            
            if (data.Count == 0) return;
            int maxValue = data.Values.DefaultIfEmpty(0).Max();
            maxValue = Math.Max(maxValue, 1);
            
            g.DrawLine(Pens.LightGray, startX, startY, startX, endY);
            g.DrawLine(Pens.LightGray, startX, endY, endX, endY);
            
            int gridLines = 5;
            for (int i = 0; i <= gridLines; i++)
            {
                int y = startY + (int)((endY - startY) * (1 - (double)i / gridLines));
                g.DrawLine(new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash }, startX, y, endX, y);
                int value = (int)(maxValue * i / gridLines);
                g.DrawString(value.ToString(), new Font("Segoe UI", 9F), Brushes.Gray, 5, y - 10);
            }
            
            var items = data.OrderByDescending(x => x.Value).ToList();
            int barCount = items.Count;
            float barWidth = (float)chartWidth / (barCount + 1) - 20;
            float stepX = (float)chartWidth / (barCount + 1);
            Color barColor = Color.FromArgb(13, 110, 253);
            
            for (int i = 0; i < items.Count; i++)
            {
                float x = startX + (i + 1) * stepX;
                int value = items[i].Value;
                int y = endY - (int)((double)value / maxValue * chartHeight);
                
                float barHeight = endY - y;
                g.FillRectangle(new SolidBrush(barColor), x, y, barWidth, barHeight);
                
                string valueText = value.ToString();
                using (Font valueFont = new Font("Segoe UI", 9F, FontStyle.Bold))
                {
                    SizeF valueSize = g.MeasureString(valueText, valueFont);
                    float valueX = x + (barWidth / 2) - (valueSize.Width / 2);
                    float valueY = y - valueSize.Height - 5;
                    if (valueY < startY) valueY = y + barHeight + 5;
                    
                    RectangleF valueRect = new RectangleF(valueX - 2, valueY - 1, valueSize.Width + 4, valueSize.Height + 2);
                    g.FillRectangle(Brushes.White, valueRect);
                    g.DrawString(valueText, valueFont, Brushes.Black, valueX, valueY);
                }
                
                string label = items[i].Key;
                SizeF labelSize = g.MeasureString(label, new Font("Segoe UI", 9F));
                g.DrawString(label, new Font("Segoe UI", 9F), Brushes.Black,
                    x + (barWidth / 2) - (labelSize.Width / 2), endY + 8);
            }
        }
        
        public static void DrawMemberActivityChart(Graphics g, Panel panel, Dictionary<string, int> data)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(panel.BackColor);
            int padding = 60;
            int titleHeight = 0;
            int chartWidth = panel.Width - (padding * 2);
            int chartHeight = panel.Height - (padding * 2) - titleHeight;
            int startX = padding;
            int startY = padding;
            int endX = panel.Width - padding;
            int endY = panel.Height - padding - titleHeight;
            
            if (data.Count == 0) return;
            int maxValue = data.Values.DefaultIfEmpty(0).Max();
            maxValue = Math.Max(maxValue, 1);
            
            g.DrawLine(Pens.LightGray, startX, startY, startX, endY);
            g.DrawLine(Pens.LightGray, startX, endY, endX, endY);
            
            int gridLines = 5;
            for (int i = 0; i <= gridLines; i++)
            {
                int y = startY + (int)((endY - startY) * (1 - (double)i / gridLines));
                g.DrawLine(new Pen(Color.LightGray, 1) { DashStyle = DashStyle.Dash }, startX, y, endX, y);
                int value = (int)(maxValue * i / gridLines);
                g.DrawString(value.ToString(), new Font("Segoe UI", 9F), Brushes.Gray, 5, y - 10);
            }
            
            var items = data.OrderByDescending(x => x.Value).ToList();
            int barCount = items.Count;
            float barWidth = (float)chartWidth / (barCount + 1) - 30;
            float stepX = (float)chartWidth / (barCount + 1);
            Color barColor = Color.FromArgb(128, 0, 128);
            
            for (int i = 0; i < items.Count; i++)
            {
                float x = startX + (i + 1) * stepX;
                int value = items[i].Value;
                int y = endY - (int)((double)value / maxValue * chartHeight);
                
                float barHeight = endY - y;
                g.FillRectangle(new SolidBrush(barColor), x, y, barWidth, barHeight);
                
                string valueText = value.ToString();
                using (Font valueFont = new Font("Segoe UI", 9F, FontStyle.Bold))
                {
                    SizeF valueSize = g.MeasureString(valueText, valueFont);
                    float valueX = x + (barWidth / 2) - (valueSize.Width / 2);
                    float valueY = y - valueSize.Height - 5;
                    if (valueY < startY) valueY = y + barHeight + 5;
                    
                    RectangleF valueRect = new RectangleF(valueX - 2, valueY - 1, valueSize.Width + 4, valueSize.Height + 2);
                    g.FillRectangle(Brushes.White, valueRect);
                    g.DrawString(valueText, valueFont, Brushes.Black, valueX, valueY);
                }
                
                string label = items[i].Key;
                SizeF labelSize = g.MeasureString(label, new Font("Segoe UI", 9F));
                g.DrawString(label, new Font("Segoe UI", 9F), Brushes.Black,
                    x + (barWidth / 2) - (labelSize.Width / 2), endY + 8);
            }
        }
        
        public static void DrawPeakTimesChart(Graphics g, Panel panel, DateTime? startDate = null, DateTime? endDate = null)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            int padding = 60;
            int chartWidth = panel.Width - padding * 2;
            int chartHeight = panel.Height - padding * 2;
            int startX = padding;
            int startY = padding;
            int endY = startY + chartHeight;
            
            g.DrawString("Peak Borrowing Times by Hour", new Font("Segoe UI", 12F, FontStyle.Bold),
                Brushes.Black, startX, startY - 30);
            g.DrawLine(Pens.Gray, startX, endY, startX + chartWidth, endY);
            g.DrawLine(Pens.Gray, startX, startY, startX, endY);
            
            Dictionary<int, int> hourlyData = new Dictionary<int, int>();
            for (int i = 0; i < 24; i++) hourlyData[i] = 0;
            
            try
            {
                using (var conn = ServiceFactory.GetDbContext().GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT HOUR(BorrowDate) as Hour, COUNT(*) as Count
                                    FROM Transactions
                                    WHERE BorrowDate IS NOT NULL";
                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND (@StartDate IS NULL OR DATE(BorrowDate) >= @StartDate) AND (@EndDate IS NULL OR DATE(BorrowDate) <= @EndDate)";
                    }
                    query += " GROUP BY HOUR(BorrowDate) ORDER BY HOUR(BorrowDate)";
                    
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDate.Value);
                            cmd.Parameters.AddWithValue("@EndDate", endDate.Value);
                        }
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                hourlyData[reader.GetInt32("Hour")] = reader.GetInt32("Count");
                        }
                    }
                }
            }
            catch { }
            
            int maxValue = Math.Max(hourlyData.Values.Max(), 1);
            int barWidth = chartWidth / 24;
            
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(13, 110, 253)))
            {
                for (int hour = 0; hour < 24; hour++)
                {
                    int count = hourlyData[hour];
                    int barHeight = (int)((double)count / maxValue * chartHeight);
                    int x = startX + hour * barWidth;
                    
                    if (barHeight > 0)
                    {
                        g.FillRectangle(brush, x, endY - barHeight, barWidth - 2, barHeight);
                        if (barHeight > 20)
                        {
                            string label = count.ToString();
                            using (Font labelFont = new Font("Segoe UI", 8F, FontStyle.Bold))
                            {
                                SizeF textSize = g.MeasureString(label, labelFont);
                                float labelX = x + (barWidth - textSize.Width) / 2;
                                float labelY = endY - barHeight - textSize.Height - 2;
                                RectangleF labelRect = new RectangleF(labelX - 2, labelY - 1, textSize.Width + 4, textSize.Height + 2);
                                g.FillRectangle(Brushes.White, labelRect);
                                g.DrawString(label, labelFont, Brushes.Black, labelX, labelY);
                            }
                        }
                    }
                    
                    if (hour % 2 == 0)
                    {
                        string hourLabel = hour.ToString("00") + ":00";
                        SizeF hourSize = g.MeasureString(hourLabel, new Font("Segoe UI", 8F));
                        g.DrawString(hourLabel, new Font("Segoe UI", 8F), Brushes.Gray,
                            x + (barWidth - hourSize.Width) / 2, endY + 5);
                    }
                }
            }
        }
        
        public static void DrawDailyActivityChart(Graphics g, Panel panel, Dictionary<DateTime, int> borrowData, Dictionary<DateTime, int> returnData)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(panel.BackColor);
            int padding = 60;
            int chartWidth = panel.Width - padding * 2;
            int chartHeight = panel.Height - padding * 2;
            int startX = padding;
            int startY = padding;
            int endY = startY + chartHeight;
            
            using (Font titleFont = new Font("Segoe UI", 14F, FontStyle.Bold))
            {
                g.DrawString("Daily Borrowing/Return Activity", titleFont, Brushes.Black, startX, 10);
            }
            
            g.DrawLine(Pens.Gray, startX, endY, startX + chartWidth, endY);
            
            var allDates = borrowData.Keys.Union(returnData.Keys).OrderBy(d => d).ToList();
            if (allDates.Count == 0) return;
            
            int maxValue = Math.Max(
                borrowData.Values.DefaultIfEmpty(0).Max(),
                returnData.Values.DefaultIfEmpty(0).Max()
            );
            maxValue = Math.Max(maxValue, 1);
            
            int pointSpacing = allDates.Count > 1 ? chartWidth / (allDates.Count - 1) : chartWidth;
            var borrowPoints = new List<PointF>();
            var returnPoints = new List<PointF>();
            
            for (int i = 0; i < allDates.Count; i++)
            {
                float x = startX + i * pointSpacing;
                int borrowValue = borrowData.ContainsKey(allDates[i]) ? borrowData[allDates[i]] : 0;
                int returnValue = returnData.ContainsKey(allDates[i]) ? returnData[allDates[i]] : 0;
                int borrowY = endY - (int)((double)borrowValue / maxValue * chartHeight);
                int returnY = endY - (int)((double)returnValue / maxValue * chartHeight);
                
                borrowPoints.Add(new PointF(x, borrowY));
                returnPoints.Add(new PointF(x, returnY));
                
                g.FillEllipse(new SolidBrush(Color.FromArgb(13, 110, 253)), x - 3, borrowY - 3, 6, 6);
                g.FillEllipse(new SolidBrush(Color.FromArgb(40, 167, 69)), x - 3, returnY - 3, 6, 6);
            }
            
            if (borrowPoints.Count > 1)
            {
                g.DrawLines(new Pen(Color.FromArgb(13, 110, 253), 2), borrowPoints.ToArray());
                g.DrawLines(new Pen(Color.FromArgb(40, 167, 69), 2), returnPoints.ToArray());
            }
            
            g.FillRectangle(new SolidBrush(Color.FromArgb(13, 110, 253)), startX + chartWidth - 150, startY + 10, 15, 15);
            g.DrawString("Borrowed", new Font("Segoe UI", 9F), Brushes.Black, startX + chartWidth - 130, startY + 8);
            g.FillRectangle(new SolidBrush(Color.FromArgb(40, 167, 69)), startX + chartWidth - 150, startY + 30, 15, 15);
            g.DrawString("Returned", new Font("Segoe UI", 9F), Brushes.Black, startX + chartWidth - 130, startY + 28);
        }
    }
}
