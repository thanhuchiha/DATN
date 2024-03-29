import { combineReducers } from "redux";
import { categoryListReducer } from "./category.list.reducer";
import { userManagementListReducer } from "./user.management.list.reducer"
import { roleListReducer } from "./role.list.reducer"
import { userListReducer } from "./user.list.reducer"
import { profileReducer } from "./profile.reducer"
import { productListReducer } from "./product.list.reducer"
import { shopListReducer } from "./shop.list.reducer"

export default combineReducers({
  categoryList: categoryListReducer,
  userManagementList: userManagementListReducer,
  roleList: roleListReducer,
  userList: userListReducer,
  profile: profileReducer,
  productList: productListReducer,
  shopList: shopListReducer
});


