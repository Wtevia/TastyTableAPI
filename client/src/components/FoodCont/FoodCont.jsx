import React, { useEffect } from "react";
import { Link, useSearchParams } from "react-router-dom";
import card from "./img/card1.png";
import afri from "./img/afri.jpg";
import chine from "./img/chine.jpg";
import ital from "./img/ital.jpg";
import FoodBox from 'components/FoodBox';
import PaymentSect from "components/PaymentSect";
import './foodcont.css';
import { useDispatch, useSelector } from "react-redux";
import ActionsTopics from 'pages/Products/actions/topics';
import ActionsProducts from 'pages/Products/actions/products';


const FoodCont = ({
    children,
}) => {
    const dispatch = useDispatch();
    const {
        list,
        isLoading,
        isError,
    } = useSelector(({topics}) => topics.TopicsReducer);
    const [searchParams, setSearchParams] = useSearchParams();
    // const topics = useSelector(topics => topics);
    const products = useSelector(({topics}) => topics.ProductsReducer);

    useEffect(() => {
        dispatch(ActionsTopics.receiveTopics());
        dispatch(ActionsProducts.receiveProducts());
        // console.log(products);
        // console.log(ital);
        // console.log(topics.topics.TopicsReducer);
        // console.log({
        //     list,
        //     isLoading,
        //     isError,
        // });
    }, []);

    return (
        <>
            <div className="foodcontainer">
                <div className="left-side">
                    <div className="cards">
                        <div className="all">
                            <div className="varieties">
                                <Link to="/products" className="var-btn">
                                    All
                                </Link>
                                {
                                    isLoading && (
                                        <span>Loading...</span>
                                    )
                                }
                                {
                                    !isLoading &&
                                    list.map(topic => {
                                        return (
                                            <Link to={`/products?topic=${topic}`} className="var-btn">
                                                {topic}
                                            </Link>
                                        )}
                                    )
                                }
                                
                            </div>
                        </div>

                        <main>
                            {
                                products.isLoading && (
                                    <span>Loading...</span>
                                )
                            }
                            {
                                !products.isLoading &&
                                products.list.map(product => {
                                    if (searchParams.get('topic') != null && searchParams.get('topic') != product.category) {}
                                    else {
                                        return (
                                            <FoodBox imgSrc={product.image} title={product.name} price={`$${product.price}`} description={product.description} />
                                        )}
                                    }
                                )
                            }
                            {/* <FoodBox imgSrc={card} title={"All 1"} price={"$20"} /> 
                            <FoodBox imgSrc={afri} title={"All 2"} price={"$10"} />
                            <FoodBox imgSrc={ital} title={"All 3"} price={"$5"} />
                            <FoodBox imgSrc={chine} title={"All 4"} price={"$7"} />
                            <FoodBox imgSrc={card} title={"All 5"} price={"$10"} />
                            <FoodBox imgSrc={card} title={"All 6"} price={"$15"} /> */}
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