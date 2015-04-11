using System;
using System.Net.Http;
using System.Threading.Tasks;
using myttClient.Model;
using Newtonsoft.Json;


namespace myttClient.Metodos
{
    public class Login
    {
        private readonly Funcoes _func = new Funcoes();

        public async Task<string> GetToken(string username, string password)
        {
            try
            {
                var client = new HttpClient {BaseAddress = new Uri(App.UrlBase)};

                client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");

                var dados = string.Format("grant_type=password&username={0}&password={1}", username, password);

                var response = await client.PostAsync("api/acesso/token", new StringContent(dados));

                response.EnsureSuccessStatusCode();

                var token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());

                return token.access_token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task GetUsuario(string token)
        {
            try
            {
                var client = new HttpClient { BaseAddress = new Uri(App.UrlBase) };

                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                
                var response = await client.GetAsync("api/usuario/usuariologado");

                response.EnsureSuccessStatusCode();

                var usuario = await response.Content.ReadAsStringAsync();

                _func.SaveOrUpdateOnLocalStorage("Usuario", usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
