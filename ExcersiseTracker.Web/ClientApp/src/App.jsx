import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './Pages/Home';
import Signup from './Pages/Signup';   
import Login from './Pages/Login';     

const App = () => {
    return (
        <Layout>
            <Routes>
                <Route path='/' element={<Home />} />
                <Route path='/signup' element={<Signup />} />   
                <Route path='/login' element={<Login />} />     

            </Routes>
        </Layout>
    );
}

export default App;