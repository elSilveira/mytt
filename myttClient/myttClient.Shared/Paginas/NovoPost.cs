using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using myttClient.Metodos;

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

        private void BtnEnviar_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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
