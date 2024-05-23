/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState, useEffect } from "react";
import { Button } from "primereact/button";

import "primereact/resources/themes/saga-blue/theme.css"; // Or any other theme you prefer
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
import "./questionViewForm.css";
import { InputTextarea } from "primereact/inputtextarea";

const QuestionViewForm = (props) => {
  const { questionInfo } = props;
  const [comments, setComments] = useState([]);
  const [newComment, setNewComment] = useState("");
  const [editComment, setEditComment] = useState({ id: null, text: "" });

  // Add a new comment
  const handleAddComment = () => {
    if (newComment.trim()) {
      setComments((prevComments) => [
        ...prevComments,
        { id: Date.now(), text: newComment },
      ]);
      setNewComment("");
    }
  };

  // Edit an existing comment
  const handleEditComment = (id, text) => {
    setEditComment({ id, text });
  };

  // Update a comment
  const handleUpdateComment = () => {
    setComments((prevComments) =>
      prevComments.map((comment) =>
        comment.id === editComment.id
          ? { ...comment, text: editComment.text }
          : comment
      )
    );
    setEditComment({ id: null, text: "" });
  };

  // Remove a comment
  const handleRemoveComment = (id) => {
    setComments((prevComments) =>
      prevComments.filter((comment) => comment.id !== id)
    );
  };

  return (
    <div>
      <div className="card flex my-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="title">Question Title</label>
          <p>{questionInfo.title}</p>
        </div>
      </div>
      <div className="card flex my-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="description">Description</label>
          <div
            style={{
              border: "1px solid #ccc",
              padding: "10px",
              height: "200px",
              overflow: "auto",
            }}
          >
            <div
              dangerouslySetInnerHTML={{ __html: questionInfo.description }}
            />
          </div>
        </div>
      </div>
      <div className="card flex my-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="sampleAnswer">Sample Answer</label>
          <p>{questionInfo.sampleAnswer}</p>
        </div>
      </div>
      <div className="grid">
        <div className="sm:col-12 md:col-12 lg:col-6">
          <div className="card flex my-4">
            <div className="flex flex-column gap-2 sm:w-full md:w-full lg:w-9">
              <label htmlFor="category">Category</label>
              <p>{questionInfo.categoryName}</p>
            </div>
          </div>
        </div>
        <div className="sm:col-12 md:col-12 lg:col-6">
          <div className="card flex my-4">
            <div className="flex flex-column gap-2 sm:w-full md:w-full lg:w-9">
              <label htmlFor="tags">Tags</label>
              <p>{questionInfo.tags}</p>
            </div>
          </div>
        </div>
      </div>
      <div className="card my-4 p-4">
        <div className="flex flex-column gap-2 w-full">
          <h2>Comments</h2>
          {comments.map((comment) => (
            <div key={comment.id} className="comment-item">
              <span>{comment.text}</span>
              <div className="comment-buttons">
                <Button
                  label="Edit"
                  icon="pi pi-pencil"
                  className="p-button-text p-button-sm"
                  onClick={() => handleEditComment(comment.id, comment.text)}
                />
                <Button
                  label="Remove"
                  icon="pi pi-trash"
                  className="p-button-danger p-button-sm"
                  onClick={() => handleRemoveComment(comment.id)}
                />
              </div>
            </div>
          ))}

          {editComment.id ? (
            <div className="edit-comment">
              <InputTextarea
                value={editComment.text}
                onChange={(e) =>
                  setEditComment({ ...editComment, text: e.target.value })
                }
                rows={3}
                className="p-inputtext p-component"
              />
              <div className="edit-comment-buttons">
                <Button
                  label="Update"
                  icon="pi pi-check"
                  onClick={handleUpdateComment}
                  className="p-button-success p-button-sm"
                />
                <Button
                  label="Cancel"
                  icon="pi pi-times"
                  onClick={() => setEditComment({ id: null, text: "" })}
                  className="p-button-secondary p-button-sm"
                />
              </div>
            </div>
          ) : (
            <div className="add-comment">
              <InputTextarea
                value={newComment}
                onChange={(e) => setNewComment(e.target.value)}
                rows={3}
                className="p-inputtext p-component"
              />
              <Button
                label="Add Comment"
                icon="pi pi-plus"
                onClick={handleAddComment}
                className="p-button-primary p-button-sm"
              />
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default QuestionViewForm;
