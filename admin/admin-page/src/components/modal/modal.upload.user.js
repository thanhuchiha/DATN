import React, { Component } from 'react';
import { Button, Modal, ModalHeader, ModalFooter } from 'reactstrap';
import { Input } from "reactstrap";
import Api from "../../api/api.shop";
import { toastSuccess, toastError } from "../../helpers/toast.helper";

class ModalUpload extends Component {
  constructor(props) {
    super(props);
    this.state = {
      imageFile: {}
    };
  }

  onHandleChange = (e) => {
    this.setState({
      imageFile: e.target.files[0]
    });
  }

  doneUpdate = () => {
    this.setState({
      imageFile: {}
    })
  }

  clickOk = async () => {
    console.log("here : ", this.props.item)
    if (!this.state.imageFile.name) {
      toastError("Please choose an image to update image shop!!!");
      return;
    }
    let { userIds } = this.props.item;
    if (userIds == null) {
      let listUserId = [];
      this.props.item.users.forEach(item => {
        listUserId.push(item.id);
        console.log(listUserId);
      });
      userIds = listUserId;
    }
    const avatarUrl = await Api.uploadImage("avatarUrl", this.state.imageFile);
    let item = { ...this.props.item, avatarUrl: avatarUrl, userIds: userIds };
    try {
      await Api.updateShop(item);
      this.props.toggleModalUpload();
      this.props.getShopList();
      this.doneUpdate()
      toastSuccess("The Image Shop has been updated successfully");
    } catch (err) {
      toastError(err);
    }
  }

  render() {
    return (
      <div>
        <Modal isOpen={this.props.isShowModal} toggle={this.props.toggleModalUpload}>
          <ModalHeader>
            {this.props.title || "Change Image"}
          </ModalHeader>

          <Input type="file" name="imageFile" onChange={this.onHandleChange} accept="image/x-png,image/gif,image/jpeg" />

          <ModalFooter>
            <Button color="danger" onClick={this.clickOk}>
              Xác nhận
            </Button>{" "}
            <Button color="secondary" onClick={this.props.toggleModalUpload}>
              Huỷ bỏ
            </Button>
          </ModalFooter>
        </Modal>
      </div>
    );
  }

};

export default ModalUpload;