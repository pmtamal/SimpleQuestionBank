import React from "react";
import { Button } from "primereact/button";

export const DialogFooter = ({ onModalCancel, onModalSubmit }) => {
  return (
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
};
