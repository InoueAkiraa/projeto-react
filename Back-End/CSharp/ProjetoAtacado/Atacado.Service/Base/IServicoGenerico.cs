using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Atacado.Service.Base
{
    public interface IServicoGenerico<TDominio, TPoco>
        where TDominio : class
        where TPoco : class
    {
        List<TPoco> Listar();

        List<TPoco> Listar(int? take = null, int? skip = null);

        List<TPoco> Consultar(Expression<Func<TDominio, bool>>? predicate = null);
        
        TPoco? PesquisarPorChave(object chave);
        
        TPoco? Inserir(TPoco poco);
        
        TPoco? Alterar(TPoco poco);
        
        TPoco? Excluir(object chave);
        
        TDominio ConverterPara(TPoco poco);
        
        TPoco ConverterPara(TDominio dominio);
        
        List<TPoco> ConverterPara(IQueryable<TDominio> query);
    }
}
