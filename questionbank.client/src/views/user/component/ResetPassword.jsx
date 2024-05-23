import { useState } from "react";

import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";

import { useToast } from "../../../customContext/ToastContext";
import { Password } from "primereact/password";
import { useEffect } from "react";

import { resetUserPassword } from "../userApi";

const ResetPassword = ({ modalVisible, setModalVisible, userId = null }) => {
  let defaultUserData = {
    currentPassword: "",
    newPassword: "",
    confirmPassword: "",
  };
  const { showToast } = useToast();
  const [userData, setUserData] = useState(defaultUserData);

  const onModalSubmit = async (e) => {
    let toastMsg = [];
    if (userData.currentPassword == "") {
      toastMsg.push(<li>Current password field is required</li>);
    }
    if (userData.newPassword == "") {
      toastMsg.push(<li>New password field is required</li>);
    }
    if (userData.confirmPassword == "") {
      toastMsg.push(<li>Confirm password field is required</li>);
    }
    if (!(userData.newPassword == userData.confirmPassword)) {
      toastMsg.push(<li>New passwod and confirm password must be same</li>);
    }
    if (toastMsg.length) {
      showToast({
        type: "error",
        headerText: "Error",
        message: <ul>{toastMsg}</ul>,
      });
      return;
    }
    try {
      await resetUserPassword({
        currentPassword: userData.currentPassword,
        newPassword: userData.newPassword,
      });
    } catch (error) {
      showToast({
        type: "error",
        headerText: "Error",
        message: "Password reset failed",
      });
      return;
    }
    setModalVisible(false);
  };

  const onModalCancel = (e) => {
    setModalVisible(false);
  };
  const onPasswordInfoChange = (e) => {
    setUserData({ ...userData, [e.target.name]: e.target.value });
  };

  useEffect(() => {
    setUserData({ ...defaultUserData });
  }, [modalVisible]);

  const modalHeader = <>Reset Password</>;
  const modalFooter = (
    <>
      <Button
        label="Cancel"
        outlined
        severity="danger"
        onClick={onModalCancel}
        raised
      />
      <Button label="Submit" onClick={onModalSubmit} raised />
    </>
  );
  const modalBody = (
    <>
      <div className="field">
        <label>Current Password</label>
        <Password
          feedback={false}
          name="currentPassword"
          tabIndex={1}
          toggleMask
          className="w-full"
          value={userData.currentPassword}
          onChange={onPasswordInfoChange}
          pt={{
            input: { className: "w-full" },
          }}
        />
      </div>
      <div className="field">
        <label>New Password</label>
        <Password
          feedback={false}
          name="newPassword"
          tabIndex={1}
          toggleMask
          className="w-full"
          value={userData.newPassword}
          onChange={onPasswordInfoChange}
          pt={{
            input: { className: "w-full" },
          }}
        />
      </div>
      <div className="field">
        <label>Confirm Password</label>
        <Password
          feedback={false}
          name="confirmPassword"
          tabIndex={1}
          toggleMask
          className=" w-full"
          value={userData.confirmPassword}
          onChange={onPasswordInfoChange}
          pt={{
            input: { className: "w-full" },
          }}
        />
      </div>
    </>
  );
  return (
    <Dialog
      header={modalHeader}
      blockScroll={true}
      visible={modalVisible}
      position="center"
      style={{ width: "30vw", height: "30hw" }}
      footer={modalFooter}
      draggable={false}
      resizable={false}
      onHide={() => setModalVisible(false)}
    >
      {modalBody}
    </Dialog>
  );
};

export default ResetPassword;
