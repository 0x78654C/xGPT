using System.Net.NetworkInformation;
using System.Windows;

namespace xGPT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Check Inernet connection
            if (PingHost("8.8.8.8") == false)
            {
                MessageBox.Show("No internet connection!", this.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Close();
            }

            // Loading ChatGPT.
            LoadWeb();
        }

        /// <summary>
        /// Change webview2 size to window one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                gpt_web.Height = SystemParameters.WorkArea.Height - 24;
                gpt_web.Width = SystemParameters.WorkArea.Width;
                await gpt_web.EnsureCoreWebView2Async(null);
                return;
            }
            gpt_web.Height = this.Height - 30;
            gpt_web.Width = this.Width - 10;
        }

        /// <summary>
        /// Verifies if IP is up or not
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>verifies if IP is up or not</returns>
        public static bool PingHost(string ipAddress)
        {
            bool pingable = false;
            Ping? pinger = null;
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(ipAddress);
                pingable = reply.Status == IPStatus.Success;

            }
            catch
            {
                // We handle erros in other functions.
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return pingable;
        }

        /// <summary>
        /// Load web ChatGPT.
        /// </summary>
        private async void LoadWeb()
        {
            gpt_web.Source = new Uri($"https://chatgpt.com/?ref=dotcom");
            await gpt_web.EnsureCoreWebView2Async(null);
        }
    }
}