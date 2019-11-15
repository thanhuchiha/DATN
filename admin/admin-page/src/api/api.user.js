import RequestHepper from "../helpers/request.helper"
import { appConfig } from "../config/app.config"

export default class UserApi {
    static getAllUser(params) {
        return RequestHepper.get(appConfig.apiUrl + "users/all-users", params);
    }
    static getUserProfile(id) {
        return RequestHepper.get(appConfig.apiUrl + `users/${id}`);
    }
}
