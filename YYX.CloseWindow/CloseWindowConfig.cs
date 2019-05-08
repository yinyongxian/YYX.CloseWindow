using System;

namespace YYX.CloseWindow
{
    [Serializable]
    public class CloseWindowConfig
    {
        private string windowTitle;

        public CloseWindowConfig()
        {
            windowTitle = "窗口标题";
        }

        public CloseWindowConfig(string windowTitle)
        {
            this.windowTitle = windowTitle;
        }

        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; }
        }
    }
}
