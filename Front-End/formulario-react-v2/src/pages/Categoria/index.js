import React, {useState, useEffect} from "react";
import {Link, Navigate} from 'react-router-dom';
import './index.css';
import logoCadastro from '../../assets/etiquetas.png';
import {FiXCircle, FiEdit, FiUserX} from 'react-icons/fi';
import api from '../../services/api';

export function Categorias(){
    const [categorias, setCategorias] = useState([]);    

    useEffect( () => {
        api.get('https://localhost:7290/api/supermercado/Categoria').then(
            response => {setCategorias(response.data);
            }
        )
    })

    async function editAluno(id){
        try {
            Navigate(`categoria/novo/${id}`);
        }catch(error){
        alert('Erro ao recuperar os dados');
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
                <input type="text" placeholder="Nome"/>
                <button type="button" class='button'>
                    Filtrar por nome
                </button>
            </form>
            <h1> Registros de Categoria </h1>
            <ul>
                {categorias.map(categoria => (
                <li key={categoria.id}>                                
                    <b>Id da Categoria: </b>{categoria.codigoCategoria}<br></br>
                    <b>Descrição: </b>{categoria.descricao}<br></br>
                    <b>Data de Inclusão: </b>{categoria.dataInclusao}<br></br>
                    <b>Ativo: </b>{categoria.ativo ? 'Sim' : 'Não'}<br></br>

                    <button onClick={() => editAluno(categoria.id)} type="button">
                        <FiEdit size="25" color="#17202a"/>
                    </button>

                    <button type="button">
                        <FiUserX size="25" color="#17202a"/>
                    </button>
                </li>
                ))}                
            </ul>
        </div>
    )
}