import React, { Component, Fragment } from 'react';
import { BrowserRouter, Route, Switch } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import Loadable from "react-loadable";
import AuthenticationRoute from "./components/common/authentication.route";
import "react-toastify/dist/ReactToastify.min.css";
import "./App.scss";
import "font-awesome/css/font-awesome.min.css";
const loading = () => (
  <div className="animated fadeIn pt-3 text-center">Loading ...</div>
)

// Containers
const DefaultLayout = Loadable({
  loader: () => import("./pages/admin/Admin"),
  loading
});

//Page
const Login = Loadable({
  loader: () => import("./components/login/login"),
  loading
})

const Register = Loadable({
  loader: () => import("./components/register/register"),
  loading
})



class App extends Component {
  render() {
    return (
      <Fragment>
        <ToastContainer />
        <BrowserRouter>
          <Switch>
            <Route exact path="/login" name="Login Page" component={Login} />
            <Route
              exact
              path="/register"
              name="Register Page"
              component={Register}
            />
            <AuthenticationRoute path="/" name="admin" component={DefaultLayout} />
          </Switch>
        </BrowserRouter>
      </Fragment>
    )
  }
}

export default App;
