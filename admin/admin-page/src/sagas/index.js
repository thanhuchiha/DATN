import { all, fork } from "redux-saga/effects";

import { watchCategoryListSagasAsync } from "./category.list.saga";
import { watchUserManagementListSagasAsync } from "./user.management.list.saga"
import { watchRoleListSagasAsync } from "./role.list.saga"
import { watchUserListSagaAsync } from "./user.list.saga"
import { watchProfileSagasAsync } from "./profile.saga"
import { watchProductListSagaAsync } from "./product.list.saga"
export default function* sagas() {
  yield all([
    fork(watchCategoryListSagasAsync),
    fork(watchUserManagementListSagasAsync),
    fork(watchRoleListSagasAsync),
    fork(watchUserListSagaAsync),
    fork(watchProfileSagasAsync),
    fork(watchProductListSagaAsync)
  ]);
}
