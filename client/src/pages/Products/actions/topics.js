import { getJson } from 'requests';
import {
    RECEIVE_TOPICS,
    REQUEST_TOPICS,
    ERROR_RECEIVE_TOPICS
} from './actionTypes';
import config from 'config';

const requestTopic = () => ({
    type: REQUEST_TOPICS,
});
const receiveTopic = (topics) => ({
    type: RECEIVE_TOPICS,
    topics,
})
const errorReceiveTopic = () => ({
    type: ERROR_RECEIVE_TOPICS,
});

const getTopics = ({url}) => {
    console.log(`GET ${url}`);
    return getJson({ url })
            .catch(() => {
                return {
                    topics: ['Drinks', 'Deserts', 'Fast-food', 'Salads'],
                };
            })
}

const receiveTopics = () => (dispatch) => {
    dispatch(requestTopic());
    const url = `${config.BASE_URL}${config.TOPICS_SERVICE}`;
    return getTopics({ url })
                .then((data) => dispatch(receiveTopic(data.topics)))
                .catch(() => dispatch(errorReceiveTopic()));
}

export default {
    receiveTopics,
};