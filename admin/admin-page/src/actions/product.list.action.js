export const GET_PRODUCT_LIST = "[PRODUCT_LIST] GET_PRODUCT_LIST";
export const GET_PRODUCT_LIST_SUCCESS = "[PRODUCT_LIST] GET_PRODUCT_LIST_SUCCESS";
export const GET_PRODUCT_LIST_FAILED = "[PRODUCT_LIST] GET_PRODUCT_LIST_FAILED";

export const getProductList = params => {
    return {
        type: GET_PRODUCT_LIST,
        payload: {
            params
        }
    };
};

export const getProductListSuccess = payload => {
    return {
        type: GET_PRODUCT_LIST_SUCCESS,
        payload
    };
};

export const getProductListFailed = () => {
    return {
        type: GET_PRODUCT_LIST_FAILED
    };
};