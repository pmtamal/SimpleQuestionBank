/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState } from "react";

import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";

import { useToast } from "../../../customContext/ToastContext";
import ShowQuestion from "../../../components/ShowQuestion";
import CommentSection from "../../../components/CommentSection";

const AddReview = (props) => {
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

  const modalHeader = <>Review Question</>;
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
        label="Approve"
        severity="success"
        onClick={onModalSubmit}
        raised
      />
    </>
  );
  const modalBody = (
    <>
      <ShowQuestion questionId />
      <CommentSection questionId />
    </>
  );

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

export default AddReview;
