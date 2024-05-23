import { useState } from "react";
import LoginForm from "../../components/LoginPage";
import { signIn, getUserInfo } from "./authApi";
import { signInPostBody } from "../../constants/apiBody";
import { useDispatch } from "react-redux";
import { successfulLogin, setToken, logout } from "./authSlice";
import { useToast } from "../../customContext/ToastContext";

export const Login = () => {
  const { showToast } = useToast();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const dispatch = useDispatch();

  const handleLogin = async () => {
    try {
      const res = await signIn(signInPostBody(username, password));
      const token = res.data;
      if (token.hasError) {
        showToast({
          type: "error",
          headerText: "Error",
          message: <ul>{token.errorMessage}</ul>,
        });
        return;
      }
      dispatch(setToken(token));
      const user = (await getUserInfo()).data;
      dispatch(successfulLogin(user));
    } catch (error) {
      dispatch(logout);
    }
  };

  return (
    <>
      <LoginForm
        setPassword={setPassword}
        setUsername={setUsername}
        handleLogin={handleLogin}
      />
    </>
  );
};
