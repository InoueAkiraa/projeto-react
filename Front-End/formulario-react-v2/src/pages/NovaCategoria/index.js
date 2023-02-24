import React, { useEffect, useState } from "react";
import {Link, useNavigate, useParams} from 'react-router-dom';
import { FiCornerDownLeft, FiUserPlus } from "react-icons/fi";
import './index.css';
import api from "../../services/api";

export function NovaCategoria(){

    const [id, setId] = useState(null);
    const [descricao, setDescricao] = useState('');
    const {categoriaId} = useParams();
    const navegar = useNavigate();

    useEffect(() => {
        if (categoriaId === '0')
            return;
        else
            loadCategoria();
    }, categoriaId);

    async function loadCategoria(){
        try {
            const response = await api.get(`https://localhost:7290/api/supermercado/Categoria/${categoriaId}`);            
            setId(response.data.id); 
            setDescricao(response.data.descricao);
        }catch(error){
            alert('Erro ao recuperar a categoria ' + error);
            navegar.push('/categoria');
        }
    }    

    async function saveOrUpdate(event){
        const data = {
            descricao,
            ativo
        }

        try{
            if(categoriaId === '0'){
                await api.post('https://localhost:7290/api/supermercado/', data); 
            }
            else{
                data.id = id;
                await api.put(`https://localhost:7290/api/supermercado/${alunoId}`, data);
            }            
        }catch(error){
            alert('Erro ao recuperar a categoria ' + error);
        }
        Navigate.push('/categoria');
    }

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
                    <input placeholder="Descricão">
                        value={descricao}
                        onchange={e=> setDescricao(e.target.value)}
                    </input>
                    {/* CORRIGIR */}
                    <input placeholder="Data de Inclusão">
                        value={null}
                        onchange={e=> setDataInclusao(e.target.value)}
                    </input>
                    <input placeholder="Ativo"></input>
                    <button className="button" type="submit">{categoriaId === '0'? 'Incluir' : 'Atualizar'}</button>
                </form>
            </div>
        </div>

    );
}