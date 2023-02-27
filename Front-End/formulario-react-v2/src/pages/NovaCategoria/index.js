import React, { useEffect, useState } from "react";
import {Link, useNavigate, useParams} from 'react-router-dom';
import { FiCornerDownLeft, FiUserPlus } from "react-icons/fi";
import './index.css';
import api from "../../services/api";

export function NovaCategoria(){

    const [id, setId] = useState(null);
    const [descricao, setDescricao] = useState('');
    const [dataInclusao, setDataInclusao] = useState(null);
    const [ativo, setAtivo] = useState(null);
    const {categoriaId} = useParams();
    const navegar = useNavigate();

    useEffect(() => {
        if (categoriaId === '0')
            return;
        else
            loadCategoria();
    }, categoriaId)

    async function loadCategoria(){
        try {
            const response = await api.get(`https://localhost:7290/api/supermercado/Categoria/${categoriaId}`);            
            setId(response.data.id); 
            setDescricao(response.data.descricao);
        }catch(error){
            alert('Erro ao recuperar a categoria ' + error);
            navegar('/categoria');
        }
    }    

    async function saveOrUpdate(event){
        const data = {
            descricao,
            dataInclusao,
            ativo
        }

        try{
            if(categoriaId === '0'){
                await api.post('https://localhost:7290/api/supermercado/Categoria', data); 
                alert("Categoria criada com sucesso !!");
            }
            else{
                data.id = id;
                await api.put('https://localhost:7290/api/supermercado/Categoria', data);
            }            
        }catch(error){
            alert('Erro ao recuperar a categoria ' + error);
        }
        navegar('/categoria');
    }

    return(
        <div className="nova-categoria-container">
            <div className="content">                
                <section className="form">    
                    <FiUserPlus size={105} color="#17202a"></FiUserPlus>      
                    <h1>{categoriaId === '0'? 'Incluir Nova Categoria' : `Atualizar Categoria: ${categoriaId}`}</h1>
                    <Link className="back-link" to="/categoria">
                        <FiCornerDownLeft size={25} color="#17202a"/>
                        Retornar
                    </Link>          
                </section>
                
                <form onSubmit={saveOrUpdate}>                                    
                    <input placeholder="Descricão"
                        value={descricao}
                        onChange={e=> setDescricao(e.target.value)}
                    />                                        
                    <button className="button" type="submit">{categoriaId === '0'? 'Incluir' : 'Atualizar'}</button>
                </form>
            </div>
        </div>

    );
}