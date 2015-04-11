using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using myttClient.Metodos;
using myttClient.Model;

namespace myttClient.Paginas
{
    public partial class Posts
    {
        private readonly Funcoes _func = new Funcoes();

        private void Posts_OnLoaded(object sender, RoutedEventArgs e)
        {
            var posts = new List<Mensagens>
            {
                new Mensagens
                {
                    Id = 1,
                    Data = "24/05/2015 14h35",
                    Texto = "asdfasfhasdjkfhasfha s dflkahsd flkas dflkhasdlfhasld fhasdkf asdlfk aslkdf askdf asda",
                    Username = "andrelima"
                },
                new Mensagens
                {
                    Id = 2,
                    Data = "25/05/2015 14h35",
                    Texto = "asdfasfhasdjkfhasfha s dflkahsd flkas",
                    Username = "andrelima"
                },
                new Mensagens
                {
                    Id = 3,
                    Data = "26/05/2015 14h35",
                    Texto = "asdfasfhasdjkfhasfha s dflkahsd flkas asdf j~çlapiu sdfuhasdfhasldkjfh askfyhasdo fhasdkfh askfha skjd",
                    Username = "andrelima"
                },
            };

            PostsListBox.ItemsSource = posts;
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
            throw new NotImplementedException();
        }
    }
}
