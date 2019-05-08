using System.Windows.Forms;

namespace YYX.CloseWindow
{
    public static class ContextMenuStripExtension
    {
        public static void AddAutoRun(this ContextMenuStrip contextMenuStrip)
        {
            var toolStripMenuItem = new ToolStripMenuItem("开机启动")
            {
                Checked = AutoRun.GetRunEnable()
            };

            toolStripMenuItem.Click += (sender, e) =>
            {
                var runEnable = !toolStripMenuItem.Checked;
                AutoRun.SetRunEnable(runEnable);
                toolStripMenuItem.Checked = runEnable;
            };

            contextMenuStrip.Items.Add(toolStripMenuItem);
        }
    }
}
