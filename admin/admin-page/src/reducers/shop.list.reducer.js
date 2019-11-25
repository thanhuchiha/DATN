import { GET_SHOP_LIST, GET_SHOP_LIST_SUCCESS, GET_SHOP_LIST_FAILED } from "../actions/shop.list.action"

const initialState = {
    shopList: [],
    loading: false,
    failed: false
};

export function shopListReducer(state = initialState, action) {
    switch (action.type) {
        case GET_SHOP_LIST:
            return Object.assign({}, state, {
                loading: true,
                failed: false
            });
        case GET_SHOP_LIST_SUCCESS:
            return Object.assign({}, state, {
                shopList: action.payload,
                loading: false,
                failed: false
            });
        case GET_SHOP_LIST_FAILED:
            return Object.assign({}, state, {
                loading: true,
                failed: true
            });
        default:
            return state;
    }
}