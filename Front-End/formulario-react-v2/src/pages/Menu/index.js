import React, {useState} from "react";
import './index.css';
import {MdProductionQuantityLimits} from 'react-icons/md';
import api from "../../services/api";

export function Menu(){
    return(
        <div>
            <head>
                <title>Página inicial</title>
                <link rel="stylesheet" type="text/css" href="./index.css"/>
            </head>
            <body>
                <h1>Escolha uma tabela de dados:</h1>

                <div class="box">
                <a href="http://localhost:3000/categoria">
                    <h2>Categoria</h2>
                    <p>Formulário com todas as Categorias registradas no banco de dados.</p>
                </a>
                </div>

                <div class="box">
                <a href="formulario2.html">
                    <h2>Produto</h2>
                    <p>Formulário com todos os Produtos registrados no banco de dados.</p>
                </a>
                </div>

                <div class="box">
                <a href="formulario3.html">
                    <h2>Estado</h2>
                    <p>Formulário com todos Estados os registrados no banco de dados.</p>
                </a>
                </div>
                
            </body>
        </div>
    )
}