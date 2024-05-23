import { useState } from "react";

import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";

import { InputText } from "primereact/inputtext";
import { InputNumber } from "primereact/inputnumber";

import { useToast } from "../../../customContext/ToastContext";

const AddApproval = (props) => {
  const { showToast } = useToast();
  const [selectedReviewer, setSelectedReviewer] = useState(null);
  const [selectedApprover, setSelectedApprover] = useState(null);

  const [categoryName, setCategoryName] = useState("");
  const [requiredReviewer, setRequiredReviewer] = useState(1);

  const { modalVisible, setModalVisible, questionId = null } = props;

  const onModalSubmit = (e) => {
    setModalVisible(false);
  };
  const onModalCancel = (e) => {
    setModalVisible(false);
  };

  const modalHeader = <>Approve Question</>;
  const modalFooter = (
    <>
      <Button
        label="Reject"
        outlined
        severity="danger"
        onClick={onModalCancel}
        raised
      />
      <Button
        label="Change Request"
        outlined
        severity="warning"
        onClick={onModalCancel}
        raised
      />
      <Button label="Merge" severity="success" onClick={onModalSubmit} raised />
    </>
  );
  const modalBody = <>Approve modal body</>;
  return (
    <Dialog
      header={modalHeader}
      blockScroll={true}
      visible={modalVisible}
      position="center"
      style={{ width: "50vw", height: "100%" }}
      footer={modalFooter}
      draggable={false}
      resizable={false}
      maximizable
      onHide={() => setModalVisible(false)}
    >
      {modalBody}
    </Dialog>
  );
};

export default AddApproval;
