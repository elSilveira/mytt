using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using myttClient.Metodos;
using myttClient.Paginas;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace myttClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly StatusBar _statusBar = StatusBar.GetForCurrentView();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUserName.Text) || string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                var dialog = new MessageDialog("Campos obrigatórios!", "Atenção!");

                await dialog.ShowAsync();
            }
            else
            {
                ProgressRing.IsActive = true;

                var msgErro = string.Empty;
                try
                {
                    var login = new Login();

                    var token = await login.GetToken(TxtUserName.Text, TxtPassword.Text);

                    if (string.IsNullOrWhiteSpace(token))
                    {
                        throw new Exception("Token vazio! Por favor tente novamente!");
                    }

                    new Funcoes().SaveOrUpdateOnLocalStorage("token", token);

                    Frame.Navigate(typeof (Posts));
                }
                catch (Exception ex)
                {
                    msgErro = string.IsNullOrWhiteSpace(ex.Message) ? ex.InnerException.Message : ex.Message;
                }

                if (!string.IsNullOrWhiteSpace(msgErro))
                {
                    var dialog = new MessageDialog(msgErro, "Houve um erro na requisição!");

                    await dialog.ShowAsync();

                    ProgressRing.IsActive = false;
                }
            }
        }

        private void BtnNovaConta_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NovaConta));
        }
    }
}