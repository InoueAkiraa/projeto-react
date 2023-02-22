import React from "react";
import {Link, useParams} from 'react-router-dom';
import { FiCornerDownLeft, FiUserPlus } from "react-icons/fi";
import './index.css';

export function NovaCategoria(){

    const {categoriaId} = useParams();

    return(
        <div className="nova-categoria-container">
            <div className="content">                
                <section className="form">    
                    <FiUserPlus size={105} color="#17202a"></FiUserPlus>      
                    <h1>{categoriaId === '0'? 'Incluir Nova Categoria' : 'Atualizar Categoria'}</h1>
                    <Link className="back-link" to="/categoria">
                        <FiCornerDownLeft size={25} color="#17202a"/>
                        Retornar
                    </Link>          
                </section>
                
                <form>
                    <input placeholder="Descricão"></input>
                    <input placeholder="Data de Inclusão"></input>
                    <input placeholder="Ativo"></input>
                    <button className="button" type="submit">{categoriaId === '0'? 'Incluir' : 'Atualizar'}</button>
                </form>
            </div>
        </div>

    );
}