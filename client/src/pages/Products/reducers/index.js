import { combineReducers } from "redux";
import TopicsReducer from './topics';
import ProductsReducer from './products';

export default combineReducers({
    TopicsReducer,
    ProductsReducer,
});