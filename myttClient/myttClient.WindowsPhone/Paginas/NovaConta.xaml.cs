using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using myttClient.Metodos;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace myttClient.Paginas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NovaConta
    {
        readonly StatusBar _statusBar = StatusBar.GetForCurrentView();

        public NovaConta()
        {
            InitializeComponent();
            WebView.ScriptNotify += WebView_ScriptNotify;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void WebView_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            ProgressRing.IsActive = false;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // esconde o statusBar
            await _statusBar.HideAsync();
        }

        private async void WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            // pega o valor passado pelo webview e converte em um array onde
            // a posicão 0 é o nome de usuário e a posição 1 é a senha.
            var splited = e.Value.Split('|');

            new Funcoes().SaveUserNameAndPassword(splited[0], splited[1]);

            await _statusBar.ShowAsync();

            Frame.Navigate(typeof(Posts));
        }

        private void BtnVoltar_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (MainPage));
        }
    }
}
