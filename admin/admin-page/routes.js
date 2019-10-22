import React from "react";
import DefaultLayout from "./pages/admin/Admin";

const Dashboard = React.lazy(() => import("./pages/admin/Dashboard/Dashboard"));

const routes = [
    {
      path: "/",
      exact: true,
      name: "Admin",
      component: DefaultLayout
    },
    { path: "/dashboard", name: "Dashboard", component: Dashboard }
];

export default routes;