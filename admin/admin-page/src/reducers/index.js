import { combineReducers } from "redux";
import { categoryListReducer } from "./category.list.reducer";

export default combineReducers({
  categoryList: categoryListReducer
  
});
