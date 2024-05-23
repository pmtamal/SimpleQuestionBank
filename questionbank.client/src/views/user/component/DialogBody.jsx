import React from "react";
import { InputText } from "primereact/inputtext";
import { Dropdown } from "primereact/dropdown";
import { MultiSelect } from "primereact/multiselect";

export const DialogBody = ({
  handleChange,
  formData,
  roleOptions,
  tagOptions,
}) => {
  return (
    <>
      <div className="field">
        <label>Email</label>
        <InputText
          id="email"
          className="w-full"
          value={formData.email}
          onChange={(e) => handleChange(e, "email")}
        />
      </div>
      <div className="field">
        <label htmlFor="password">Password</label>
        <InputText
          id="password"
          className="w-full"
          type="password"
          value={formData.password}
          onChange={(e) => handleChange(e, "password")}
        />
      </div>
      <div className="field">
        <label htmlFor="cellNo">Cell No</label>
        <InputText
          id="cellNo"
          className="w-full"
          value={formData.cellNo}
          onChange={(e) => handleChange(e, "cellNo")}
        />
      </div>
      <div className="field">
        <label htmlFor="roleId">Role</label>
        <Dropdown
          id="roleId"
          className="w-full"
          value={formData.roleId}
          options={roleOptions}
          onChange={(e) => handleChange(e, "roleId")}
          placeholder="Select a role"
        />
      </div>

      <div className="field">
        <label htmlFor="roleId">Tags</label>
        <MultiSelect
          id="tags"
          value={formData.tagIds}
          className="w-full"
          options={tagOptions}
          onChange={(e) => handleChange(e, "tagIds")}
          placeholder="Select tags"
          display="chip"
        />
      </div>

      <div className="field">
        <label htmlFor="firstName">First Name</label>
        <InputText
          id="firstName"
          className="w-full"
          value={formData.person.firstName}
          onChange={(e) => handleChange(e, "person", "firstName")}
        />
      </div>
      <div className="field">
        <label htmlFor="middleName">Middle Name</label>
        <InputText
          id="middleName"
          className="w-full"
          value={formData.person.middleName}
          onChange={(e) => handleChange(e, "person", "middleName")}
        />
      </div>
      <div className="field">
        <label htmlFor="lastName">Last Name</label>
        <InputText
          id="lastName"
          className="w-full"
          value={formData.person.lastName}
          onChange={(e) => handleChange(e, "person", "lastName")}
        />
      </div>
      <div className="field">
        <label htmlFor="address">Address</label>
        <InputText
          id="address"
          className="w-full"
          value={formData.person.address}
          onChange={(e) => handleChange(e, "person", "address")}
        />
      </div>
      <div className="field">
        <label htmlFor="dob">Date of Birth</label>
        <InputText
          id="dob"
          type="date"
          className="w-full"
          value={formData.person.dob}
          onChange={(e) => handleChange(e, "person", "dob")}
        />
      </div>
    </>
  );
};
