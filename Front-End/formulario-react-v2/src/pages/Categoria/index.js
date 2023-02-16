import React from "react";
import {Link} from 'react-router-dom';
import './index.css';
import logoCadastro from '../../assets/etiquetas.png';
import {FiXCircle} from 'react-icons/fi';

export function Categorias(){
    return (
        <div className="categoria-container">
            <header>
                <img src={logoCadastro} alt="Cadastro"/>
                <Link className="button" to="categoria/nova">Nova Categoria</Link>
                <button type="button">
                    <FiXCircle size={35} color="#17202a"/>
                </button>
            </header>

            <form>
                <input type="text" placeholder="Nome"/>
                <button type="button" class='button'>
                    Filtrar categoria por nome (parcial)
                </button>
            </form>
            <h1> Relação de Alunos </h1>
        </div>
    )
}