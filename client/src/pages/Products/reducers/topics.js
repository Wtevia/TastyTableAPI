import {
    RECEIVE_TOPICS,
    REQUEST_TOPICS,
    ERROR_RECEIVE_TOPICS
} from '../actions/actionTypes';

const initialState = {
    list: [],
    isLoading: false,
    isError: false,
};

export default (state = initialState, action) => {
    switch (action.type) {
        case REQUEST_TOPICS: {
            return {
                ...state,
                isLoading: true,
                isError: false,
            };
        }
        case RECEIVE_TOPICS: {
            return {
                ...state,
                isLoading: false,
                isError: false,
                list: action.topics,
            };
        }
        case ERROR_RECEIVE_TOPICS: {
            return {
                ...state,
                isLoading: false,
                isError: true,
            }
        }

        default: return state;
    }
}