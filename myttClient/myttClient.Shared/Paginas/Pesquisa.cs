using System;
using System.Collections.Generic;
using System.Text;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using myttClient.Metodos;

namespace myttClient.Paginas
{
    public partial class Pesquisa
    {
        private readonly Funcoes _func = new Funcoes();

        private void BtnLogoff_OnClick(object sender, RoutedEventArgs e)
        {
            _func.RemoveUsernameAndPasswordFromLocalStorage();

            Frame.Navigate(typeof(MainPage));
        }

        private void BtnSobre_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void TxtPesquisa_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                try
                {
                    var login = new Login();
                    await login.Pesquisar(TxtPesquisa.Text).ContinueWith(async (t) =>
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            LstUsuarios.ItemsSource = t.Result;
                        });
                    });
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}
