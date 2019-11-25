export const GET_SHOP_LIST = "[SHOP_LIST] GET_SHOP_LIST";
export const GET_SHOP_LIST_SUCCESS = "[SHOP_LIST] GET_SHOP_LIST_SUCCESS";
export const GET_SHOP_LIST_FAILED = "[SHOP_LIST] GET_SHOP_LIST_FAILED";

export const getShopList = params => {
    return {
        type: GET_SHOP_LIST,
        payload: {
            params
        }
    };
};

export const getShopListSuccess = payload => {
    return {
        type: GET_SHOP_LIST_SUCCESS,
        payload
    };
};

export const getShopListFailed = () => {
    return {
        type: GET_SHOP_LIST_FAILED
    };
};