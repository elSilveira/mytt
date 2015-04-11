using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using myttClient.Model;
using Newtonsoft.Json;

namespace myttClient.Metodos
{
    public class RequisicoesPost
    {
        private readonly Funcoes _func = new Funcoes();

        public async Task<List<Post>> GetPosts()
        {
            // GET api/usuario/posts?id={id}
            var usuarioJson = _func.GetValuesOnLocalStorage("Usuario").ToString();
            var token = _func.GetValuesOnLocalStorage("token").ToString();

            var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);

            var client = new HttpClient { BaseAddress = new Uri(App.UrlBase) };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = await client.GetAsync("api/usuario/posts?id=" + usuario.Id);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<List<Post>>(json);

            return model;
        }

        public async Task<bool> PostMensagem(Post model)
        {
            // POST api/Post
            var usuarioJson = _func.GetValuesOnLocalStorage("Usuario").ToString();
            var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);
            var token = _func.GetValuesOnLocalStorage("token").ToString();
            var client = new HttpClient { BaseAddress = new Uri(App.UrlBase) };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var d = new
                {
                    Id = 0, 
                    model.Message, 
                    model.PublishDate, 
                    model.IdParent, 
                    model.IsRT, 
                    IdUsuario = usuario.Id
                };

            var json = JsonConvert.SerializeObject(d);

            var postData = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Post", postData);
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<Post>(await response.Content.ReadAsStringAsync());

            return result.Id > 0;
        }
    }
}
