import React, { useState } from "react";
import { Button } from "primereact/button";
import "primereact/resources/themes/saga-blue/theme.css"; // Or any other theme you prefer
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";
import "./questionViewForm.css"; // Custom CSS file for additional styling

const QuestionDetails = ({ questionInfo, comments }) => {
  const [commentsState, setCommentsState] = useState(comments || []);

  // Mark a comment as resolved
  const handleResolveComment = (id) => {
    setCommentsState((prevComments) =>
      prevComments.map((comment) =>
        comment.id === id ? { ...comment, resolved: true } : comment
      )
    );
  };

  return (
    <div className="container">
      <div className="card my-4 p-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="title">Question Title</label>
          <p>{questionInfo.title}</p>
        </div>
      </div>

      <div className="card my-4 p-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="description">Description</label>
          <div className="description-box">
            <div
              dangerouslySetInnerHTML={{ __html: questionInfo.description }}
            />
          </div>
        </div>
      </div>

      <div className="card my-4 p-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="sampleAnswer">Sample Answer</label>
          <p>{questionInfo.sampleAnswer}</p>
        </div>
      </div>

      <div className="grid">
        <div className="sm:col-12 md:col-12 lg:col-6">
          <div className="card my-4 p-4">
            <div className="flex flex-column gap-2 w-full">
              <label htmlFor="category">Category</label>
              <p>{questionInfo.categoryName}</p>
            </div>
          </div>
        </div>
        <div className="sm:col-12 md:col-12 lg:col-6">
          <div className="card my-4 p-4">
            <div className="flex flex-column gap-2 w-full">
              <label htmlFor="tags">Tags</label>
              <p>{questionInfo.tags}</p>
            </div>
          </div>
        </div>
      </div>

      {/* Comments Section */}
      <div className="card my-4 p-4">
        <div className="flex flex-column gap-2 w-full">
          <h2>Comments</h2>
          {commentsState.length > 0 ? (
            commentsState.map((comment) => (
              <div
                key={comment.id}
                className={`comment-item ${comment.resolved ? "resolved" : ""}`}
              >
                <span>
                  <strong>{comment.commenter}</strong>: {comment.text}
                </span>
                <div className="comment-buttons">
                  <Button
                    label="Resolve"
                    icon="pi pi-check"
                    className={`p-button-sm ${
                      comment.resolved
                        ? "p-button-success"
                        : "p-button-outlined"
                    }`}
                    onClick={() => handleResolveComment(comment.id)}
                    disabled={comment.resolved}
                  />
                </div>
              </div>
            ))
          ) : (
            <p>No comments available.</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default QuestionDetails;
