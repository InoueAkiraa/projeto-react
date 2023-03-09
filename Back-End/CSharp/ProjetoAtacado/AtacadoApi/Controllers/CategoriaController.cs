using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Atacado.Domain.EF;
using Atacado.Envelope.Modelo;
using Atacado.Envelope.Motor;
using Atacado.Poco;
using Atacado.Service.Recursos;

namespace AvaliarApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/supermercado/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        private CategoriaServico servico;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contexto"></param>
        public CategoriaController(AtacadoContext contexto) : base()
        {
            this.servico = new CategoriaServico(contexto);
        }

        /// <summary>
        /// Retorna todos os registros existentes na tabela Categoria utilizando parâmetros de paginação.
        /// </summary>
        /// <param name="take">Quantos registros você deseja obter.</param>
        /// <param name="skip">Quantos registros você deseja pular.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<CategoriaPoco>> GetAll(int? take = null, int? skip = null)
        {
            try
            {
                List<CategoriaPoco> listaPoco = this.servico.Listar(take, skip);
                return Ok(listaPoco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Retorna o registro de acordo com a chave primária especificada.
        /// </summary>
        /// <param name="chave">Código da chave primária.</param>
        /// <returns></returns>
        [HttpGet("{chave:int}")]
        public ActionResult<CategoriaPoco> GetById(int chave)
        {
            try
            {
                CategoriaPoco poco = this.servico.PesquisarPorChave(chave);
                return Ok(poco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Realiza a criação de um novo registro de Categoria.
        /// </summary>
        /// <param name="poco">Body do registro a ser cadastrado.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<CategoriaPoco> Post([FromBody] CategoriaPoco poco)
        {
            try
            {
                CategoriaPoco novoPoco = this.servico.Inserir(poco);
                return Ok(novoPoco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Altera um dado existente dentro da tabela Categoria.
        /// </summary>
        /// <param name="poco"> Body do dado que será alterado (pela chave primária informada). </param>
        /// <returns> Altera o dado selecionado. </returns>
        [HttpPut]
        public ActionResult<CategoriaPoco> Put([FromBody] CategoriaPoco poco)
        {
            try
            {
                CategoriaPoco novoPoco = this.servico.Alterar(poco);
                return Ok(novoPoco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Exclui um registro existente na tabela Categoria, utilizando a chave primária.
        /// </summary>
        /// <param name="chave"> Chave primária para localização.</param>
        /// <returns> Dado excluido por Id. </returns>
        [HttpDelete("{chave:int}")]
        public ActionResult<CategoriaPoco> DeleteById(int chave)
        {
            try
            {
                CategoriaPoco poco = this.servico.Excluir(chave);
                return Ok(poco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Retorna todos os registros de modo envelopado para o arquivo JSON.
        /// </summary>
        /// <param name="limite">Quantos registros você deseja obter.</param>
        /// <param name="salto">Quantos registros você deseja pular.</param>
        /// <returns></returns>
        [HttpGet("envelope/")]
        public ActionResult<EnvelopeGenerico<CategoriaEnvelope>> GetAllEnvelope(int? limite = null, int? salto = null)
        {
            try
            {
                List<CategoriaPoco> listaPoco = this.servico.Listar(null, null);
                int totalReg = listaPoco.Count;
                listaPoco = this.servico.Listar(limite, salto);
                return Envelopamento(totalReg, limite, salto, listaPoco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Retorna o registro de acordo com o código da chave primária informada.
        /// </summary>
        /// <param name="chave">Código da chave primária do registro</param>
        /// <returns></returns>
        [HttpGet("envelope/{chave:int}")]
        public ActionResult<CategoriaEnvelope> GetByIdEnvelope(int chave)
        {
            try
            {
                CategoriaPoco poco = this.servico.PesquisarPorChave(chave);
                CategoriaEnvelope envelope = new CategoriaEnvelope(poco);
                envelope.SetLinks();
                return Ok(envelope);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        private ActionResult<EnvelopeGenerico<CategoriaEnvelope>> Envelopamento(int? totalReg, int? limite, int? salto, List<CategoriaPoco> listaPoco)
        {
            string linkPost = "POST /categoria";
            ListEnvelope<CategoriaEnvelope> list;

            if (limite > totalReg)
            {
                string erro = "Limite não pode ser maior que a quantidade de Registros.";
                list = new ListEnvelope<CategoriaEnvelope>(null, 400, erro, linkPost, "1.0");
                return BadRequest(list.Etapa);
            }
            else
            {
                List<CategoriaEnvelope> listaEnvelope = listaPoco.Select(cat => new CategoriaEnvelope(cat)).ToList();
                listaEnvelope.ForEach(item => item.SetLinks());

                if (listaPoco.Count() == 0)
                {
                    list = new ListEnvelope<CategoriaEnvelope>(listaEnvelope, 404, "Não existem mais registros a serem mostrados!", linkPost, "1.0");
                    return Ok(list.Etapa);
                }

                if (salto == null)
                {
                    list = new ListEnvelope<CategoriaEnvelope>(listaEnvelope, 200, "OK", linkPost, "1.0");
                    list.Etapa.Paginacao.TotalReg = totalReg;
                }
                else
                {
                    var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}");
                    string urlServidor = location.AbsoluteUri;
                    list = new ListEnvelope<CategoriaEnvelope>(listaEnvelope, 200, "OK", linkPost, "1.0", urlServidor, salto, limite, totalReg);
                }
                return Ok(list.Etapa);
            }
        }
    }
}
