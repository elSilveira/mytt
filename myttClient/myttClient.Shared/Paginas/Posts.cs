using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using myttClient.Metodos;
using myttClient.Model;

namespace myttClient.Paginas
{
    public partial class Posts
    {
        private readonly Funcoes _func = new Funcoes();

        private async void Posts_OnLoaded(object sender, RoutedEventArgs e)
        {
            var req = new RequisicoesPost();
            await req.GetPosts().ContinueWith(async (t) =>
            {
                var msg = string.Empty;
                try
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        PostsListBox.ItemsSource = t.Result;
                    });
                }
                catch (Exception ex)
                {
                    msg = string.IsNullOrWhiteSpace(ex.Message)
                        ? ex.InnerException.Message
                        : ex.Message;    
                }

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    var dialog = new MessageDialog(msg);
                    await dialog.ShowAsync();
                }
            });
        }

        private void BtnNovoPost_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (NovoPost));
        }

        private void BtnLogoff_OnClick(object sender, RoutedEventArgs e)
        {
            _func.RemoveUsernameAndPasswordFromLocalStorage();

            Frame.Navigate(typeof(MainPage));
        }

        private void BtnSobre_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnPesquisar_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (Pesquisa));
        }
    }
}
