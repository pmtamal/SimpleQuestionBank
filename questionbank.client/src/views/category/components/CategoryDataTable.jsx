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

import { getAllCategory } from "../../../apiClient/categoryApi";

const CategoryDataTable = (props) => {
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
    "title",
    "description",
    "minRequiredReviewer",
    "reviewers",
    "approvers",
  ];

  const handleClose = (visibleModal) => {
    setCategoryModalVisible(visibleModal);
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
      <Button
        label="Add Category"
        onClick={onAddCategoryButtonClicked}
        raised
      />
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

    const getCategoryList = async () => {
      const result = await getAllCategory();
      //   const mappedData = result.data.map(item => ({
      //     value: item.id,
      //     label: item.name
      //   }));
      setTableData(result.data);

      console.log(result);
    };

    getCategoryList();

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
          emptyMessage="No category found."
          scrollable
        >
          <Column header="Category Name" sortable field="title" />
          <Column header="Category Name" sortable field="description" />
          <Column
            header="Min Required Reviewer"
            sortable
            field="minNoOfReviewers"
          />
          <Column header="Reviewers" sortable field="reviewers" />
          <Column header="Approvers" sortable field="approvers" />
          <Column
            header=""
            field="Id"
            body={(rowData) => actionButton(rowData)}
          />
        </DataTable>
      </div>
      <div>
        <AddCategory
          modalVisible={categoryModalVisible}
          setModalVisible={handleClose}
          categoryId={id}
        />
      </div>
    </div>
  );
};

export default CategoryDataTable;
