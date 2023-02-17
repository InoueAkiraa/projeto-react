import React from "react";
import {Link} from 'react-router-dom';
import './index.css';
import logoCadastro from '../../assets/etiquetas.png';
import {FiXCircle, FiEdit, FiUserX} from 'react-icons/fi';

export function Categorias(){
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
                <li>
                    <b>Descricão:</b>Produto2<br></br>
                    <b>Data de Inserção:</b>01/02/2023<br></br>
                    <b>Ativo:</b>True<br></br>
                    <button type="button">
                        <FiEdit size="25" color="#17202a"/>
                    </button>

                    <button type="button">
                        <FiUserX size="25" color="#17202a"/>
                    </button>
                </li>

                <li>
                    <b>Descricão:</b>Produto2<br></br>
                    <b>Data de Inserção:</b>01/02/2023<br></br>
                    <b>Ativo:</b>True<br></br>
                    <button type="button">
                        <FiEdit size="25" color="#17202a"/>
                    </button>

                    <button type="button">
                        <FiUserX size="25" color="#17202a"/>
                    </button>
                </li>
            </ul>
        </div>
    )
}