import React, { useState, useEffect } from "react";
import { useDispatch } from 'react-redux';
import {
    BrowserRouter as Router,
    Routes,
    Route,
    useNavigate
} from 'react-router-dom';
import * as PAGES from 'constants/pages';
import '../styles/App.css';
import SideBar from "components/SideBar/SideBar";
// import FAQ from "pages/FAQ";
import FAQProvider from 'pageProviders/FAQ';
// import Login from 'pages/Login';
import LoginProvider from 'pageProviders/Login';
import Products from 'pages/Products';
import {
    fetchUser,
} from '../actions/user';

const HomePage = () => {
    const navigate = useNavigate();

    useEffect(() => {
        navigate(`/${PAGES.PRODUCTS}`);
    }, [navigate]);

    return null;
};

const App = () => {
    const [state, setState] = useState({
        componentDidMount: false,
    });
    const dispatch = useDispatch();
    
    useEffect(() => {
        dispatch(fetchUser());
        setState(prevState => ({
          ...prevState,
          componentDidMount: true,
        }));
    }, []);
    return (
        <div className="App">
            <Router>
                <SideBar />
                {/* SIdebar */}
                {state.componentDidMount && (
                    <Routes>
                        <Route path="*" element={<HomePage />} />
                        <Route path={`/${PAGES.PRODUCTS}`} element={<Products />} />
                        <Route path={`/${PAGES.LOGIN}`} element={<LoginProvider />} />
                        <Route path={`/${PAGES.USER_INFO}`} element={<h1>USER_INFO</h1>} />
                        <Route path={`/${PAGES.CHAT}`} element={<h1>CHAT</h1>} />
                        <Route path={`/${PAGES.TRACK}`} element={<h1>TRACK</h1>} />
                        <Route path={`/${PAGES.FAQ}`} element={<FAQProvider />} />
                    </Routes>
                )}
                
            </Router>
        </div>
    );
};

export default App;