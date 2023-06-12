import { getJson } from 'requests';
import {
    REQUEST_PRODUCTS,
    RECEIVE_PRODUCTS,
    ERROR_RECEIVE_PRODUCTS,
} from '../actions/actionTypes';
import config from 'config';

const requestProduct = () => ({
    type: REQUEST_PRODUCTS,
});
const receiveProduct = (products) => ({
    type: RECEIVE_PRODUCTS,
    products,
});
const errorReceiveProduct = () => ({
    type: ERROR_RECEIVE_PRODUCTS,
});

const getProducts = ({ url }) => {
    console.log(`GET ${url}`);
    return getJson({ url })
            .catch(() => {
                return {
                    products: [
                        {
                            id: 1,
                            name: 'Pinecakes',
                            description: 'Created in England during the 1700s, original recipes called for one pound each of flour, sugar, butter, and eggs.',
                            price: 20,
                            category: 'Fast-food',
                            image: '/images/chine.jpg',
                        },
                        {
                            id: 2,
                            name: 'Soup',
                            description: 'A liquid dish, typically savoury and made by boiling meat, fish, or vegetables etc. in stock or water.',
                            price: 15,
                            category: 'Fast-food',
                            image: '/images/ital.jpg',
                        },
                        {
                            id: 3,
                            name: 'Water',
                            description: 'Sparkling water',
                            price: 5,
                            category: 'Drinks',
                            image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS6sm7OZjYT9Tr1_6Euk0fGOuPO86eVKp5ASBTClR-RZgd5HXPeCXDHpoVX5oVlTTnHycw&usqp=CAU'
                        },
                    ]
                }
            });
};

const receiveProducts = () => (dispatch) => {
    dispatch(requestProduct());
    const url = `${config.BASE_URL}${config.PRODUCTS_SERVICE}`;
    return getProducts({ url })
                .then((data) => dispatch(receiveProduct(data.products)))
                .catch(() => dispatch(errorReceiveProduct()));

};

export default {
    receiveProducts,
};