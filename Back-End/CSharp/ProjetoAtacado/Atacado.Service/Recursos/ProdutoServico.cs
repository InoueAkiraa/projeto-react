using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Atacado.Domain.EF;
using Atacado.Poco;
using Atacado.Service.Base;

namespace Atacado.Service.Recursos
{
    public class ProdutoServico : ServicoGenerico<Produto, ProdutoPoco>
    {
        public ProdutoServico(AtacadoContext contexto) : base(contexto) 
        { }

        public override List<ProdutoPoco> Listar(int? take = null, int? skip = null)
        {
            IQueryable<Produto> query;
            if (skip == null)
            {
                query = this.genrepo.GetAll();
            }
            else
            {
                query = this.genrepo.GetAll(take, skip);
            }
            return this.ConverterPara(query);
        }

        public override List<ProdutoPoco> Consultar(Expression<Func<Produto, bool>>? predicate = null)
        {
            IQueryable<Produto> query;
            if (predicate == null)
            {
                query = this.genrepo.Browseable(null);
            }
            else
            {
                query = this.genrepo.Browseable(predicate);
            }
            return this.ConverterPara(query);
        }

        public override List<ProdutoPoco> Vasculhar(int? take = null, int? skip = null, Expression<Func<Produto, bool>>? predicate = null)
        {
            IQueryable<Produto> query;
            if (skip == null)
            {
                if (predicate == null)
                {
                    query = this.genrepo.Browseable(null);
                }
                else
                {
                    query = this.genrepo.Browseable(predicate);
                }
            }
            else
            {
                if (predicate == null)
                {
                    query = this.genrepo.GetAll(take, skip);
                }
                else
                {
                    query = this.genrepo.Searchable(take, skip, predicate);
                }
            }
            return this.ConverterPara(query);
        }

        public override List<ProdutoPoco> ConverterPara(IQueryable<Produto> query)
        {
            return query.Select(pro =>
                new ProdutoPoco()
                {
                    CodigoProduto = pro.CodigoProduto,
                    CodigoCategoria = pro.CodigoCategoria,
                    Descricao = pro.Descricao,
                    Ativo = pro.Ativo,
                    DataInclusao = pro.DataInclusao
                }).ToList();
        }

        public override int ContarTotalRegistros(Expression<Func<Produto, bool>>? predicate)
        {
            IQueryable<Produto> query;
            if (predicate == null)
            {
                query = this.genrepo.Browseable(null);
            }
            else
            {
                query = this.genrepo.Browseable(predicate);
            }
            return query.Count();
        }
    }
}
