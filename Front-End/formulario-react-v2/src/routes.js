import React from "react";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Menu} from './pages/Menu';
import {Categorias} from "./pages/Categoria";

export default function Rotas(){
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Menu/>}/>
                <Route path="/categoria" element={<Categorias/>}/>
            </Routes>
        </BrowserRouter>
    )
}