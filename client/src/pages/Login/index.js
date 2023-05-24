import React, {useState} from "react";
import "./style.css"
import "../../global.css"
import { useNavigate  } from 'react-router-dom'
import api from "../../services/api";

import logoImage from '../../../src/assets/logo.svg'
import padlock from '../../../src/assets/padlock.png'

const Login = () => {

    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");

    const history = useNavigate(); // para redirecionar para página correta depois de o usuário fazer
                                   // o login.


    async function login(e) {
        e.preventDefault()

        const data = {
            userName,
            password
        }

        try 
        {
            const response = await api.post('/api/auth/v1/signin', data);
            
            localStorage.setItem('userName', userName)
            localStorage.setItem('accessToken', response.data.accessToken)
            localStorage.setItem('refreshToken', response.data.refreshToken)
            
            history("/books")
            
        } catch(error) 
        {
            alert("Erro login invalid!")
        }
    }

    return (
        <div className="login-cotainer">
            <section className="form">
            <img src={logoImage} alt="Erudio Logo" />
            <form action="">
                <h1>Access your Account</h1>
                <input 
                    type="text"
                    placeholder="Username" 
                    value={userName}
                    onChange={e => setUserName(e.target.value)}
                    required
                /> 
                <input 
                    type="password"
                    placeholder="Password" 
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                    required
                /> 

                <button className="button" type="submit" onClick={login}>Login</button>
            </form>
            </section>
            <img src={padlock} alt="login" />
        </div>
    )
}


export default Login;