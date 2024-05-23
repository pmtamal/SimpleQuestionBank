/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState, useEffect } from "react";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";

import Comment from "./Comment";

import {
  addComment,
  updateReviewCommentComment,
  deleteComment,
  resolveComment,
} from "../apiClient/questionFeedBackApi";

const CommentSection = (props) => {
  const {
    questionId,
    commentList,
    ownQuestion,
    isResolvable = false,
    readOnlyComment = false,
  } = props;

  const [newComment, setNewComment] = useState("");
  const [comments, setComments] = useState([]);

  if (commentList) {
    useEffect(() => {
      setComments([...commentList]);
    }, [commentList]);
  }

  const deleteClickHandler = async (id) => {
    const result = await deleteComment(id);

    if (!result.data.hasError) {
      setComments(
        comments.filter((comm) => {
          return comm.id !== id;
        })
      );
    }
  };

  const resolveClickHandler = async (id) => {
    let result = await resolveComment(id);
    if (!result.data.hasError) {
      setComments((prevComments) =>
        prevComments.map((comnt) =>
          comnt.id === id ? { ...comnt, isResolved: true } : comnt
        )
      );
    }
  };

  const editClickHandler = async (updatedComment) => {
    const result = await updateReviewCommentComment(updatedComment);
    if (!result.data.hasError) {
      setComments((prevComments) =>
        prevComments.map((comnt) =>
          comnt.id === updatedComment.id
            ? { ...comnt, comment: updatedComment.comment }
            : comnt
        )
      );
    }
  };

  const newCommentSavelHandler = async () => {
    if (newComment.length > 0) {
      var result = await addComment({
        comment: newComment,
        questionId: questionId,
      });
      // var newCommnt = {
      //   id: 5,
      //   commenter: "Afranul Haque",
      //   comment: newComment,
      //   commentDate: "20 May, 2024",
      // };

      setComments([...comments, result.data]);
      setNewComment("");
    }
  };

  return (
    <>
      <div>
        <h3>Comments</h3>
      </div>
      {!ownQuestion && (
        <div className="flex">
          <div className="md:col-9 lg:col-9">
            <InputText
              className="w-full"
              value={newComment}
              onChange={(e) => setNewComment(e.target.value)}
              placeholder="Add your comment here"
            />
          </div>
          <div className="lg:col-3">
            <Button
              className="mx-2"
              icon="pi pi-check"
              outlined
              raised
              onClick={newCommentSavelHandler}
            />
            <Button
              icon="pi pi-times"
              severity="danger"
              outlined
              raised
              onClick={() => setNewComment("")}
            />
          </div>
        </div>
      )}

      <div className="flex flex-column gap-4 px-2 pt-2">
        {comments.map((comment) => {
          return (
            <Comment
              key={comment.id}
              comment={comment}
              deleteClickHandler={deleteClickHandler}
              editOkClickHandler={editClickHandler}
              resolveClickHandler={resolveClickHandler}
              ownQuestion={ownQuestion}
              isResolvable={isResolvable}
              readOnlyComment={readOnlyComment}
            />
          );
        })}
      </div>
    </>
  );
};

export default CommentSection;
