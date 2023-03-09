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
    public class CategoriaServico : ServicoGenerico<Categoria, CategoriaPoco>
    {
        public CategoriaServico(AtacadoContext contexto) : base(contexto) 
        { }

        public override List<CategoriaPoco> Listar(int? take = null, int? skip = null)
        {
            IQueryable<Categoria> query;
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

        public override List<CategoriaPoco> Consultar(Expression<Func<Categoria, bool>>? predicate = null)
        {
            IQueryable<Categoria> query;
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

        public override List<CategoriaPoco> Vasculhar(int? take = null, int? skip = null, Expression<Func<Categoria, bool>>? predicate = null)
        {
            IQueryable<Categoria> query;
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

        public override List<CategoriaPoco> ConverterPara(IQueryable<Categoria> query)
        {
            return query.Select(cat =>
                new CategoriaPoco()
                {
                    CodigoCategoria = cat.CodigoCategoria,
                    Descricao = cat.Descricao,
                    DataInclusao = cat.DataInclusao,
                    Ativo = cat.Ativo
                }).ToList();
        }

        public override int ContarTotalRegistros(Expression<Func<Categoria, bool>>? predicate)
        {
            IQueryable<Categoria> query;
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
