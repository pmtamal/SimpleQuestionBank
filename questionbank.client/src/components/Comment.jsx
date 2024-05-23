/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
import React, { useState } from "react";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import DateColumn from "./DateColumn";

const Comment = ({
  comment,
  deleteClickHandler,
  editOkClickHandler,
  resolveClickHandler,
  isResolvable,
  readOnlyComment = false,
}) => {
  const [editComment, setEditComment] = useState({ ...comment });
  const [editMode, setEditMode] = useState(false);

  console.log("inside comment comp", comment);
  const editClickHandler = (e) => {
    setEditMode(true);
  };

  const onEditOkClick = () => {
    setEditMode(false);
    editOkClickHandler({ ...editComment });
  };

  return (
    <div>
      <div className="flex gap-2 align-items-center pb-2">
        <div>
          <b>{comment.commenter}</b>
        </div>
        <div style={{ color: "#85837f", fontSize: "0.85rem" }}>
          <i>{<DateColumn dateStr={comment.commentDate} />}</i>
        </div>
      </div>
      <div className="pb-2 pl-2">
        {editMode ? (
          <div className="flex">
            <div className="md:col-9 lg:col-9">
              <InputText
                className="w-full"
                value={editComment.comment}
                onChange={(e) =>
                  setEditComment({ ...editComment, comment: e.target.value })
                }
              />
            </div>
            <div className="lg:col-3">
              <Button
                className="mx-2"
                icon="pi pi-check"
                outlined
                raised
                onClick={onEditOkClick}
              />
              <Button
                icon="pi pi-times"
                severity="danger"
                outlined
                raised
                onClick={() => {
                  setEditMode(false);
                }}
              />
            </div>
          </div>
        ) : (
          <span>{editComment.comment}</span>
        )}
      </div>
      <div className="flex gap-3 comment-action-btn">
        {comment.isEditableByUser && !readOnlyComment && (
          <>
            <span onClick={editClickHandler}>Edit</span>
            <span onClick={() => deleteClickHandler(comment.id)}>Delete</span>
          </>
        )}
        {isResolvable &&
          (!comment.isResolved ? (
            <span onClick={() => resolveClickHandler(comment.id)}>Resolve</span>
          ) : (
            <b style={{ color: "green" }}>Resolved</b>
          ))}
      </div>
    </div>
  );
};

export default Comment;
