import { Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import userRoleEnum from "./enum/userRoleEnum";

export const RoleBaseRoute = ({ element: Element, roles, ...rest }) => {
  const { isLoggedIn, user } = useSelector((state) => state.auth);

  if (!isLoggedIn || !roles.includes(user.role)) {
    return <Navigate to="/" />;
  }
  return <Element {...rest} />;
};
