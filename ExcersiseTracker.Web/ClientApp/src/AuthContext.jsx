import React, { createContext, useContext, useEffect, useState } from 'react';
import getAxios from './AuthAxios'; 

const AuthContext = createContext();

const AuthContextComponent = ({ children }) => {

    const [user, setUser] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const getUser = async () => {
            try {
                const { data } = await getAxios().get('/api/account/getcurrentuser');
                setUser(data);
            }
            catch {
            }
            setIsLoading(false);
        }

        getUser();
    }, []);


    if (isLoading) {
        return <h1>Loading...</h1>
    }

    return <AuthContext.Provider value={{ user, setUser }}>
        {children}
    </AuthContext.Provider>

}

const useAuth = () => useContext(AuthContext);


export { AuthContextComponent, useAuth };