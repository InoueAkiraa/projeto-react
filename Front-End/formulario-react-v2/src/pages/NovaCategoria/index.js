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
    const {codigoCategoria} = useParams();
    const navegar = useNavigate();
    
    const stringToBoolean = (stringValue) => {
        switch(stringValue?.toLowerCase()?.trim()){
            case "true": 
            case "yes": 
            case "1": 
              return true;
    
            case "false": 
            case "no": 
            case "0": 
            case null: 
            case undefined:
              return false;
    
            default: 
              return JSON.parse(stringValue);
        }
    }

    useEffect(() => {
        if (codigoCategoria === '0')
            return;
        else
            loadCategoria();
    }, codigoCategoria)

    async function loadCategoria(){
        try {
            const response = await api.get(`https://localhost:7290/api/supermercado/Categoria/${codigoCategoria}`);            
            setId(response.data.id); 
            setDescricao(response.data.descricao);
            setDataInclusao(response.data.dataInclusao);
            setAtivo(response.data.ativo);
        }catch(error){
            alert('Erro ao recuperar a categoria ' + error);
            navegar('/categoria');
        }
    }    

    async function saveOrUpdate(event){
        event.preventDefault();

        const data = {   
            codigoCategoria,         
            descricao,
            dataInclusao,
            ativo
        }

        try{
            if(codigoCategoria === '0'){
                await api.post('https://localhost:7290/api/supermercado/Categoria', data); 
                alert("Categoria [" +data.descricao +"] criada com sucesso !!");
            }   
            else{
                data.codigoCategoria = parseInt(codigoCategoria);
                data.ativo = stringToBoolean(ativo);
                await api.put('https://localhost:7290/api/supermercado/Categoria', data);
            }  
            navegar('../categoria');           
        }catch(error){
            alert('Erro ao recuperar a categoria ' + error);
        }        
    }

    return(
        <div className="nova-categoria-container">
            <div className="content">                
                <section className="form">    
                    <FiUserPlus size={105} color="#17202a"></FiUserPlus>      
                    <h1>{codigoCategoria === '0'? 'Incluir Nova Categoria' : `Atualizar Categoria: ${codigoCategoria}`}</h1>
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

                    {codigoCategoria !== '0' && (
                        <input placeholder="Ativo"
                        value={ativo}
                        onChange={e=> setAtivo(e.target.value)}
                        />
                    )}
                                                        
                    <button className="button" type="submit">{codigoCategoria === '0'? 'Incluir' : 'Atualizar'}</button>
                </form>
            </div>
        </div>
    );
}