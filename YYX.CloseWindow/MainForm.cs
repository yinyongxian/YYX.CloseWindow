using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace YYX.CloseWindow

{
    public partial class MainForm : Form
    {
        private const string LpWindowName = @"close.ico - Windows 照片查看器";

        public MainForm()
        {
            InitializeComponent();

            notifyIcon.ContextMenuStrip=  CreateNotifyIconContextMenuStrip();

            CloseWindow();

            var timer = new Timer { Interval = 100, AutoReset = true};
            timer.Elapsed += (sender, eve) => { CloseWindow(); };
            timer.Start();
        }

        private ContextMenuStrip CreateNotifyIconContextMenuStrip()
        {
            var contextMenuStrip = new ContextMenuStrip();
            {
                var closeItem = contextMenuStrip.Items.Add("关闭");
                closeItem.Click += (sender, e) => { Close(); };
            }

            contextMenuStrip.AddAutoRun();

            return contextMenuStrip;
        }

        protected override void OnShown(EventArgs e)
        {
            Hide();
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private static void CloseWindow()
        {
            const int WM_CLOSE = 0x10;
            var windowHandle = (IntPtr) FindWindow(null, LpWindowName);
            if (windowHandle != IntPtr.Zero)
            {
                SendMessage(windowHandle, WM_CLOSE, 0, 0);
            }
        }
    }
}