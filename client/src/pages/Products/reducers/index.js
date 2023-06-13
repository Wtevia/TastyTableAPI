import { combineReducers } from "redux";
import TopicsReducer from './topics';
import ProductsReducer from './products';
import CartReducer from './cart';

export default combineReducers({
    TopicsReducer,
    ProductsReducer,
    CartReducer,
});