using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using mytt.Models;

namespace mytt.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        private Contexto db = new Contexto();

        [Route("api/usuario/posts")]
        public async Task<IList<Post>> GetPost(string id)
        {
            var sql = string.Format("SELECT * FROM Post p WHERE IdUsuario = {0} OR IdUsuario IN (SELECT IdUsuarioSeguido FROM Seguidores WHERE IdUsuarioSeguidor = {0}) OR Message LIKE '%@{1}%' ORDER BY PublishDate DESC",
                id, User.Identity.Name);
            var posts = await db.Post.SqlQuery(sql).ToListAsync();
            return posts;
        }

        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> GetPost(int id)
        {
            Post post = await db.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        public async Task<IHttpActionResult> PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.PublishDate = DateTime.Now;

            db.Post.Add(post);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, post);
        }

        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> DeletePost(int id)
        {
            Post post = await db.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            db.Post.Remove(post);
            await db.SaveChangesAsync();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Post.Count(e => e.Id == id) > 0;
        }
    }
}