import React, {useState, useEffect} from 'react';
import {baseUrl} from './config';

import 'bootstrap/dist/css/bootstrap.min.css';

import axios from 'axios';

import {Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap';

import logoCadastro from '../../assets/etiquetas.png';

function App() { 

  const [data, setData] = useState([]);
  const [updateData, setUpdateData] = useState(true);

  const [modalIncluir, setModalIncluir] = useState(false);
  const [modalEditar, setModalEditar] = useState(false);
  const [modalExcluir, setModalExcluir] = useState(false);
    
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

  const [categoriaSelecionada, setCategoriaSelecionada] = useState({ // a gente cria o estado categoriaSelecionado
    codigoCategoria: '',
    descricao: '',
    dataInclusao: null,
    ativo : null
  })

  const selecionarCategoria=(categoria, opcao)=>{
    setCategoriaSelecionada(categoria);
    (opcao === "Editar") ?
      abrirFecharModalEditar() : abrirFecharModalExcluir();
  }

  const abrirFecharModalIncluir=()=>{
    setModalIncluir(!modalIncluir);
  }

  const abrirFecharModalEditar=()=>{
    setModalEditar(!modalEditar);
  }

  const abrirFecharModalExcluir=()=>{
    setModalExcluir(!modalExcluir);
  }

  const handleChange = cat=>{ //método responsável por guardar os dados dos inputs.
    const {name, value} = cat.target;
    setCategoriaSelecionada({ //aqui ele atualiza o estado com os dados guardados.
      ...categoriaSelecionada,[name]:value
    });
    console.log(categoriaSelecionada);
  } 
  
  const pedidoGet = async()=>{
    await axios.get(baseUrl)
    .then(response => {
      setData(response.data);      
    }).catch(error =>{
      console.log(error);
    })
  }

  const pedidoPost = async()=>{
    delete categoriaSelecionada.codigoCategoria;
    await axios.post(baseUrl, categoriaSelecionada)
    .then(response =>{
      setData(data.concat(response.data));
      setUpdateData(true); 
      abrirFecharModalIncluir();
    }).catch(error => {
      console.log(error);
    })
  }

  const pedidoPut = async() =>{
    categoriaSelecionada.ativo = stringToBoolean(categoriaSelecionada.ativo);
    await axios.put(baseUrl, categoriaSelecionada)
    .then(response => {
      var resposta = response.data;
      var dadosAuxiliar = data;
      dadosAuxiliar.map(categoria => {
        if(categoria.codigoCategoria === categoriaSelecionada.codigoCategoria){
          categoria.descricao = resposta.descricao;
          categoria.dataInclusao = resposta.dataInclusao;
          categoria.ativo = resposta.ativo;
        }
      });  
      setUpdateData(true);
      abrirFecharModalEditar();
    }).catch(error => {
      console.log(error);
    })
  }

  const pedidoDelete = async() =>{
    await axios.delete(baseUrl + "/" +categoriaSelecionada.codigoCategoria) //envia request DELETE via axios
    .then(response =>{
      setData(data.filter(categoria => categoria.codigoCategoria !== response.data)); //filtro nos dados
      setUpdateData(true);
      abrirFecharModalExcluir();                                                    // p/ excluir o registro certo
    }).catch(error => {
      console.log(error);
    })
  }
  
  useEffect(() => {
    if (updateData){
      pedidoGet();
      setUpdateData(false);
    }    
  }, [updateData])

  return (  
    <div className="categoria-container">
      <br/>

      <h3>Cadastro de Categorias</h3>

      <header>
        <img src={logoCadastro} alt="Cadastro"/>
        <button className='btn btn-success' onClick={() => abrirFecharModalIncluir()}>Incluir Nova Categoria</button>
      </header>

      <table className='table table-bordered'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Descricao</th>
            <th>Operação</th>
          </tr>
        </thead>

        <tbody>
          {data.map(categoria =>(
            <tr key={categoria.codigoCategoria}>
              <td>{categoria.codigoCategoria}</td>
              <td>{categoria.descricao}</td>
              <td>
                <button className='btn btn-warning' onClick={() => selecionarCategoria(categoria, "Editar")}>Editar</button> {" "}
                <button className='btn btn-danger' onClick={() => selecionarCategoria(categoria, "Excluir")}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
 
      <Modal isOpen={modalIncluir}> {/* Definimos a janela modal  */}
        <ModalHeader> Incluir Categoria </ModalHeader> {/* definição do header */}
        <ModalBody>
          <div className='form-group'> {/* Criação do formulário com Labels e Inputs  */}
            <label>Descricao: </label>
            <br/>
            <input type="text" className='form-control' name='descricao' onChange={handleChange}/>
            <br/>            
          </div>
        </ModalBody>

        <ModalFooter> {/* Rodapé onde definimos os buttons  */}
          <button className='btn btn-success' onClick={() => pedidoPost() }>Incluir</button>{"    "}
          <button className='btn btn-danger' onClick={() => abrirFecharModalIncluir() }>Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar Categoria</ModalHeader>
        <ModalBody>
          <div className='form-group'>
            <label>IdCategoria: </label><br/>
            <input type="text" className='form-control' name='codigoCategoria' readOnly value={categoriaSelecionada && categoriaSelecionada.codigoCategoria}/> 
            <br/>

            <label>Descricao: </label><br/>
            <input type="text" className='form-control' name='descricao' onChange={handleChange}
            value={categoriaSelecionada && categoriaSelecionada.descricao}/><br/>
{/*format(parseISO(categoriaSelecionada.dataInclusao), "dd/MM/yyyy",{locale: ptBR})*/}
            <label>DataInclusao: </label><br/>
            <input type="text" className='form-control' name='dataInclusao' readOnly onChange={handleChange}
            value={categoriaSelecionada && categoriaSelecionada.dataInclusao}/><br/>
          
            <label>Ativo: </label><br/>
            <input type="text" className='custom-control-input' name='ativo' onChange={handleChange}
            value={categoriaSelecionada && categoriaSelecionada.ativo}/><br/>
          </div>
        </ModalBody>

        <ModalFooter>
          <button className='btn btn-warning' onClick={() => pedidoPut()}>Editar</button>{"  "}          
          <button className='btn btn-danger' onClick={() => abrirFecharModalEditar()}>Cancelar</button>
        </ModalFooter>
      </Modal>
      
      <Modal isOpen={modalExcluir}>
        <ModalHeader> Excluir Categoria </ModalHeader>
        <ModalBody>
          Confirma a exclusão desta Categoria: {categoriaSelecionada && categoriaSelecionada.descricao} ?
        </ModalBody>

        <ModalFooter>
          <button className="btn btn-danger" onClick={() => pedidoDelete()}> Sim </button>
          <button className="btn btn-secondary" onClick={() => abrirFecharModalExcluir()}> Não </button>
        </ModalFooter>
      </Modal>
    </div>
  );
}

export default App;
