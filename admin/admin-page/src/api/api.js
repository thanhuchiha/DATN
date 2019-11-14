import RequestHelper from "../helpers/request.helper";
import { appConfig } from "../config/app.config";
import { uploadFile } from "../helpers/upload.file.helper";

export default class Api {
  // Category
  static getCategoryList(params) {
    return RequestHelper.get(appConfig.apiUrl + "categories", params);
  }

  static getAllCategoryListNoFilter(params) {
    return RequestHelper.get(
      appConfig.apiUrl + "categories/all-categories",
      params
    );
  }

  static addCategory(category) {
    return RequestHelper.post(appConfig.apiUrl + "categories", category);
  }
  static updateCategory(category) {
    return RequestHelper.put(
      appConfig.apiUrl + `categories/${category.id}`,
      category
    );
  }
  static deleteCategory(categoryId) {
    return RequestHelper.delete(appConfig.apiUrl + `categories/${categoryId}`);
  }
  static getRoleList(params) {
    return RequestHelper.get(appConfig.apiUrl + "roles", params);
  }

  //upload file
  static uploadAvatar(folder, file) {
    return uploadFile(folder, file);
  }

  // sso
  static login(data) {
    return RequestHelper.post(appConfig.apiUrl + "sso/login", data);
  }

  static getPermissons() {
    return RequestHelper.get(appConfig.apiUrl + "sso/permissions");
  }

  static getProfile() {
    return RequestHelper.get(appConfig.apiUrl + "sso/profile");
  }
}
