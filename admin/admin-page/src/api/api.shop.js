import RequestHelper from "../helpers/request.helper"
import { appConfig } from "../config/app.config"
import { uploadFile } from "../helpers/upload.file.helper";
export default class ShopApi {
    //API Shop
    static getShopList(params) {
        return RequestHelper.get(appConfig.apiUrl + "shops", params);
    }

    static getShopById(id) {
        return RequestHelper.get(appConfig.apiUrl + `shops/${id}`);
    }

    static addShop(shop) {
        return RequestHelper.post(appConfig.apiUrl + "shops", shop);
    }

    static updateShop(shop) {
        return RequestHelper.put(appConfig.apiUrl + `shops/${shop.id}`, shop);
    }

    static deleteShop(shopId) {
        return RequestHelper.delete(appConfig.apiUrl + `shops/${shopId}`);
    }

    static uploadImage(folder, file) {
        return uploadFile(folder, file);
    }

}