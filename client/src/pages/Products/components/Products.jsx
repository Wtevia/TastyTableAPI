import React from "react";
import '../styles/container.css';
import TopSect from 'components/TopSect';
import FoodCont from "components/FoodCont";

const Products = ({
    children,
}) => {
    return (
        <div className="container">
            <TopSect />
            <FoodCont />
        </div>
    );
};

export default Products;