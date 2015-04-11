using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using myttClient.Metodos;
using myttClient.Model;

namespace myttClient.Paginas
{
    public partial class NovoPost
    {
        private readonly Funcoes _func = new Funcoes();

        private void TxtMensagem_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            var contador = TxtMensagem.Text.Length;

            LblContador.Text = string.Format("Restam {0} caracteres", 255 - contador);
        }

        private async void BtnEnviar_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtMensagem.Text))
            {
                var dialog = new MessageDialog("Campo obrigatório", "Atenção!");
                await dialog.ShowAsync();
            }

            try
            {
                var req = new RequisicoesPost();

                var post = new Post
                {
                    Id = 0,
                    IdParent = 0,
                    IsRT = false,
                    IdUsuario = 0,
                    PublishDate = DateTime.UtcNow.AddHours(-3),
                    Message = TxtMensagem.Text
                };

                await req.PostMensagem(post).ContinueWith(async (t) =>
                {
                    if (t.Result)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            Frame.Navigate(typeof (Posts));
                        });
                    }
                    else
                    {
                        var dialog = new MessageDialog(@"Ocorreu um erro desconhecido!\n\nTente novamente.", "Atenção!");
                        await dialog.ShowAsync();
                    }
                });
            }
            catch
            {
                // ignored
            }
        }

        private void BtnCancelar_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (Posts));
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
    }
}
