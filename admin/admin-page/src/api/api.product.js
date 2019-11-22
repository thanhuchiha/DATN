import RequestHelper from "../helpers/request.helper"
import { appConfig } from "../config/app.config"
import { uploadFile } from "../helpers/upload.file.helper";
export default class ProductApi {
    //APIProduct
    static getProductList(params) {
        return RequestHelper.get(appConfig.apiUrl + "products", params);
    }

    static getProductById(id) {
        return RequestHelper.get(appConfig.apiUrl + `products/${id}`);
    }

    static addProduct(product) {
        return RequestHelper.post(appConfig.apiUrl + "products", product);
    }

    static updateProduct(product) {
        return RequestHelper.put(appConfig.apiUrl + `products/${product.id}`, product);
    }

    static deleteProduct(productId) {
        return RequestHelper.delete(appConfig.apiUrl + `products/${productId}`);
    }

    static uploadImage(folder, file) {
        return uploadFile(folder, file);
    }

}