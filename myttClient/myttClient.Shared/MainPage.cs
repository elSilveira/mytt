using System;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using myttClient.Metodos;
using myttClient.Paginas;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace myttClient
{
    public partial class MainPage
    {
#if WINDOWS_PHONE_APP
        readonly StatusBar _statusBar = StatusBar.GetForCurrentView();
#endif
        private readonly Funcoes _func = new Funcoes();

        private async void BtnLogin_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUserName.Text) || string.IsNullOrWhiteSpace(TxtPassword.Password))
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
                    await Login(false);
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = false;
            
            if (_func.HasUsernameAndPassword())
            {
                ProgressRing.IsActive = true;
                try
                {
                    await Login(true);
                }
                catch
                {
                    // ignored
                }
            }
        }

        internal async Task Login(bool autoLogin)
        {
            try
            {
                var login = new Login();

                var userName = TxtUserName.Text;
                var password = TxtPassword.Password;

                if (autoLogin)
                {
                    userName = _func.GetValuesOnLocalStorage("Username").ToString();
                    password = _func.GetValuesOnLocalStorage("Password").ToString();
                }

                await login.GetToken(userName, password).ContinueWith(async (t) =>
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(t.Result))
                            {
                                throw new Exception("Token vazio! Por favor tente novamente!");
                            }

                            _func.SaveOrUpdateOnLocalStorage("token", t.Result);

                            await login.GetUsuario(t.Result);

                            if (!(string.IsNullOrWhiteSpace(TxtPassword.Password) &&
                                  string.IsNullOrWhiteSpace(TxtUserName.Text)))
                            {
                                _func.SaveUserNameAndPassword(TxtUserName.Text, TxtPassword.Password); 
                            }

                            TxtUserName.Text = string.Empty;
                            TxtPassword.Password = string.Empty;

                            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                Frame.Navigate(typeof(Posts));
                            });
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    });
               
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
