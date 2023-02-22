import React from "react";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Menu} from './pages/Menu';
import {Categorias} from "./pages/Categoria";
import { NovaCategoria } from "./pages/NovaCategoria";
export default function Rotas(){
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Menu/>}/>
                <Route path="/categoria" element={<Categorias/>}/>
                <Route path="/categoria/novo/:categoriaId" element={<NovaCategoria/>}/>
            </Routes>
        </BrowserRouter>
    )
}