import React from "react";
import DefaultLayout from "./pages/admin/Admin";

const Dashboard = React.lazy(() => import("./pages/admin/Dashboard/Dashboard"));

const CategoryListPage = React.lazy(() =>
  import("./pages/admin/categoryManagement/Category.list.page")
);

const UserManagementPage = React.lazy(() =>
  import("./pages/admin/userManagement/user.management.list.page")
);

const Profile = React.lazy(() => import("./pages/admin/userProfile/user.profile"));

const ProductListPage = React.lazy(() =>
  import("./pages/admin/productManagement/product.list.page")
);

const routes = [
  {
    path: "/",
    exact: true,
    name: "Admin",
    component: DefaultLayout
  },
  {
    path: "/",
    exact: true,
    name: "Admin",
    component: DefaultLayout
  },
  { path: "/dashboard", name: "Dashboard", component: Dashboard },
  { path: "/category", name: "Category", component: CategoryListPage },
  { path: "/users/:userId", name: "User Profile", component: Profile },
  {
    path: "/user-management",
    name: "User Management",
    component: UserManagementPage
  },
  {path: "/product", name: "Product", component: ProductListPage}

  //{ path: "/categories", name: "Job Category", component: CategoryListPage }
];


export default routes;
