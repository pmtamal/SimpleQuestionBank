import axios from "axios";
import { apiRoutes } from "../constants/apiRoutes";
import { logout, setToken } from "../views/auth/authSlice";
import { refreshTokenPostBody } from "../constants/apiBody";

const HttpInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

let store;

export const injectStore = (_store) => {
  store = _store;
};

HttpInstance.interceptors.request.use(
  (config) => {
    const token = store.getState().auth.token?.accessToken;
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

HttpInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    if (
      error.response &&
      error.response.status === 401 &&
      !error.config.isRefreshTokenRequest
    ) {
      try {
        const retryConfig = { ...error.config, isRefreshTokenRequest: true };
        const token = store.getState().auth.token;
        let res = await HttpInstance.post(
          apiRoutes.refreshToken,
          refreshTokenPostBody(token.accessToken, token.refreshToken),
          retryConfig
        );
        store.dispatch(setToken(res.data));
        return HttpInstance(retryConfig);
      } catch (error) {
        store.dispatch(logout);
      }
    }
    if (error.response && error.response.status === 401) {
      store.dispatch(logout);
    }
    return Promise.reject(error);
  }
);

export default HttpInstance;
