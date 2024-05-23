import React from "react";
import { useEffect } from "react";
import { useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { IconField } from "primereact/iconfield";
import { InputText } from "primereact/inputtext";
import { InputIcon } from "primereact/inputicon";
import data from "../data.js";
import AddQuestion from "../../myQuestion/components/AddQuestion.jsx";
import { Dropdown } from "primereact/dropdown";
import {
  getAllApproverQuestion,
  getAllReviewerQuestion,
} from "../../../apiClient/questionFeedBackApi.js";
import DateColumn from "../../../components/DateColumn.jsx";
import { questionStatus } from "../../../constants/questionStatus";
import ReadOnlyQuestion from "../../myQuestion/components/ReadOnlyQuestion.jsx";
import { questionViewerType } from "../../../Utility/common.js";

const ApproveDataDataTable = (props) => {
  const [globalFilterValue, setGlobalFilterValue] = useState("");
  const [tableData, setTableData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [filters, setFilters] = useState({
    global: { value: null, matchMode: "contains" },
  });
  const [refreshKey, setRefreshKey] = useState(0);

  const [questionModalVisible, setQuestionModalVisible] = useState(false);
  const [questionViewModalVisible, setquestionViewModalVisible] =
    useState(false);
  const [questionId, setQuestionId] = useState(null);
  const [selectedQuestion, setSelectedQuestion] = useState({});
  const [selectedOption, setSelectedOption] = useState({
    name: "All",
    status: "0",
  });

  const reviewStatus = [
    { name: "All", status: "0" },
    { name: "Pending", status: "1" },
    { name: "Completed", status: "2" },
  ];

  let globalFilterFields = [
    "categoryName",
    "minRequiredReviewer",
    "reviewers",
    "approvers",
  ];

  const handleClose = (visibleModal, isCancelled = false) => {
    setquestionViewModalVisible(visibleModal);
    if (!isCancelled) setRefreshKey((oldKey) => oldKey + 1); // Change the key to trigger a re-render
  };

  // event handlers
  const onGlobalFilterChange = (e) => {
    const value = e.target.value;
    let _filters = { ...filters };
    _filters["global"].value = value;

    setFilters(_filters);
    setGlobalFilterValue(value);
  };
  const onViewButtonClick = async (e, rowData) => {
    setQuestionId(rowData.id);
    setSelectedQuestion(rowData);
    setquestionViewModalVisible(true);
  };
  const onAddQuestionButtonClicked = (e) => {
    setQuestionId(null);
    setQuestionModalVisible(true);
  };
  const onEditQuestionButtonClicked = (e, rowData) => {
    setQuestionId(rowData.id);
    setQuestionModalVisible(true);
  };

  // table search and button panel
  const tableSearchPanel = (
    <div className="flex justify-content-between">
      <IconField iconPosition="left">
        <InputIcon className="pi pi-search" />
        <InputText
          value={globalFilterValue}
          onChange={onGlobalFilterChange}
          placeholder="Keyword Search"
        />
      </IconField>
      <div className="flex flex-wrap justify-content-center gap-3">
        <Dropdown
          value={selectedOption}
          onChange={(e) => setSelectedOption(e.value)}
          options={reviewStatus}
          optionLabel="name"
          className="w-full md:w-14rem"
        />
      </div>
    </div>
  );

  // table action button
  const actionButton = (rowData) => {
    return (
      <div style={{ display: "flex", alignItems: "center" }}>
        <div style={{ marginRight: "10px" }}>
          <Button
            tooltip="Review Question"
            icon="pi pi-file-edit"
            text
            aria-label="Filter"
            onClick={(e) => onViewButtonClick(e, rowData)}
            tooltipOptions={{ position: "bottom" }}
            raised
          />
        </div>
      </div>
    );
  };

  useEffect(() => {
    setLoading(true);

    const getQuestionList = async () => {
      const result = await getAllApproverQuestion(selectedOption.status);
      //   const mappedData = result.data.map(item => ({
      //     value: item.id,
      //     label: item.name
      //   }));
      setTableData(result.data);
      setLoading(false);
    };

    getQuestionList();
  }, [selectedOption, refreshKey]);

  return (
    <>
      <DataTable
        value={tableData}
        paginator
        showGridlines
        stripedRows
        rows={15}
        loading={loading}
        dataKey="id"
        filters={filters}
        globalFilterFields={globalFilterFields}
        header={tableSearchPanel}
        emptyMessage="No question found."
      >
        <Column header="Title" sortable field="title" />
        <Column header="Category" sortable field="categoryName" />
        <Column header="Tags" sortable field="tags" />
        <Column header="Created By" sortable field="createdBy" />
        <Column
          header="Created On"
          sortable
          field={(rowData) => <DateColumn dateStr={rowData.createdOn} />}
        />
        <Column
          header="FinalizedOn"
          sortable
          field={(rowData) => <DateColumn dateStr={rowData.finalizedOn} />}
        />
        <Column
          header="status"
          sortable
          field={(rowData) =>
            questionStatus.find((qstatus) => qstatus.status == rowData.status)
              .name
          }
        />
        <Column
          header=""
          field="id"
          body={(rowData) => actionButton(rowData)}
        />
      </DataTable>

      <ReadOnlyQuestion
        modalVisible={questionViewModalVisible}
        setModalVisible={handleClose}
        questionId={questionId}
        ownQuestion={selectedQuestion.status != 3}
        viewerType={questionViewerType.approver}
        selectedQuestion={selectedQuestion}
        readOnlyComment={selectedQuestion.status != 3}
      />
    </>
  );
};

export default ApproveDataDataTable;
