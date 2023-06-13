import React from "react";
import { FaUser, FaSignOutAlt,} from "react-icons/fa";
import { IoChatbubbleEllipsesSharp, IoLocation } from "react-icons/io5";
import { IoIosHelpCircle } from "react-icons/io";
import { MdDashboard } from "react-icons/md";
import { Link } from 'react-router-dom';
import "./sidebar.css";
import logo from './img/prime-logo.png';
import * as PAGES from 'constants/pages';


const Icon = ({ icon }) => (
    <li>
      <p href="">{icon}</p>
    </li>
  );

const SideBar = ({
    children,
}) => {
    return(
        <header>
            <img className="logo" src={logo} alt="logo" />
            
            <ul className="top-menu">
                <Link to={`${PAGES.PRODUCTS}`}><Icon icon={<MdDashboard title="Order"/>} /></Link>
                <Link to={`${PAGES.USER_INFO}`}><Icon icon={<FaUser title="Your Profile"/>} /></Link>
                <Link to={`${PAGES.CHAT}`}><Icon icon={<IoChatbubbleEllipsesSharp title="Chat with sales rep"/>} /></Link>
                <Link to={`${PAGES.TRACK}`}><Icon icon={<IoLocation title="Track your meal"/>} /></Link>
                <Link to={`${PAGES.FAQ}`}><Icon icon={<IoIosHelpCircle title="FAQs"/>} /></Link>
                
            </ul>

            <ul className="bottom-menu">
                <Icon icon={<FaSignOutAlt />} />
            </ul>
        
        </header>
    );
};

export default SideBar;