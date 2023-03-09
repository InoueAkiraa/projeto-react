using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Atacado.Domain.EF;
using Atacado.Envelope.Modelo;
using Atacado.Envelope.Motor;
using Atacado.Poco;
using Atacado.Service.Recursos;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using LinqKit;

namespace AvaliarApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/supermercado/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        private ProdutoServico servico;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contexto"></param>
        public ProdutoController(AtacadoContext contexto) : base()
        {
            this.servico = new ProdutoServico(contexto);
        }

        /// <summary>
        /// Retorna todos os registros existentes na tabela Produto utilizando parâmetros de paginação.
        /// </summary>
        /// <param name="take">Quantos registros você deseja obter.</param>
        /// <param name="skip">Quantos registros você deseja pular.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<ProdutoPoco>> GetAll(int? take = null, int? skip = null)
        {
            try
            {
                List<ProdutoPoco> listaPoco = this.servico.Listar(take, skip);
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
        public ActionResult<ProdutoPoco> GetById(int chave)
        {
            try
            {
                ProdutoPoco poco = this.servico.PesquisarPorChave(chave);
                return Ok(poco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Realiza a criação de um novo registro de Produto.
        /// </summary>
        /// <param name="poco">Body do registro a ser cadastrado.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ProdutoPoco> Post([FromBody] ProdutoPoco poco)
        {
            try
            {
                ProdutoPoco novoPoco = this.servico.Inserir(poco);
                return Ok(novoPoco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Altera um dado existente dentro da tabela Produto.
        /// </summary>
        /// <param name="poco"> Body do dado que será alterado (pela chave primária informada). </param>
        /// <returns> Altera o dado selecionado. </returns>
        [HttpPut]
        public ActionResult<ProdutoPoco> Put([FromBody] ProdutoPoco poco)
        {
            try
            {
                ProdutoPoco novoPoco = this.servico.Alterar(poco);
                return Ok(novoPoco);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// Exclui um registro existente na tabela Produto, utilizando a chave primária.
        /// </summary>
        /// <param name="chave"> Chave primária para localização.</param>
        /// <returns> Dado excluido por Id. </returns>
        [HttpDelete("{chave:int}")]
        public ActionResult<ProdutoPoco> DeleteById(int chave)
        {
            try
            {
                ProdutoPoco poco = this.servico.Excluir(chave);
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
        public ActionResult<EnvelopeGenerico<ProdutoEnvelope>> GetAllEnvelope(int? limite = null, int? salto = null)
        {
            try
            {
                List<ProdutoPoco> listaPoco = this.servico.Listar(null, null);
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
        /// Retorna todos os registros de modo envelopado para o arquivo JSON filtrados pelo código de Categoria.
        /// </summary>
        /// <param name="catcod"></param>
        /// <param name="limite"></param>
        /// <param name="salto"></param>
        /// <returns></returns>
        [HttpGet("envelope/PorCategoria/{catcod:int}")]
        public ActionResult<EnvelopeGenerico<ProdutoEnvelope>> GetPorCategoriaEnvelope(int catcod, int? limite = null, int? salto = null)
        {
            try
            {
                List<ProdutoPoco> listaPoco;
                var predicado = PredicateBuilder.New<Produto>(true);
                int totalReg = 0;
                if (limite == null)
                {
                    if (salto != null)
                    {
                        return BadRequest("Informe os parâmetros Take e Skip.");
                    }
                    else
                    {
                        predicado = predicado.And(s => s.CodigoCategoria == catcod);
                        listaPoco = this.servico.Consultar(predicado);
                        totalReg = listaPoco.Count;
                        return Envelopamento(totalReg, limite, salto, listaPoco);
                    }
                }
                else
                {
                    if (salto == null)
                    {
                        return BadRequest("Informe os parâmetros Take e Skip.");
                    }
                    else
                    {
                        predicado = predicado.And(s => s.CodigoCategoria == catcod);
                        totalReg = this.servico.ContarTotalRegistros(predicado);
                        listaPoco = this.servico.Vasculhar(limite, salto, predicado);
                        return Envelopamento(totalReg, limite, salto, listaPoco);
                    }
                }
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
        public ActionResult<ProdutoEnvelope> GetByIdEnvelope(int chave)
        {
            try
            {
                ProdutoPoco poco = this.servico.PesquisarPorChave(chave);
                ProdutoEnvelope envelope = new ProdutoEnvelope(poco);
                envelope.SetLinks();
                return Ok(envelope);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        private ActionResult<EnvelopeGenerico<ProdutoEnvelope>> Envelopamento(int? totalReg, int? limite, int? salto, List<ProdutoPoco> listaPoco)
        {
            string linkPost = "POST /produto";
            ListEnvelope<ProdutoEnvelope> list;

            if (limite > totalReg)
            {
                List<ProdutoEnvelope> listaEnvelope = listaPoco.Select(pro => new ProdutoEnvelope(pro)).ToList();
                listaEnvelope.ForEach(item => item.SetLinks());
                string erro = "A quantidade de registros solicitada não pode ser maior do que a quantidade total de Registros existentes." + "[" + totalReg +"]";
                list = new ListEnvelope<ProdutoEnvelope>(null, 400, erro, linkPost, "1.0");
                return Ok(list.Etapa);
            }
            else
            {
                List<ProdutoEnvelope> listaEnvelope = listaPoco.Select(pro => new ProdutoEnvelope(pro)).ToList();
                listaEnvelope.ForEach(item => item.SetLinks());

                if (listaPoco.Count() == 0)
                {
                    list = new ListEnvelope<ProdutoEnvelope>(null, 404, "Não existem mais registros a serem mostrados!", linkPost, "1.0");
                    return Ok(list.Etapa);
                }

                if (salto == null)
                {
                    list = new ListEnvelope<ProdutoEnvelope>(listaEnvelope, 200, "OK", linkPost, "1.0");
                    list.Etapa.Paginacao.TotalReg = totalReg;
                }
                else
                {
                    var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}");
                    string urlServidor = location.AbsoluteUri;
                    list = new ListEnvelope<ProdutoEnvelope>(listaEnvelope, 200, "OK", linkPost, "1.0", urlServidor, salto, limite, totalReg);
                }
                return Ok(list.Etapa);
            }
        }
    }
}
