import React, { Component, Suspense } from "react";
import { Redirect, Route, Switch } from "react-router-dom";
import { Container } from "reactstrap";
import cookie from "react-cookies";

import {
  AppAside,
  AppBreadcrumb,
  AppFooter,
  AppHeader,
  AppSidebar,
  AppSidebarFooter,
  AppSidebarForm,
  AppSidebarHeader,
  AppSidebarMinimizer,
  AppSidebarNav
} from "@coreui/react";
// sidebar nav config
import navigation from "../../_nav";
// routes config
import routes from "../../routes";

const DefaultAside = React.lazy(() => import("./DefaultLayout/DefaultAside"));
const DefaultFooter = React.lazy(() => import("./DefaultLayout/DefaultFooter"));
const DefaultHeader = React.lazy(() => import("./DefaultLayout/DefaultHeader"));

class DefaultLayout extends Component {
  loading = () => (
    <div className="animated fadeIn pt-1 text-center">Đang tải...</div>
  );

  signOut(e) {
    e.preventDefault();
    cookie.remove("token", { path: "/" });
    this.props.history.push("/login");
  }

  //real-time-notification
  // componentDidMount = async () => {
  //   this.props.getProfile();
  //   await  this.props.connectHubNotificationCalendar();
  // };
  //end-real-time-notification
  
  getEmployeeSidebar = () => {
    //const { profile } = this.props.profile;
    // const permissions = profile.permissions || [];
    // const menus = profile.admin
    //   ? navigation.items
    //   : navigation.items.filter(
    //       item =>
    //         !item.permissions ||
    //         item.permissions.find(el => permissions.indexOf(el) !== -1)
    //     );
    return navigation.items;
  };

  render() {
    const items = this.getEmployeeSidebar();
    return (
      <>
        <div className="app">
          <AppHeader fixed>
            <Suspense fallback={this.loading()}>
              <DefaultHeader onLogout={e => this.signOut(e)} />
            </Suspense>
          </AppHeader>

          <div className="app-body">
            <AppSidebar fixed display="lg">
              <AppSidebarHeader />
              <AppSidebarForm />
              <Suspense>
              <AppSidebarNav navConfig={{ items }} {...this.props} />
              </Suspense>
              <AppSidebarFooter />
              <AppSidebarMinimizer />
            </AppSidebar>

            <main className="main">
              <AppBreadcrumb appRoutes={routes} />
              <Container fluid>
                <Suspense fallback={this.loading()}>
                  <Switch>
                    {routes.map((route, idx) => {
                      return route.component ? (
                        <Route
                          key={idx}
                          path={route.path}
                          exact={route.exact}
                          name={route.name}
                          render={props => <route.component {...props} />}
                        />
                      ) : null;
                    })}
                    <Redirect from="/" to="/dashboard" />
                  </Switch>
                </Suspense>
              </Container>
            </main>

            <AppAside fixed>
              <Suspense fallback={this.loading()}>
                <DefaultAside />
              </Suspense>
            </AppAside>
          </div>

          <AppFooter>
            <Suspense fallback={this.loading()}>
              <DefaultFooter />
            </Suspense>
          </AppFooter>
        </div>
      </>
    );
  }
}

export default DefaultLayout;
