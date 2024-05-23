import { useState } from "react";

import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";
import { TabView, TabPanel } from "primereact/tabview";
import { InputText } from "primereact/inputtext";
import { InputNumber } from "primereact/inputnumber";
import { Tag } from "primereact/tag";

import ReviewerDataTable from "./ReviewerDataTable";
import { useToast } from "../../../customContext/ToastContext";
import { addCategory } from "../../../apiClient/categoryApi";
import { getCategory, updateCategory } from "../../../apiClient/categoryApi";

const AddCategory = (props) => {
  const { showToast } = useToast();
  const [selectedReviewer, setSelectedReviewer] = useState(null);
  const [selectedApprover, setSelectedApprover] = useState(null);

  const [categoryName, setCategoryName] = useState("");
  const [categoryDescription, setCategoryDescription] = useState("");
  const [requiredReviewer, setRequiredReviewer] = useState(1);

  const { modalVisible, setModalVisible, categoryId = null } = props;

  const loadData = async () => {
    if (categoryId) {
      const getCategoryById = async () => {
        const result = await getCategory(categoryId);

        let data = result.data;

        setCategoryName(data.title);
        setCategoryDescription(data.description);
        setRequiredReviewer(data.minNoOfReviewers);
        setSelectedReviewer(data.reviewerUesrs);
        setSelectedApprover(data.approvalUesrs);
      };

      await getCategoryById();
    } else {
      setCategoryName("");
      setCategoryDescription("");
      setRequiredReviewer(1);
      setSelectedReviewer(null);
      setSelectedApprover(null);
    }
  };

  const onModalSubmit = async (e) => {
    let toastMsg = [];
    if (!selectedReviewer || selectedReviewer.length < requiredReviewer) {
      //toastMsg += <li>Selected reviewers count must be greater than or equal to required reviewers count</li>;
      toastMsg.push(
        <li>
          Selected reviewers count must be greater than or equal to required
          reviewers count
        </li>
      );
    }
    if (!categoryName) {
      toastMsg.push(<li>Category name field is required</li>);
    }
    if (!selectedApprover) {
      toastMsg.push(<li>Minium one approver is required</li>);
    }
    if (toastMsg.length) {
      showToast({
        type: "error",
        headerText: "Error",
        message: <ul>{toastMsg}</ul>,
      });
      return;
    }

    const data = {
      title: categoryName,
      description: categoryDescription,
      minNoOfReviewers: requiredReviewer,
      approverUesrIds: selectedApprover.map((ap) => ap.id),
      reviewerUesrIds: selectedReviewer.map((ap) => ap.id),
      id: categoryId ? categoryId : 0,
    };

    if (!categoryId) await addCategory(data);
    else {
      await updateCategory(data);
    }

    showToast({
      type: "success",
      headerText: "Success",
      message: "category created successfully",
    });
    setModalVisible(false);
  };
  const onModalCancel = (e) => {
    setModalVisible(false);
  };

  const modalHeader = <>{categoryId ? "Update Category" : "Add Category"}</>;
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
        <label>Title</label>
        <InputText
          className="w-full"
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
        />
      </div>
      <div className="field">
        <label>Description</label>
        <InputText
          className="w-full"
          value={categoryDescription}
          onChange={(e) => setCategoryDescription(e.target.value)}
        />
      </div>
      <div className="field">
        <label>Required Reviewer Count</label>
        <InputNumber
          className="w-full"
          value={requiredReviewer}
          onValueChange={(e) => setRequiredReviewer(e.value)}
          mode="decimal"
          showButtons
          min={1}
        />
      </div>

      <TabView>
        <TabPanel
          header={
            <>
              <div className="flex flex-wrap justify-content-center gap-3">
                <div className="flex align-items-center justify-content-center">
                  Reviewer
                </div>
                <div className="flex align-items-center justify-content-center">
                  <Tag value={selectedReviewer ? selectedReviewer.length : 0} />
                </div>
              </div>
            </>
          }
          //leftIcon="pi pi-calendar mr-2"
        >
          <ReviewerDataTable
            selectedReviewer={selectedReviewer}
            setSelectedReviewer={setSelectedReviewer}
            userType={1}
          />
        </TabPanel>
        <TabPanel
          header={
            <>
              <div className="flex flex-wrap justify-content-center gap-3">
                <div className="flex align-items-center justify-content-center">
                  Approver
                </div>
                <div className="flex align-items-center justify-content-center">
                  <Tag value={selectedApprover ? selectedApprover.length : 0} />
                </div>
              </div>
            </>
          }
        >
          <ReviewerDataTable
            selectedReviewer={selectedApprover}
            setSelectedReviewer={setSelectedApprover}
            userType={2}
          />
        </TabPanel>
      </TabView>
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
      onShow={loadData}
      onHide={() => setModalVisible(false)}
    >
      {modalBody}
    </Dialog>
  );
};

export default AddCategory;
