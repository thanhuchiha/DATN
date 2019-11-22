import React, { Component } from "react";
import { connect } from "react-redux";
import { Row, Col, Button, Table, Label, Input, FormGroup } from "reactstrap";
import Form from "react-validation/build/form";
import ModalUpload from "../../../components/modal/modal.upload";
import ModalConfirm from "../../../components/modal/modal.confirm";
import Pagination from "../../../components/pagination/Pagination";
import ModalInfo from "../../../components/modal/modal.info";
import ValidationInput from "../../../components/common/validation.input";
import { toastSuccess, toastError } from "../../../helpers/toast.helper";
import lodash from "lodash";
import { getProductList } from "../../../actions/product.list.action";
import Api from "../../../api/api.product";
import { pagination } from "../../../constant/app.constant";
import { getAllCategoryListNoFilter } from "../../../actions/category.list.action";
import MultipleSelect from "../../../components/common/multiple.select";

class ProductListPage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            isShowDeleteModal: false,
            isShowInfoModal: false,
            item: {},
            itemId: null,
            params: {
                skip: pagination.initialPage,
                take: pagination.defaultTake
            },
            query: "",
            isShowUploadModal: false,
        };
        this.delayedCallback = lodash.debounce(this.search, 1000);
    }

    toggleDeleteModal = () => {
        this.setState(prevState => ({
            isShowDeleteModal: !prevState.isShowDeleteModal
        }));
    };

    toggleModalInfo = (item, title) => {
        this.setState(prevState => ({
            isShowInfoModal: !prevState.isShowInfoModal,
            item: item || {},
            formTitle: title
        }));
    };

    showConfirmDelete = itemId => {
        this.setState(
            {
                itemId: itemId
            },
            () => this.toggleDeleteModal()
        );
    };

    showAddProduct = () => {
        let title = "Create Product";
        let product = {
            name: "",
            description: "",
            price: ""
        };
        this.toggleModalInfo(product, title);
    };

    showUpdateModal = item => {
        let title = "Update Product";
        this.toggleModalInfo(item, title, false);
    };

    onModelChange = el => {
        let inputName = el.target.name;
        let inputValue = el.target.value;
        if (inputName === "imageFile") {
            inputValue = el.target.files[0];
        } else {
            inputValue = el.target.value;
        }
        let item = Object.assign({}, this.state.item);
        item[inputName] = inputValue;
        this.setState({ item });
    };

    search = e => {
        this.setState(
            {
                params: {
                    ...this.state.params,
                    skip: 1
                },
                query: e.target.value
            },
            () => {
                this.getProductList();
            }
        );
    };

    onSearchChange = e => {
        e.persist();
        this.delayedCallback(e);
    };

    handlePageClick = e => {
        this.setState(
            {
                params: {
                    ...this.state.params,
                    skip: e.selected + 1
                }
            },
            () => this.getProductList()
        );
    };

    getProductList = () => {
        let params = Object.assign({}, this.state.params, {
            query: this.state.query
        });
        this.props.getProductList(params);
    };

    addProduct = async () => {
        const { name, description, price, imageFile, categoryIds } = this.state.item;
        if (!this.state.item.imageFile) {
            toastError("Please input image product");
            return;
        }
        const image = await Api.uploadImage("image", imageFile);
        const product = { name, description, price, image, categoryIds };
        if (!name || !description || !price || !categoryIds) {
            toastError("Please input all field");
            return;
        }
        try {
            await Api.addProduct(product);
            this.toggleModalInfo();
            this.getProductList();
            toastSuccess("The product has been created successfully");
        } catch (err) {
            toastError(err);
        }
    };

    updateProduct = async () => {
        let { id, name, description, price, categoryIds, image } = this.state.item;
        if (!id || !name || !description || !price) {
            toastError("Please input all field");
            return;
        }
        console.log("image", image);
        //var categoryIds = this.state.item.categories.map()
        if (categoryIds == null) {
            let listCategoryId = [];
            this.state.item.categories.forEach(item => {
                listCategoryId.push(item.id);
                console.log(listCategoryId);
            });
            categoryIds = listCategoryId;
        }

        if (this.state.item.imageFile) {
            console.log("upload");
            image = await Api.uploadImage("image1", this.state.item.imageFile);
        }
        const product = { id, name, description, price, image, categoryIds };
        try {
            await Api.updateProduct(product);
            this.toggleModalInfo();
            this.getProductList();
            toastSuccess("The product has been updated successfully");
        } catch (err) {
            toastError(err);
        }
    };

    deleteProduct = async () => {
        try {
            await Api.deleteProduct(this.state.itemId);
            this.toggleDeleteModal();
            this.getProductList();
            toastSuccess("The product has been deleted successfully");
        } catch (err) {
            toastError(err);
        }
    };

    saveProduct = () => {
        let { id } = this.state.item;
        if (id) {
            this.updateProduct();
        } else {
            this.addProduct();
        }
    };

    onSubmit(e) {
        e.preventDefault();
        this.form.validateAll();
        this.saveProduct();
    }

    componentDidMount() {
        this.getProductList();
        this.getAllCategoryListNoFilter();
    }

    getAllCategoryListNoFilter = () => {
        let params = Object.assign({}, {
            query: this.state.query
        });
        this.props.getAllCategoryListNoFilter(params);
    }

    showChangeImage = (item) => {
        this.toggleModalUpload(item, "Change Image")
    }

    toggleModalUpload = (item, title) => {
        this.setState(prevState => ({
            isShowUploadModal: !prevState.isShowUploadModal,
            item: item || {},
            formTitle: title
        }));
    }

    removeFile = () => {
        this.setState({
            item: {
                ...this.state.item,
                image: ''
            },
        })
    }

    onChangeCategories = (e) => {
        const item = Object.assign({}, this.state.item);
        item["categoryIds"] = e.map((val, idx) => {
            return val.id;
        });
        this.setState({
            item
        })
    }

    render() {
        const { isShowDeleteModal, isShowInfoModal, isShowUploadModal, item, formTitle } = this.state;
        const { productList } = this.props.productList;
        const { sources, pageIndex, totalPages } = productList;
        const hasResults = productList.sources && productList.sources.length > 0;
        console.log(this.state.item)
        const categoryList = Object.assign([], this.props.categoryList.categoryList);
        return (
            <div className="animated fadeIn">
                <ModalConfirm
                    clickOk={this.deleteProduct}
                    isShowModal={isShowDeleteModal}
                    toggleModal={this.toggleDeleteModal}
                />

                <ModalUpload
                    title={formTitle}
                    isShowModal={isShowUploadModal}
                    item={item}
                    toggleModalUpload={this.toggleModalUpload}
                    getProductList={this.getProductList}
                />

                <ModalInfo
                    size="lg"
                    title={this.state.formTitle}
                    isShowModal={isShowInfoModal}
                    hiddenFooter
                >
                    <div className="modal-wrapper">
                        <div className="form-wrapper">
                            <Form
                                onSubmit={e => this.onSubmit(e)}
                                ref={c => {
                                    this.form = c;
                                }}
                            >
                                <ValidationInput
                                    name="name"
                                    title="Name"
                                    type="text"
                                    required={true}
                                    value={item.name}
                                    onChange={this.onModelChange}
                                />
                                {/* category */}
                                <MultipleSelect
                                    defaultValue={item.categories ? item.categories : []}
                                    name="categoryIds"
                                    title="Category"
                                    labelField="name"
                                    valueField="id"
                                    options={categoryList}
                                    onChange={this.onChangeCategories}
                                />
                                {/* Description */}
                                <FormGroup>
                                    <Label for="exampleEmail"><strong>Description: </strong></Label>
                                    <Input
                                        name="description"
                                        required={true}
                                        type="textarea"
                                        value={item.description || ''}
                                        onChange={this.onModelChange} />
                                </FormGroup>
                                {/* Price */}
                                <FormGroup>
                                    <Label for="exampleMobile"><strong>Price: </strong></Label>
                                    <Input
                                        type="number"
                                        name="price"
                                        defaultValue={item.price}
                                        placeholder="Product Price"
                                        onChange={this.onModelChange} />
                                </FormGroup>

                                {/* image */}
                                <FormGroup>
                                    <Label ><strong>Image: </strong></Label>
                                    {item.image ?
                                        <Row>
                                            <FormGroup>
                                                <img src={item.image} alt="product" style={{ width: "180px", marginLeft: "100px" }} ></img>
                                            </FormGroup>
                                            <Col lg='10'>
                                                <Input
                                                    name="imageFile"
                                                    title="Image Product: "
                                                    placeholder={item.image}
                                                    disabled={true}
                                                    type="text"
                                                />
                                            </Col>

                                            <Col lg='2'>
                                                <i className="fa fa-times-circle fa-2x" onClick={this.removeFile} aria-hidden="true"></i>
                                            </Col>
                                        </Row>

                                        : <Input
                                            name="imageFile"
                                            title="Image Product:"
                                            type="file"
                                            accept="image/x-png,image/gif,image/jpeg"
                                            onChange={this.onModelChange} />
                                    }
                                </FormGroup>

                                <div className="text-center">
                                    <Button className=" btn-primary" type="submit">
                                        Confirm
                  </Button>{" "}
                                    <Button className="btn-danger" onClick={this.toggleModalInfo}>
                                        Cancel
                  </Button>
                                </div>
                            </Form>
                        </div>
                    </div>
                </ModalInfo>

                <div className="animated fadeIn">
                    <Row className="flex-container header-table">
                        <Col xs="5" sm="5" md="5" lg="5">
                            <Button
                                onClick={this.showAddProduct}
                                className="btn btn-success btn-sm" > Create </Button>
                        </Col>

                        <Col xs="5" sm="5" md="5" lg="5">
                            <input
                                onChange={this.onSearchChange}
                                className="form-control form-control-sm custom_search"
                                placeholder="Searching..." />
                        </Col>
                    </Row>
                    <Table responsive bordered className="react-bs-table react-bs-table-bordered data-table">
                        <thead>
                            <tr className="table-header">
                                <th>STT</th>
                                <th>Name</th>
                                <th>Categories</th>
                                <th>Description</th>
                                <th>Price</th>
                                <th>Image</th>
                                <th className="custom_action">Action</th>
                            </tr>
                        </thead>

                        <tbody>

                            {hasResults &&
                                sources.map((itemData, i) => {
                                    return (
                                        <tr className="table-row" key={i}>
                                            <td > {productList.pageIndex !== 0 ? productList.pageIndex * productList.pageSize - productList.pageSize + ++i : ++i}</td>
                                            <td>{itemData.name}</td>
                                            <td>{itemData.categories.map((val, idx) => {
                                                return val.name;
                                            }).join(', ')}</td>
                                            <td>{itemData.description}</td>
                                            <td>{itemData.price}</td>
                                            <td><img onClick={() => this.showChangeImage(itemData)} className="custom_avatar" src={itemData.image} alt="product" /></td>
                                            <td>
                                                <Button
                                                    className="btn btn-primary fa fa-pencil"
                                                    onClick={() => this.showUpdateModal(itemData)} />

                                                <Button
                                                    className="btn btn-danger fa fa-trash"
                                                    onClick={() => this.showConfirmDelete(itemData.id)} />
                                            </td>
                                        </tr>
                                    );

                                })}
                        </tbody>
                    </Table>

                    {hasResults && totalPages > 1 &&
                        <Pagination
                            initialPage={0}
                            totalPages={totalPages}
                            forcePage={pageIndex - 1}
                            pageRangeDisplayed={2}
                            onPageChange={this.handlePageClick}
                        />
                    }

                </div>
            </div>

        );
    }
}

export default connect(
    state => ({
        productList: state.productList,
        categoryList: state.categoryList
    }),
    {
        getProductList,
        getAllCategoryListNoFilter
    }
)(ProductListPage);
