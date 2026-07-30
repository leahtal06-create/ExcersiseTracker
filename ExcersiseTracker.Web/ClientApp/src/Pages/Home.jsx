import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';

const Home = () => {

    return (
        <div className="app-container">
            <div className="d-flex flex-column justify-content-center align-items-center">
                <h1>Welcome to Excersise Tracker</h1>
            </div>
            <div>
                <h2>Login to use the system!!!!!</h2>
            </div>
        </div>
    );
};

export default Home;