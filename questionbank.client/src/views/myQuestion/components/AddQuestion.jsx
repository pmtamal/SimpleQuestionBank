/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState } from "react";

import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";

import { useToast } from "../../../customContext/ToastContext";
import QuestionForm from "../../createEditQuestion/QuestionForm";
import { addQuestion, updateOwnQuestion } from "../../../apiClient/questionApi";
import { getOwnQuestionEdit } from "../../../apiClient/questionApi";
import CommentSection from "../../../components/CommentSection";

import { getOwnComments } from "../../../apiClient/questionFeedBackApi";

const AddQuestion = (props) => {
  const { showToast } = useToast();

  const [question, setQuestion] = useState({
    title: "",
    description: "",
    sampleAnswer: "",
    category: "",
    tags: [],
  });

  const [comments, setComments] = useState([]);

  const { modalVisible, setModalVisible, questionId } = props;

  const loadData = () => {
    if (questionId) {
      const getQuestion = async () => {
        var result = await getOwnQuestionEdit(questionId);
        var questionObj = result.data;

        var resultComment = await getOwnComments(questionId);
        setComments(
          resultComment.data ? [...Array.from(resultComment.data)] : []
        );

        setQuestion({
          id: questionObj.id,
          title: questionObj.title,
          description: questionObj.description,
          sampleAnswer: questionObj.sampleAnswer,
          category: questionObj.questionCategoryId,
          tags: questionObj.skillsTagIds,
        });
      };

      getQuestion();
    } else {
      setQuestion({
        title: "",
        description: "",
        sampleAnswer: "",
        category: "",
        tags: [],
      });
    }
  };

  const modalHeader = <>{questionId ? "Update Question" : "Add Question"}</>;

  const saveHandler = async (isDraft) => {
    let toastMsg = [];

    if (!question.title) {
      toastMsg.push(<li>Question Title field is required</li>);
    }
    if (!question.tags) {
      toastMsg.push(<li>Minium one tag is required</li>);
    }

    if (toastMsg.length) {
      showToast({
        type: "error",
        headerText: "Required Field Missing",
        message: <ul>{toastMsg}</ul>,
      });
      return;
    }

    let questionData = {
      id: question.id,
      title: question.title,
      description: question.description,
      sampleAnswer: question.sampleAnswer,
      questionCategoryId: question.category,
      skillsTagIds: question.tags,
      saveAsDraft: isDraft,
    };

    console.log("Question data to be saved:", questionData);

    let result = questionId
      ? await updateOwnQuestion(questionData)
      : await addQuestion(questionData);
    // based on response from API
    if (result.data.hasError) {
      showToast({
        type: "failed",
        headerText: "Failed",
        message: (
          <ul>Question could not be saved. {`${result.data.errorMessage}`}</ul>
        ),
      });
    } else {
      showToast({
        type: "success",
        headerText: "Success",
        message: <ul>Question saved succssfully.</ul>,
      });
      setModalVisible(false, false);
      setQuestion({
        title: "",
        description: "",
        sampleAnswer: "",
        category: "",
        tags: [],
      });
    }
  };

  const cancelHandler = () => {
    setModalVisible(false, true);
    setQuestion({
      title: "",
      description: "",
      sampleAnswer: "",
      category: "",
      tags: [],
    });
  };

  const modalFooter = (
    <>
      <div className="flex flex-wrap md:justify-content-center lg:justify-content-end">
        <Button
          label="Cancel"
          className="flex justify-content-center border-round m-2"
          outlined
          severity="danger"
          onClick={cancelHandler}
          raised
        />
        <Button
          label="Draft"
          className="flex justify-content-center border-round m-2"
          onClick={() => saveHandler(true)}
          severity="secondary"
          raised
        />
        <Button
          label="Submit"
          className="flex justify-content-center border-round m-2"
          onClick={() => saveHandler(false)}
          raised
        />
      </div>
    </>
  );
  const modalBody = (
    <>
      <QuestionForm
        questionId={questionId}
        questionInfo={question}
        setQuestionInfo={setQuestion}
      />

      <CommentSection
        ownQuestion={true}
        isResolvable={true}
        commentList={comments}
      />
    </>
  );

  return (
    <Dialog
      header={modalHeader}
      blockScroll={true}
      visible={modalVisible}
      position="center"
      style={{ width: "50vw", height: "100%" }}
      footer={modalFooter}
      draggable={false}
      resizable={false}
      maximizable
      onHide={() => setModalVisible(false, true)}
      onShow={() => loadData()}
    >
      {modalBody}
    </Dialog>
  );
};

export default AddQuestion;
