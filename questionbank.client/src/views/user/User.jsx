import { useState } from "react";
import UserDataTable from "./component/UserDataTable";
import UserRegistration from "./component/UserRegistration";

export const User = () => {
  const [modalVisible, setModalVisible] = useState(null);

  return (
    <>
      <div className="container">
        <h2>User</h2>
        <UserDataTable />
      </div>
    </>
  );
};
