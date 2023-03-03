import React, {useState, useEffect} from "react";
import {Link, useNavigate} from 'react-router-dom';
import './index.css';
import logoCadastro from '../../assets/etiquetas.png';
import {FiXCircle, FiEdit, FiUserX} from 'react-icons/fi';
import api from '../../services/api';

export function Categorias(){
    //filtrar dados
    const [searchInput, setSearchInput] = useState('');
    const [filtro, setFiltro] = useState([]);

    const [categorias, setCategorias] = useState([]);    
    const navigate = useNavigate();

    const searchCategorias = (searchValue) => {
        setSearchInput(searchValue);
        if (searchInput !== ''){
            const dadosFiltrados = categorias.filter((item) => {
                return Object.values(item).join('').toLowerCase()
                .includes(searchInput.toLowerCase())
            });
            setFiltro(dadosFiltrados);
        }
        else{
            setFiltro(categorias);
        }
    }

    useEffect( () => {
        api.get('https://localhost:7290/api/supermercado/Categoria').then(
            response => {setCategorias(response.data);
            }
        )
    })

    async function editCategoria(codigoCategoria){
        try {
            navigate(`/categoria/novo/${codigoCategoria}`);
        }catch(error){
        alert('Erro ao recuperar os dados');
        }
    }

    async function deleteCategoria(codigoCategoria){
        try{
            if(window.confirm('Deseja excluir a categoria com o seguinte id = ' + codigoCategoria + ' ? '))
            {
                await api.delete(`https://localhost:7290/api/supermercado/Categoria/${codigoCategoria}`);
                setCategorias(categorias.filter(categoria => categoria.codigoCategoria !== codigoCategoria));
            }

        }catch(error){
            alert('Erro ao excluir a categoria');
        }
    }

    return (
        <div className="categoria-container">
            <header>
                <img src={logoCadastro} alt="Cadastro"/>
                <Link className="button" to="/categoria/novo/0">Nova Categoria</Link>
                <button type="button">
                    <FiXCircle size={40} color="#17202a"/>
                </button>
            </header>

            <form className="categoria-form">
                <input type="text" placeholder="Filtrar por nome..."
                onChange={(e) => searchCategorias(e.target.value)}
                />                                            
            </form>

            <h1> Registros de Categoria </h1>
            {searchInput.length > 1 ?(
                <ul>
                {filtro.map(categoria => (
                <li key={categoria.id}>                                
                    <b>Id da Categoria: </b>{categoria.codigoCategoria}<br></br>
                    <b>Descrição: </b>{categoria.descricao}<br></br>
                    <b>Data de Inclusão: </b>{categoria.dataInclusao}<br></br>
                    <b>Ativo: </b>{categoria.ativo ? 'Sim' : 'Não'}<br></br>

                    <button onClick={() => editCategoria(categoria.codigoCategoria)} type="button">
                        <FiEdit size="25" color="#17202a"/>
                    </button>

                    <button onClick={() => deleteCategoria(categoria.codigoCategoria)}type="button">
                        <FiUserX size="25" color="#17202a"/>
                    </button>
                </li>
                ))}                
            </ul>
            ) : (
            <ul>
                {categorias.map(categoria => (
                <li key={categoria.id}>                                
                    <b>Id da Categoria: </b>{categoria.codigoCategoria}<br></br>
                    <b>Descrição: </b>{categoria.descricao}<br></br>
                    <b>Data de Inclusão: </b>{categoria.dataInclusao}<br></br>
                    <b>Ativo: </b>{categoria.ativo ? 'Sim' : 'Não'}<br></br>

                    <button onClick={() => editCategoria(categoria.codigoCategoria)} type="button">
                        <FiEdit size="25" color="#17202a"/>
                    </button>

                    <button type="button">
                        <FiUserX size="25" color="#17202a"/>
                    </button>
                </li>
                ))}                
            </ul>    
            )}                
        </div>
    );
}