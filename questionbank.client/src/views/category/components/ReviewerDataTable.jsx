import React from "react";
import { useEffect } from "react";
import { useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { IconField } from "primereact/iconfield";
import { InputText } from "primereact/inputtext";
import { InputIcon } from "primereact/inputicon";
import data from "../data";
import Layout from "../../layout/Layout";
import AddCategory from "./AddCategory";
import { getAllApprover, getAllReviewer } from "../../../apiClient/categoryApi";

const ReviewerDataTable = (props) => {
  const [globalFilterValue, setGlobalFilterValue] = useState("");
  const [tableData, setTableData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [filters, setFilters] = useState({
    global: { value: null, matchMode: "contains" },
  });

  const { selectedReviewer, setSelectedReviewer, userType } = props;

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
  const onEditButtonClick = (e, rowData) => {
    setCategoryId(rowData.categoryId);
    setCategoryModalVisible(true);
  };
  const onAddCategoryButtonClicked = (e) => {
    setCategoryId(null);
    setCategoryModalVisible(true);
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
        tooltip="Edit Category"
        icon="pi pi-pen-to-square"
        text
        aria-label="Filter"
        onClick={(e) => onEditButtonClick(e, rowData)}
        tooltipOptions={{ position: "bottom" }}
      />
    );
  };

  useEffect(() => {
    setLoading(true);

    const getUsers = async () => {
      const result =
        userType == 1 ? await getAllReviewer() : await getAllApprover();
      //   const mappedData = result.data.map(item => ({
      //     value: item.id,
      //     label: item.name
      //   }));
      setTableData(result.data);
    };
    getUsers();

    setLoading(false);

    // need to call service here
  }, []);

  return (
    <>
      <DataTable
        value={tableData}
        selection={selectedReviewer}
        paginator
        showGridlines
        stripedRows
        rows={15}
        loading={loading}
        dataKey="id"
        filters={filters}
        globalFilterFields={globalFilterFields}
        header={tableSearchPanel}
        emptyMessage="No user found."
        scrollable
        scrollHeight="400px"
        //selectionMode="checkbox"
        onSelectionChange={(e) => {
          setSelectedReviewer(e.value);
        }}
      >
        <Column
          selectionMode="multiple"
          headerStyle={{ width: "3rem" }}
        ></Column>
        <Column header="Name" sortable field="fullName" />

        <Column header="Email" sortable field="email" />
        <Column header="Skills" field="approvers" />
      </DataTable>
    </>
  );
};

export default ReviewerDataTable;
