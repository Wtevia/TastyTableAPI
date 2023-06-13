import { postJson } from 'requests';
import {
    REQUEST_CART,
    RECEIVE_CART,
    ADD_PRODUCT_CART,
    REMOVE_PRODUCT_CART,
    MAKE_ORDER,
    ERROR_MAKE_ORDER,
} from '../actions/actionTypes';
import config from 'config';

const requestCartProducts = () => ({
    type: REQUEST_CART,
});
const receiveCartProducts = (/*products*/) => ({
    type: RECEIVE_CART,
    // products,
});
const addProductToCart = (product) => ({
    type: ADD_PRODUCT_CART,
    product,
});
const removeProductFromCart = (product) => ({
    type: REMOVE_PRODUCT_CART,
    product,
});
const makeProductOrder = () => ({
    type: MAKE_ORDER,
});
const errorMakeProductOrder = () => ({
    type: ERROR_MAKE_ORDER,
});

const receiveCart = () => (dispatch) => {
    dispatch(requestCartProducts());
    return dispatch(receiveCartProducts());
};

const addProductCart = ({ product }) => (dispatch) => {
    dispatch(requestCartProducts());
    // console.log(`add to cart`);
    // console.log(product);
    
    return dispatch(addProductToCart({product}));
};

const removeProductCart = ({ product }) => (dispatch) => {
    dispatch(requestCartProducts());
    // console.log(product);
    return dispatch(removeProductFromCart(product));
};

const makeOrder = () => (dispatch) => {
    dispatch(requestCartProducts());
    const url = `${config.BASE_URL}${config.CART_SERVICE}`;
    postJson({
        url,
        body: dispatch(receiveCartProducts()),
    }).catch(() => dispatch(errorMakeProductOrder()));
    return dispatch(makeProductOrder());
};

export default {
    receiveCart,
    addProductCart,
    removeProductCart,
    makeOrder,
};