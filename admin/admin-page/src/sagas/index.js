import { all, fork } from "redux-saga/effects";

import { watchCategoryListSagasAsync } from "./category.list.saga";

export default function* sagas() {
  yield all([
    fork(watchCategoryListSagasAsync)
    
  ]);
}
