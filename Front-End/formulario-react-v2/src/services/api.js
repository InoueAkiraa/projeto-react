import axios from "axios";

const api = axios.create({
    baseURL : "https://localhost:7290/api/supermercado/Categoria"
})

export default api;