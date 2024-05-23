/* eslint-disable no-unused-vars */
import { useState, useEffect } from "react";

import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { IconField } from "primereact/iconfield";
import { InputText } from "primereact/inputtext";
import { InputIcon } from "primereact/inputicon";

import AddQuestion from "./AddQuestion.jsx";
import ReadOnlyQuestion from "./ReadOnlyQuestion.jsx";
import { Dropdown } from "primereact/dropdown";
import { getOwnQuestionsByStatus } from "../../../apiClient/questionApi.js";
import DateColumn from "../../../components/DateColumn.jsx";
import { questionStatus } from "../../../constants/questionStatus";
import { questionViewerType } from "../../../Utility/common.js";

const MyQuestionDataTable = (props) => {
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
  const [selectedOption, setSelectedOption] = useState({
    name: "All",
    status: "0",
  });

  let globalFilterFields = [
    "categoryName",
    "minRequiredReviewer",
    "reviewers",
    "approvers",
  ];

  const handleClose = (visibleModal, isCancelled = false) => {
    setQuestionModalVisible(visibleModal);
    console.log("IsCancelled", isCancelled);
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
          options={questionStatus}
          optionLabel="name"
          className="w-full md:w-14rem"
        />
        <Button
          label="Add Question"
          onClick={onAddQuestionButtonClicked}
          raised
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
            tooltip="View Question"
            icon="pi pi-eye"
            text
            aria-label="Filter"
            onClick={(e) => onViewButtonClick(e, rowData)}
            tooltipOptions={{ position: "bottom" }}
            raised
          />
        </div>
        {(rowData.status == 1 || rowData.status == 4) && (
          <div>
            <Button
              tooltip="Edit Question"
              icon="pi pi-pencil"
              text
              aria-label="Filter"
              onClick={(e) => onEditQuestionButtonClicked(e, rowData)}
              tooltipOptions={{ position: "bottom" }}
              raised
            />
          </div>
        )}
      </div>
    );
  };

  useEffect(() => {
    setLoading(true);

    const getQuestionList = async () => {
      const result = await getOwnQuestionsByStatus(selectedOption.status);
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

      <AddQuestion
        modalVisible={questionModalVisible}
        setModalVisible={handleClose}
        questionId={questionId}
      />

      <ReadOnlyQuestion
        modalVisible={questionViewModalVisible}
        setModalVisible={setquestionViewModalVisible}
        questionId={questionId}
        ownQuestion={true}
        resolvable={true}
        viewerType={questionViewerType.creator}
      />
    </>
  );
};

export default MyQuestionDataTable;
