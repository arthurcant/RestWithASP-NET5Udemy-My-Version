import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";    

import Login from "./pages/login";
import Books from "./pages/books";
import NewBook from "./pages/NewBook";

export default function Rotas() {
    return (
        <BrowserRouter>
            <Routes>
                <Route element={<Login />} path="/" exact />
                <Route element={<Books />} path="/books" exact/>
                <Route element={<NewBook />} path="/books/new/:bookId" />
            </Routes>
        </BrowserRouter>
    )
}
