import React, { Component } from "react";
import { connect } from "react-redux";
import { Row, Col, Button, Table, Label, Input, FormGroup } from "reactstrap";
import Form from "react-validation/build/form";
import ModalConfirm from "../../../components/modal/modal.confirm";
import Pagination from "../../../components/pagination/Pagination";
import ModalUpload from "../../../components/modal/modal.upload.user";
import ModalInfo from "../../../components/modal/modal.info";
import ValidationInput from "../../../components/common/validation.input";
import { toastSuccess, toastError } from "../../../helpers/toast.helper";
import lodash from "lodash";
import { getShopList } from "../../../actions/shop.list.action"
import Api from "../../../api/api.shop";
import { pagination } from "../../../constant/app.constant";
import MultipleSelect from "../../../components/common/multiple.select";
import SelectInput from "../../../components/common/select.input";
import { getUserList } from "../../../actions/user.list.action";

class ShopListPage extends Component {
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
            isShowUploadModal: false
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

    showAddShop = () => {
        let title = "Create Shop";
        let shop = {
            name: "",
            address: "",
            description: "",
            feeShip: "",
            status: 1
        };
        this.toggleModalInfo(shop, title);
    };

    showUpdateModal = item => {
        let title = "Update shop";
        this.toggleModalInfo(item, title);
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
                this.getShopList();
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
            () => this.getShopList()
        );
    };

    getShopList = () => {
        let params = Object.assign({}, this.state.params, {
            query: this.state.query
        });
        this.props.getShopList(params);
    };

    addShop = async () => {
        const { name, address, description, feeShip, status, imageFile, userIds } = this.state.item;

        if (!this.state.item.imageFile) {
            toastError("Please input image shop");
            return;
        }
        const avatarUrl = await Api.uploadImage("avatarUrl", imageFile);

        console.log("item add : ", this.state.item);
        const shop = { name, address, description, feeShip, status, avatarUrl, userIds };
        if (!name || !description || !address || !feeShip || !userIds) {
            toastError("Please input all field");
            return;
        }
        try {
            await Api.addShop(shop);
            this.toggleModalInfo();
            this.getShopList();
            toastSuccess("The shop has been created successfully");
        } catch (err) {
            toastError(err);
        }
    };

    updateShop = async () => {
        let { id, name, address, description, feeShip, status, avatarUrl, userIds } = this.state.item;
        if (!id || !name || !address || !description || !feeShip || !avatarUrl || !status) {
            toastError("Please input all field");
            return;
        }
        console.log("userIds : " , userIds)
        if (userIds == null) {
            let listUserId = [];
            this.state.item.users.forEach(item => {
                listUserId.push(item.id);
                console.log(listUserId);
            });
            userIds = listUserId;
        }

        if (this.state.item.imageFile){
            console.log("upload");
            avatarUrl = await Api.uploadImage("avatarUrl", this.state.item.imageFile);
        }

        const shop = { id, name, address, description, feeShip, status, avatarUrl, userIds };
        console.log("shop here : ", shop)
        try {
            await Api.updateShop(shop);
            this.toggleModalInfo();
            this.getShopList();
            toastSuccess("The shop has been updated successfully");
        } catch (err) {
            toastError(err);
        }
    };

    deleteShop = async () => {
        try {
            await Api.deleteShop(this.state.itemId);
            this.toggleDeleteModal();
            this.getShopList();
            toastSuccess("The shop has been deleted successfully");
        } catch (err) {
            toastError(err);
        }
    };

    saveShop = () => {
        let { id } = this.state.item;
        if (id) {
            this.updateShop();
        } else {
            this.addShop();
        }
    };

    onSubmit(e) {
        e.preventDefault();
        this.form.validateAll();
        this.saveShop();
    }

    componentDidMount() {
        this.getShopList();
        this.getUserList();
    }

    getUserList = () => {
        let params = Object.assign({}, {
            query: this.state.query
        });
        this.props.getUserList(params);
    }

    onChangeUsers = (e) => {
        const item = Object.assign({}, this.state.item);
        item["userIds"] = e.map((val, idx) => {
            return val.id;
        });
        this.setState({
            item
        })
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
                avatarUrl: ''
            },
        })
    }

    render() {
        const { isShowDeleteModal, isShowInfoModal, item, isShowUploadModal, formTitle } = this.state;
        const { shopList } = this.props.shopList;
        const { sources, pageIndex, totalPages } = shopList;
        const hasResults = shopList.sources && shopList.sources.length > 0;
        const userList = Object.assign([], this.props.userList.userList);
        return (
            <div className="animated fadeIn">
                <ModalConfirm
                    clickOk={this.deleteShop}
                    isShowModal={isShowDeleteModal}
                    toggleModal={this.toggleDeleteModal}
                />

                <ModalUpload
                    title={formTitle}
                    isShowModal={isShowUploadModal}
                    item={item}
                    toggleModalUpload={this.toggleModalUpload}
                    getShopList={this.getShopList}
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
                                {/* address */}
                                <FormGroup>
                                    <Label for="exampleEmail"><strong>Address: </strong></Label>
                                    <Input
                                        name="address"
                                        required={true}
                                        type="textarea"
                                        value={item.address || ''}
                                        onChange={this.onModelChange} />
                                </FormGroup>

                                {/* description */}
                                <FormGroup>
                                    <Label for="exampleEmail"><strong>Description: </strong></Label>
                                    <Input
                                        name="description"
                                        required={true}
                                        type="textarea"
                                        value={item.description || ''}
                                        onChange={this.onModelChange} />
                                </FormGroup>
                                {/* fee shipping */}
                                <FormGroup>
                                    <Label for="exampleMobile"><strong>Fee Shipping: </strong></Label>
                                    <Input
                                        type="number"
                                        name="feeShip"
                                        defaultValue={item.feeShip}
                                        placeholder="Fee Shipping"
                                        onChange={this.onModelChange} />
                                </FormGroup>

                                <FormGroup>
                                    <SelectInput
                                        name="status"
                                        title="Status"
                                        nameField="nameField"
                                        valueField="valueField"
                                        value={item.status}
                                        onChange={this.onModelChange}
                                        options={[
                                            { id: 1, name: "Open" },
                                            { id: 2, name: "Close" }
                                        ]}
                                        required={true} />
                                </FormGroup>
                                {/* img shop */}
                                <FormGroup>
                                    <Label ><strong>Image Shop: </strong></Label>
                                    {item.avatarUrl ?
                                        <Row>
                                            <FormGroup>
                                                <img src={item.avatarUrl} alt="shop" style={{ width: "180px", marginLeft: "100px" }} ></img>
                                            </FormGroup>
                                            <Col lg='10'>
                                                <Input
                                                    name="imageFile"
                                                    title="Image Shop: "
                                                    placeholder={item.avatarUrl}
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
                                            title="Image Shop:"
                                            type="file"
                                            accept="image/x-png,image/gif,image/jpeg"
                                            onChange={this.onModelChange} />
                                    }
                                </FormGroup>

                                {/* list admin */}
                                <MultipleSelect
                                    defaultValue={item.users ? item.users : []}
                                    name="userIds"
                                    title="User"
                                    labelField="name"
                                    valueField="id"
                                    options={userList}
                                    onChange={this.onChangeUsers}
                                />
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
                                onClick={this.showAddShop}
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
                                <th>Address</th>
                                <th>Description</th>
                                <th>Fee Shipping</th>
                                <th>Status</th>
                                <th>Image Shop</th>
                                <th>Admin's Management</th>
                                <th className="custom_action">Action</th>
                            </tr>
                        </thead>

                        <tbody>
                            {hasResults &&
                                sources.map((item, i) => {
                                    return (
                                        <tr className="table-row" key={i}>
                                            <td > {shopList.pageIndex !== 0 ? shopList.pageIndex * shopList.pageSize - shopList.pageSize + ++i : ++i}</td>
                                            <td>{item.name}</td>
                                            <td>{item.address}</td>
                                            <td>{item.description}</td>
                                            <td>{item.feeShip}</td>
                                            <td>{item.status}</td>
                                            <td><img onClick={() => this.showChangeImage(item)} className="custom_avatar" src={item.avatarUrl} alt="shop" /></td>
                                            <td>{item.users.map((val, idx) => {
                                                return val.name;
                                            }).join(', ')}</td>
                                            <td>
                                                <Button
                                                    className="btn btn-primary fa fa-pencil"
                                                    onClick={() => this.showUpdateModal(item)} />

                                                <Button
                                                    className="btn btn-danger fa fa-trash"
                                                    onClick={() => this.showConfirmDelete(item.id)} />
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
        shopList: state.shopList,
        userList: state.userList
    }),
    {
        getShopList,
        getUserList
    }
)(ShopListPage);
