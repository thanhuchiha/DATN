import { Row, Col } from 'reactstrap';
import 'react-toastify/dist/ReactToastify.css';
import React, { Component } from 'react';
import 'react-toastify/dist/ReactToastify.css';
import "./user.profile.css";
import moment from 'moment';
import { connect } from 'react-redux';
import { getProfile } from "../../../actions/profile.action";

class UserProfile extends Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            email: '',
            mobile: '',
            gender: '',
            dateOfBirth: '',
            data: [],
            date: new Date(),
            id: '',
            avataURL: '',
        };
    }

    renderViewProfile = () => {

        const UserData = this.props.profile.profile

        console.log(UserData)
        return (
            <Row>
                <Col lg="2"/>

                <Col lg="3" className="profile_img">
                    <img src={"https://scontent.fdad3-1.fna.fbcdn.net/v/t1.0-9/p720x720/59673239_1199126610272078_2699590020179689472_o.jpg?_nc_cat=102&_nc_oc=AQlzu5PJTWispLOePey5L2n7Hn3wH7sMiVvGXSVpqOobs0inL1dMXt6246mAEzdcbpw&_nc_ht=scontent.fdad3-1.fna&oh=8c88410e644e34a6f23e9e29f0ad739e&oe=5E5B58EF"} alt="avatar_picture" />
                </Col>

                <Col lg="5" className="group_info ">
                    <Row>
                        <div className="profile-head">
                            <h3>
                                <strong>{UserData.name}</strong>
                            </h3>
                        </div>
                    </Row>
                    <div className="line_header">
                        <hr />
                    </div>

                    <Row>
                        <Col lg="2">
                            <label> <strong>Email</strong></label>
                        </Col>
                        <Col lg="10">
                            <p>: {UserData.email ? UserData.email : ""}</p>
                        </Col>
                    </Row>
                    <div className="line">
                        <hr />
                    </div>

                    <Row>
                        <Col lg="2">
                            <label> <strong>Phone</strong></label>
                        </Col>
                        <Col lg="10">
                            <p>: {UserData.mobile ? UserData.mobile : ""}</p>
                        </Col>
                    </Row>
                    <div className="line">
                        <hr />
                    </div>

                    <Row>
                        <Col lg="2">
                            <label> <strong>Gender</strong></label>
                        </Col>
                        <Col lg="10">
                            <p>: {UserData.gender === 1 ? "Male" : UserData.gender === 2 ? "Female" : "None"}</p>
                        </Col>
                    </Row>
                    <div className="line">
                        <hr />
                    </div>

                    <Row>
                        <Col lg="2">
                            <label> <strong>Birthday</strong></label>
                        </Col>
                        <Col lg="10">
                            <p>: {UserData.dateOfBirth ? moment(UserData.dateOfBirth).format('DD /MM /YYYY') : "none"}</p>
                        </Col>
                    </Row>
                    <div className="line">
                        <hr />
                    </div>

                    <Row>
                        <Col lg="2">
                            <label> <strong>Address</strong></label>
                        </Col>
                        <Col lg="10">
                            <p>: {UserData.address ? UserData.address : "none"}</p>
                        </Col>
                    </Row>
                </Col>

                <Col lg="2"/>
            </Row>
        )
    }

    render() {
        return (
            <div>
                {this.renderViewProfile()}
            </div>
        );
    }
}

export default connect(
    state => ({
        profile: state.profile
    }),
    {
        getProfile
    }
)(UserProfile);
