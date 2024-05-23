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
import ViewQuestion from "./ViewQuestion.jsx";
import { getAllMergedQuestion } from "../../../apiClient/questionApi.js";
import { formatDate } from "../../../Utility/common.js";
import DateColumn from "../../../components/DateColumn.jsx";
import ReadOnlyQuestion from "../../myQuestion/components/ReadOnlyQuestion.jsx";
import { questionViewerType } from "../../../Utility/common.js";
const AllQuestionDataTable = (props) => {
  const [globalFilterValue, setGlobalFilterValue] = useState("");
  const [tableData, setTableData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [filters, setFilters] = useState({
    global: { value: null, matchMode: "contains" },
  });

  const [questionModalVisible, setQuestionModalVisible] = useState(false);
  const [questionId, setQuestionId] = useState(null);

  let globalFilterFields = [
    "categoryName",
    "minRequiredReviewer",
    "reviewers",
    "approvers",
  ];

  // event handlers
  const onGlobalFilterChange = (e) => {
    const value = e.target.value;
    let _filters = { ...filters };
    _filters["global"].value = value;

    setFilters(_filters);
    setGlobalFilterValue(value);
  };
  const onViewButtonClick = (e, rowData) => {
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
    </div>
  );

  // table action button
  const actionButton = (rowData) => {
    return (
      <Button
        tooltip="View Question"
        icon="pi pi-eye"
        text
        aria-label="Filter"
        onClick={(e) => onViewButtonClick(e, rowData)}
        tooltipOptions={{ position: "bottom" }}
        raised
      />
    );
  };

  useEffect(() => {
    setLoading(true);

    const getQuestionList = async () => {
      const result = await getAllMergedQuestion();
      //   const mappedData = result.data.map(item => ({
      //     value: item.id,
      //     label: item.name
      //   }));
      setTableData(result.data);
      setLoading(false);
    };

    getQuestionList();
    // need to call service here
  }, []);

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
        scrollable
        scrollHeight="450px"
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
          header=""
          field="id"
          body={(rowData) => actionButton(rowData)}
        />
      </DataTable>
      <ReadOnlyQuestion
        modalVisible={questionModalVisible}
        setModalVisible={setQuestionModalVisible}
        questionId={questionId}
        ownQuestion={false}
        viewerType={questionViewerType.all}
        showComments={false}
      />
    </>
  );
};

export default AllQuestionDataTable;
