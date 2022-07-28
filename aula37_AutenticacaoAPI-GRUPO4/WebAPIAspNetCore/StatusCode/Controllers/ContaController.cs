using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatusCode.Models;
using StatusCode.Services;

namespace StatusCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private SistemaContext DbSistema = new SistemaContext();


        [HttpPost]
        [Route("CriarUsuario")]
        [AllowAnonymous]

        public ActionResult<Usuario> PublicarUmUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(new { menseger = "Usuário nulo" });
            }
            else
            {
                DbSistema.Usuario.Add(usuario);
                DbSistema.SaveChanges();

                return Ok(usuario);
            }
        }

        [HttpGet]
        [Route("ListaUsuarios")]
        [Authorize]
        public ActionResult<List<Usuario>> ListaUsuarios()
        {
            var Lista = DbSistema.Usuario.ToList();
            return Ok(Lista);
        }


        //HTTP POST AUTENTICAR ATIVIDADE 1

        //[HttpPost]
        //[Route("Autenticar")]
        //[AllowAnonymous]
        //public ActionResult<dynamic> Autenticar(Credencial credencial)
        //{
        //    // 1. Buscar um usuário que tenha o mesmo username
        //    // e senha que a credencial.
        //    var usuario = DbSistema.Usuario.Where(Usuario => Usuario.Nome == credencial.usuario && Usuario.Senha == credencial.senha).FirstOrDefault();

        //    // 2. Se usuário não for encontrado retorno Usuário ou Senha incorretos.
        //    if (usuario == null)
        //    {
        //        return NotFound(new { menseger = "Usuário ou senha incorretos." });
        //    }
        //    else
        //    {
        //        // 3. Caso usuário seja encontrado...

        //        // 3.1. Gerar um Token.

        //        var chaveToken = TokenService.GerarChaveToken();

        //        // 3.2. Retorno o Token.
        //        return Ok(new { token = chaveToken });
        //    }
        //}



        [HttpPost]
        [Route("Autenticar")]
        [AllowAnonymous]
        public ActionResult<dynamic> Autenticar(Credencial credencial)
        {

            // 1. Buscar um usuário que tenha o mesmo username
            // e senha que a credencial.
            var usuario = DbSistema.Usuario.Where(Usuario => Usuario.Nome == credencial.usuario && Usuario.Senha == credencial.senha).FirstOrDefault();

            // 2. Se usuário não for encontrado retorno Usuário ou Senha incorretos.
            if (usuario == null)
            {
                return NotFound(new { menseger = "Usuário ou senha incorretos." });
            }
            else
            {
                // 3. Caso usuário seja encontrado...

                // 3.1. Gerar um Token.

                var chaveToken = TokenService.GerarChaveToken();
                var usuarioEx = DbSistema.Usuario.Where(Usuario => Usuario.Nome == credencial.usuario && Usuario.Senha == credencial.senha).FirstOrDefault();
                DbSistema.Usuario.Any();
                // 3.2. Retorno o Token.
                return Ok(new { token = chaveToken , user = usuarioEx});

     

            }
        }
    }
}
