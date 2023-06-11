import React from "react";
import { Link } from "react-router-dom";
import card from "./img/card1.png";
import afri from "./img/afri.jpg";
import chine from "./img/chine.jpg";
import ital from "./img/ital.jpg";
import FoodBox from 'components/FoodBox';
import PaymentSect from "components/PaymentSect";
import './foodcont.css';


const FoodCont = ({
    children,
}) => {
    return (
        <>
            <div className="foodcontainer">
                <div className="left-side">
                    <div className="cards">
                        <div className="all">
                            <div className="varieties"> {/* TODO requset here */}
                                <Link to="/" className="var-btn">
                                    All
                                </Link>
                                <Link to="/african" className="var-btn">
                                    African
                                </Link>
                                <Link to="/chinese" className="var-btn">
                                    Chinese
                                </Link>
                                <Link to="/italian" className="var-btn">
                                    Italian
                                </Link>
                                <Link to="/desert" className="var-btn">
                                    Desert
                                </Link>
                            </div>
                        </div>

                        <main>
                            <FoodBox imgSrc={card} title={"All 1"} price={"$20"} /> {/* TODO request here */}
                            <FoodBox imgSrc={afri} title={"All 2"} price={"$10"} />
                            <FoodBox imgSrc={ital} title={"All 3"} price={"$5"} />
                            <FoodBox imgSrc={chine} title={"All 4"} price={"$7"} />
                            <FoodBox imgSrc={card} title={"All 5"} price={"$10"} />
                            <FoodBox imgSrc={card} title={"All 6"} price={"$15"} />
                        </main>
                    </div>
                </div>
                <div className="right-side">
                    <PaymentSect />
                </div>
            </div>
        </>
    )
};

export default FoodCont;