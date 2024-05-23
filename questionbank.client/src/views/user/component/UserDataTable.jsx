import React from "react";
import { useEffect } from "react";
import { useState } from "react";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { IconField } from "primereact/iconfield";
import { InputText } from "primereact/inputtext";
import { InputIcon } from "primereact/inputicon";

import { getUsers } from "../userApi";
import UserRegistration from "./UserRegistration";

const UserDataTable = () => {
  const [globalFilterValue, setGlobalFilterValue] = useState("");
  const [tableData, setTableData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [filters, setFilters] = useState({
    global: { value: null, matchMode: "contains" },
  });

  const [categoryModalVisible, setCategoryModalVisible] = useState(false);
  const [id, setCategoryId] = useState(null);
  const [refreshKey, setRefreshKey] = useState(0);

  let globalFilterFields = [
    "fullName",
    "description",
    "minRequiredReviewer",
    "reviewers",
    "approvers",
  ];

  const handleClose = () => {
    setCategoryModalVisible(false);
    setRefreshKey((oldKey) => oldKey + 1); // Change the key to trigger a re-render
  };
  // event handlers
  const onGlobalFilterChange = (e) => {
    const value = e.target.value;
    let _filters = { ...filters };
    _filters["global"].value = value;

    setFilters(_filters);
    setGlobalFilterValue(value);
  };
  const onEditButtonClick = (e, rowData) => {
    console.log("rowdata", rowData);
    setCategoryId(rowData.id);
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
      <Button label="Add User" onClick={onAddCategoryButtonClicked} raised />
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
        raised
      />
    );
  };

  useEffect(() => {
    setLoading(true);
    const getUserList = async () => {
      try {
        const result = await getUsers();
        setTableData(result.data);
      } catch (error) {}
    };

    getUserList();

    setLoading(false);

    // need to call service here
  }, [refreshKey]);

  return (
    <div>
      <div key={refreshKey}>
        <DataTable
          value={tableData}
          paginator
          showGridlines
          stripedRows
          rows={15}
          loading={loading}
          dataKey="Id"
          filters={filters}
          globalFilterFields={globalFilterFields}
          header={tableSearchPanel}
          emptyMessage="No user found."
          scrollable
        >
          <Column header="FullName" sortable field="fullName" />
          <Column header="Email" sortable field="email" />
          <Column header="Cell No" sortable field="cellNo" />
          <Column header="Role" sortable field="role" />
          <Column header="Skills" field="skillsTag" />
          <Column
            header=""
            field="Id"
            body={(rowData) => actionButton(rowData)}
          />
        </DataTable>
      </div>
      <div>
        {categoryModalVisible && (
          <UserRegistration
            modalVisible={categoryModalVisible}
            setModalVisible={handleClose}
            userId={id}
          />
        )}
      </div>
    </div>
  );
};

export default UserDataTable;
