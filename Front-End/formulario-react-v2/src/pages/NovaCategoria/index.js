import React from "react";
import { FiUserPlus } from "react-icons/fi";
import './index.css';

export function NovaCategoria(){
    return(
        <div className="nova-categoria-container">
            <div className="content">                
                <section className="form">    
                    <FiUserPlus size={105} color="#17202a"></FiUserPlus>      
                    <h1>Texto</h1>
                    <Link className="back-link" to="/categoria"></Link>          
                </section>
                
                <form>
                    <input placeholder="Descricão"></input>
                    <input placeholder="Data de Inclusão"></input>
                    <input placeholder="Ativo"></input>
                    <button className="button" type="submit">Criar</button>
                </form>
            </div>
        </div>

    );
}