import { call, put, takeLatest } from 'redux-saga/effects'
import { GET_PRODUCT_LIST, getProductListSuccess, getProductListFailed } from "../actions/product.list.action"
import Api from "../api/api.product"

function* getProductList(action) {
    try {
        const payload = yield call(Api.getProductList, action.payload.params);
        yield put(getProductListSuccess(payload));
    } catch (err) {
        yield put(getProductListFailed());
    }
}

export function* watchProductListSagaAsync() {
    yield takeLatest(GET_PRODUCT_LIST, getProductList);
}