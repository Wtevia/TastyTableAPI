import React, { useEffect } from "react";
import {
    BrowserRouter as Router,
    Routes,
    Route,
    useNavigate
} from 'react-router-dom';
import * as PAGES from 'constants/pages';
import '../styles/App.css';
import SideBar from "components/SideBar/SideBar";


const HomePage = () => {
    const navigate = useNavigate();

    useEffect(() => {
        navigate(`/${PAGES.PRODUCTS}`);
    }, [navigate]);

    return null;
};

const App = () => {

    return (
        <div className="App">
            <Router>
                <SideBar />
                {/* SIdebar */}
                <Routes>
                    <Route path="*" element={<HomePage />} />
                    <Route path={`/${PAGES.PRODUCTS}`} element={<h1>PRODUCTS</h1>} />
                    <Route path={`/${PAGES.LOGIN}`} element={<h1>LOGIN</h1>} />
                    <Route path={`/${PAGES.USER_INFO}`} element={<h1>USER_INFO</h1>} />
                    <Route path={`/${PAGES.CHAT}`} element={<h1>CHAT</h1>} />
                    <Route path={`/${PAGES.TRACK}`} element={<h1>TRACK</h1>} />
                    <Route path={`/${PAGES.FAQ}`} element={<h1>FAQ</h1>} />
                </Routes>
            </Router>
        </div>
    );
};

export default App;