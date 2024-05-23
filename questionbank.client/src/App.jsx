import "./app.css";
import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import Dashboard from "./views/dashboard/Dashboard";
import Layout from "./views/layout/Layout";
import { Login } from "./views/auth/Login";
import { User } from "./views/user/User";
import Category from "./views/category/Category";
import AllQuestion from "./views/AllQuestion/AllQuestion";
import MyQuestion from "./views/myQuestion/MyQuestion";
import Review from "./views/review/Review";
import { appRoutes } from "./constants/appRoutes";
import Approve from "./views/approve/Approve";
import { RoleBaseRoute } from "./RoleBaseRoute";

function App() {
  const isLoggedIn = useSelector((state) => state.auth.isLoggedIn);
  return (
    <BrowserRouter>
      <Routes>
        {isLoggedIn ? (
          <Route path="/" element={<Layout />}>
            <Route index element={<AllQuestion />} />
            <Route path={appRoutes.user} element={<User />} />
            <Route
              path={appRoutes.category}
              element={
                <RoleBaseRoute element={Category} roles={["SysAdmin"]} />
              }
            />
            <Route
              path={appRoutes.allQuestion}
              element={
                <RoleBaseRoute
                  element={AllQuestion}
                  roles={[
                    "SysAdmin",
                    "Reviewer",
                    "Approver",
                    "QuestionCreator",
                  ]}
                />
              }
            />
            <Route
              path={appRoutes.myQuestion}
              element={
                <RoleBaseRoute
                  element={MyQuestion}
                  roles={["QuestionCreator"]}
                />
              }
            />
            <Route
              path={appRoutes.review}
              element={<RoleBaseRoute element={Review} roles={["Reviewer"]} />}
            />
            <Route
              path={appRoutes.approve}
              element={<RoleBaseRoute element={Approve} roles={["Approver"]} />}
            />
          </Route>
        ) : (
          <>
            <Route path="/" element={<Login />} />
            <Route path="*" element={<Navigate to="/" />} />
          </>
        )}
      </Routes>
    </BrowserRouter>
  );
}

export default App;
