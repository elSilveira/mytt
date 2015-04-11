using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using myttClient.Metodos;

namespace myttClient.Paginas
{
    public partial class NovaConta
    {
#if WINDOWS_PHONE_APP
        readonly StatusBar _statusBar = StatusBar.GetForCurrentView();
#endif

        public NovaConta()
        {
            InitializeComponent();
            WebView.ScriptNotify += WebView_ScriptNotify;
        }

#if WINDOWS_PHONE_APP
        private async void Page_Loaded(object sender, RoutedEventArgs e)
#else
        private void Page_Loaded(object sender, RoutedEventArgs e)
#endif
        {
#if WINDOWS_PHONE_APP
            // esconde o statusBar
            await _statusBar.HideAsync();
#endif
        }


#if WINDOWS_PHONE_APP
        private async void WebView_ScriptNotify(object sender, NotifyEventArgs e) 
#else
        private void WebView_ScriptNotify(object sender, NotifyEventArgs e)
#endif
        {
            // pega o valor passado pelo webview e converte em um array onde
            // a posicão 0 é o nome de usuário e a posição 1 é a senha.
            var splited = e.Value.Split('|');

            new Funcoes().SaveUserNameAndPassword(splited[0], splited[1]);

#if WINDOWS_PHONE_APP
            await _statusBar.ShowAsync();
#endif

            Frame.Navigate(typeof(Posts));
        }

        private void BtnVoltar_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void WebView_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            ProgressRing.IsActive = false;
        }
    }
}
