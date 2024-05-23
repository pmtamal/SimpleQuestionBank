import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import { store, persistor } from "./redux/store";
import { Provider } from "react-redux";
import { injectStore } from "./configs/axiosClient.js";
import "primeicons/primeicons.css";
import { PrimeReactProvider } from "primereact/api";
import "primeflex/primeflex.css";
import "primereact/resources/primereact.css";
import "primereact/resources/themes/lara-light-blue/theme.css";
import ToastProvider from "./customContext/ToastContext";
import { PersistGate } from "redux-persist/integration/react";
injectStore(store);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <PrimeReactProvider>
          <ToastProvider>
            <App />
          </ToastProvider>
        </PrimeReactProvider>
      </PersistGate>
    </Provider>
  </React.StrictMode>
);
