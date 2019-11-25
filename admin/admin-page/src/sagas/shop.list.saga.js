import { call, put, takeLatest } from 'redux-saga/effects'
import { GET_SHOP_LIST, getShopListSuccess, getShopListFailed } from "../actions/shop.list.action"
import Api from "../api/api.shop"

function* getShopList(action) {
    try {
        const payload = yield call(Api.getShopList, action.payload.params);
        yield put(getShopListSuccess(payload));
    } catch (err) {
        yield put(getShopListFailed());
    }
}

export function* watchShopListSagaAsync() {
    yield takeLatest(GET_SHOP_LIST, getShopList);
}