import { useState } from "react";

import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";

import { InputText } from "primereact/inputtext";
import { InputNumber } from "primereact/inputnumber";

import { useToast } from "../../../customContext/ToastContext";

const ViewQuestion = (props) => {
  const { showToast } = useToast();
  const [selectedReviewer, setSelectedReviewer] = useState(null);
  const [selectedApprover, setSelectedApprover] = useState(null);

  const [categoryName, setCategoryName] = useState("");
  const [requiredReviewer, setRequiredReviewer] = useState(1);

  const { modalVisible, setModalVisible, questionId = null } = props;

  const onModalCancel = (e) => {
    setModalVisible(false);
  };

  const modalHeader = <>Question View</>;
  const modalFooter = (
    <>
      <Button
        label="Cancel"
        outlined
        severity="danger"
        onClick={onModalCancel}
        raised
      />
    </>
  );
  const modalBody = <>Read only question modal body</>;
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

export default ViewQuestion;
