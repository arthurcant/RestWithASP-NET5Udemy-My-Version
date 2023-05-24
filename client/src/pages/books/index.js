import React, { useState, useEffect} from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { FiPower, FiEdit, FiTrash2 } from 'react-icons/fi'
import './style.css'
import api from "../../services/api";

import logoImage from '../../assets/logo.svg'

export default function Book() {

    const [books, setBooks] = useState([]);
    const [page, setPage] = useState(1);
    const history = useNavigate();

    const userName = localStorage.getItem('userName')
    const accessToken = localStorage.getItem('accessToken')
    
    const authorization = {
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    }

    useEffect(() => {
        fetchMoreBooks()
    }, [accessToken])

    async function fetchMoreBooks() {
        const response = await api.get(`/api/book/v1/asc/4/${page}`, authorization);
        setBooks([...books, ...response.data.list]);
        setPage(page + 1);
    }

    async function deleteBook(id){
        try {
            api.delete(`/api/book/v1/${id}`, authorization);

            setBooks(books.filter(book => book.id !== id));
        } catch (error) {
            alert("Delete failed! Try again!")
        }
    }

    async function logout(id){
        try {
            api.delete(`/api/book/v1/revoke`, authorization);
            
            localStorage.clear();
            history('/')
        } catch (error) {
            alert("logout failed.")
        }
    }

    async function editBook(id){
        try {
            history(`/books/new/${id}`)
        } catch (error) {
            alert("Delete failed! Try again!")
        }
    }


    return (
        <div className="book-container">
            <header>
                <img src={logoImage} alt="Erudio" />
                <span>Welcome, <strong>{userName.toLowerCase()}</strong> </span>
                <NavLink className="button" to={"/books/new/0"}>Add new Book</NavLink>
                <button onClick={logout} type="button">
                    <FiPower size={18} color="#251FC5" />
                </button>
            </header>

            <h1>Registered Books</h1>
            <div id="area-books">
                <ul className="book-info">
                    {books.map(book => (
                        <li key={book.id}>
                            <strong>Title</strong>
                            <p>{book.title}</p>
                            <strong>Author:</strong>
                            <p>{book.author}</p>
                            <strong>Price:</strong>
                            <p>{Intl.NumberFormat('pt-BR', {style: 'currency', currency: 'BRL'}).format(book.price)}</p>
                            <strong>Release Date:</strong>
                            <p>{Intl.DateTimeFormat('pt-BR').format(new Date(book.launchDate))}</p>
                            <button onClick={() => editBook(book.id)} type="button">
                                <FiEdit size={20} color="#251FC5" />
                            </button>
                            <button onClick={() => deleteBook(book.id)} type="button">
                                <FiTrash2 size={20} color="#251FC5" />
                            </button>
                        </li>
                    ))}
                    <button className="button" onClick={fetchMoreBooks} type="button">Load More</button>
                </ul>
           </div>
        </div>
    )
}