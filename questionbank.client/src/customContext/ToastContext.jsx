import { createContext, useContext, useRef, useState } from "react";
import { Toast } from "primereact/toast";

// Create a context for the toast
const ToastContext = createContext();

// Create a provider component for the toast
const ToastProvider = ({ children }) => {
  const [toastPosition, setToastPosition] = useState("bottom-right");
  const toastRef = useRef(null);
  const showToast = ({
    type = "success",
    position = "bottom-right",
    headerText = "Success",
    message = "",
    duration = 4 * 1000,
    sticky = false,
  }) => {
    setToastPosition(position);
    toastRef.current.show({
      severity: type,
      summary: headerText,
      detail: message,
      life: duration,
      sticky: sticky,
    });
  };

  return (
    <ToastContext.Provider value={{ showToast }}>
      {children}

      <Toast
        // pt={{
        //   message: { style: { opacity: "85%", border: "0px" } },
        //   text: {
        //     style: {
        //       fontFamily: "Inter-Bold, Helvetica",
        //       fontSize: "14px",
        //       color: "white",
        //     },
        //   },
        //   icon: {
        //     style: {
        //       color: "white",
        //     },
        //   },
        //   closeButton: {
        //     style: {
        //       color: "white",
        //     },
        //   },
        //   detail: {
        //     style: {
        //       fontFamily: "Inter-Regular, Helvetica",
        //       fontWeight: "400",
        //       fontSize: "14px",
        //     },
        //   },
        // }}
        ref={toastRef}
        position={toastPosition}
      />
    </ToastContext.Provider>
  );
};

// Custom hook to use the toast context
export const useToast = () => useContext(ToastContext);
export default ToastProvider;
