import { GET_PRODUCT_LIST, GET_PRODUCT_LIST_SUCCESS, GET_PRODUCT_LIST_FAILED } from "../actions/product.list.action"

const initialState = {
    productList: [],
    loading: false,
    failed: false
};

export function productListReducer(state = initialState, action) {
    switch (action.type) {
        case GET_PRODUCT_LIST:
            return Object.assign({}, state, {
                loading: true,
                failed: false
            });
        case GET_PRODUCT_LIST_SUCCESS:
            return Object.assign({}, state, {
                productList: action.payload,
                loading: false,
                failed: false
            });
        case GET_PRODUCT_LIST_FAILED:
            return Object.assign({}, state, {
                loading: true,
                failed: true
            });
        default:
            return state;
    }
}