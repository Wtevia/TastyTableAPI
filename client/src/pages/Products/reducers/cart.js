import {
    REQUEST_CART,
    RECEIVE_CART,
    ADD_PRODUCT_CART,
    REMOVE_PRODUCT_CART,
    MAKE_ORDER,
    ERROR_MAKE_ORDER,
} from '../actions/actionTypes';

const initialState = {
    isLoading: false,
    isError: false,
    list: []
};

export default (state = initialState, action) => {
    switch (action.type) {
        case REQUEST_CART: {
            return {
                ...state,
                isLoading: true,
                isError: false,
            };
        }
        case RECEIVE_CART: {
            return {
                ...state,
                isLoading: false,
                isError: false,
                // list: action.products,
            };
        }
        case ADD_PRODUCT_CART: {
            return {
                ...state,
                isLoading: false,
                isError: false,
                list: [...state.list, action.product],
            };
        }
        case REMOVE_PRODUCT_CART: {
            return {
                ...state,
                isLoading: false,
                isError: false,
                list: [...state.list.filter(
                    product => product.product.name !== action.product.product.name
                )]
            };
        }
        case MAKE_ORDER: {
            return {
                ...state,
                isLoading: false,
                isError: false,
                list: [],
            };
        }
        case ERROR_MAKE_ORDER: {
            return {
                ...state,
                isError: true,
            };
        }

        default: return state;
    }
}