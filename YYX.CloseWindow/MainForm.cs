using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace YYX.CloseWindow
{
    public partial class MainForm : Form
    {
        private string lpWindowName;
        private bool realClose;

        public MainForm()
        {
            InitializeComponent();

            notifyIcon.ContextMenuStrip=  CreateNotifyIconContextMenuStrip();

            CloseWindow();

            var closeWindowConfig = XmlSerializerHelper<CloseWindowConfig>.Load();
            lpWindowName = closeWindowConfig.WindowTitle;
            textBoxWindowTitle.Text = lpWindowName;

            var timer = new Timer { Interval = 100, AutoReset = true};
            timer.Elapsed += (sender, eve) => { CloseWindow(); };
            timer.Start();
        }

        private ContextMenuStrip CreateNotifyIconContextMenuStrip()
        {
            var contextMenuStrip = new ContextMenuStrip();
            {
                var closeItem = contextMenuStrip.Items.Add("关闭");
                closeItem.Click += (sender, e) =>
                {
                    realClose = true;
                    Close();
                };
            }

            contextMenuStrip.AddAutoRun();

            return contextMenuStrip;
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void CloseWindow()
        {
            const int WM_CLOSE = 0x10;
            var windowHandle = (IntPtr) FindWindow(null, lpWindowName);
            if (windowHandle != IntPtr.Zero)
            {
                SendMessage(windowHandle, WM_CLOSE, 0, 0);
            }
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            lpWindowName = textBoxWindowTitle.Text;
            var closeWindowConfig = new CloseWindowConfig(lpWindowName);
            XmlSerializerHelper<CloseWindowConfig>.Save(closeWindowConfig);
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Visible = true;
                BringToFront();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!realClose)
            {
                e.Cancel = true;
                Visible = false;
            }
        }
    }
}