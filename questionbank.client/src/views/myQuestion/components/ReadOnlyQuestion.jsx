/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState } from "react";

import { Button } from "primereact/button";
import { Dialog } from "primereact/dialog";

import { getQuestion } from "../../../apiClient/questionApi";
import { getOwnQuestionView } from "../../../apiClient/questionApi";
import ShowQuestion from "../../../components/ShowQuestion";
import CommentSection from "../../../components/CommentSection";
import { questionViewerType } from "../../../Utility/common";

import {
  getOwnComments,
  updateReviewAction,
} from "../../../apiClient/questionFeedBackApi";

const ReadOnlyQuestion = (props) => {
  const {
    modalVisible,
    setModalVisible,
    questionId,
    ownQuestion,
    viewerType,
    showComments = true,
    resolvable,
    selectedQuestion,
    readOnlyComment = false,
  } = props;

  const [questionInfo, setQuestionInfo] = useState({
    title: "",
    description: "",
    sampleAnswer: "",
    categoryName: "",
    tags: [],
  });

  const [comments, setComments] = useState([]);

  console.log("Inside modal", readOnlyComment);

  const loadData = async () => {
    var result = ownQuestion
      ? await getOwnQuestionView(questionId)
      : await getQuestion(questionId);

    let data = result.data;

    if (
      viewerType == questionViewerType.reviewer ||
      viewerType == questionViewerType.approver ||
      viewerType == questionViewerType.creator
    ) {
      result = await getOwnComments(questionId);
      setComments(result.data ? [...Array.from(result.data)] : []);
      console.log("Comment currenly", comments);
      console.log("comments", result.data);
    }
    setQuestionInfo({ ...data, tags: data.tags.split(",") });
  };

  const modalHeader = (
    <>
      <span>Question</span>
    </>
  );

  const cancelHandler = () => {
    setComments([]);
    setModalVisible(false, true);
  };
  const reviewActionHandler = async (actionType) => {
    var result = await updateReviewAction({
      questionId: questionId,
      feedBackType: actionType,
    });
    if (!result.data.hasError) {
      setComments([]);
      setModalVisible(false);
    }
  };

  const modalFooter = (
    <div>
      <div className="flex flex-wrap md:justify-content-center lg:justify-content-end">
        <Button
          label="Close"
          className="flex justify-content-center border-round m-2"
          outlined
          onClick={cancelHandler}
          raised
        />
        {((viewerType == questionViewerType.reviewer &&
          selectedQuestion.status == 2) ||
          (viewerType == questionViewerType.approver &&
            selectedQuestion.status == 3)) && (
          <>
            <Button
              label="Revision"
              className="flex justify-content-center border-round m-2"
              onClick={() =>
                reviewActionHandler(
                  viewerType == questionViewerType.reviewer ? 1 : 6
                )
              }
              severity="secondary"
              raised
            />
            <Button
              label={
                viewerType == questionViewerType.reviewer ? "Accept" : "Merge"
              }
              className="flex justify-content-center border-round m-2"
              onClick={() =>
                reviewActionHandler(
                  viewerType == questionViewerType.reviewer ? 2 : 4
                )
              }
              raised
            />
          </>
        )}
      </div>
    </div>
  );

  console.log("ReadOnlyComment", readOnlyComment);
  const modalBody = (
    <>
      <ShowQuestion questionInfo={questionInfo} />
      {viewerType != questionViewerType.all && (
        <CommentSection
          commentList={comments}
          questionId={questionId}
          ownQuestion={ownQuestion}
          isResolvable={resolvable}
          readOnlyComment={readOnlyComment}
        />
      )}
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
      onHide={() => setModalVisible(false)}
      onShow={() => loadData()}
    >
      {modalBody}
    </Dialog>
  );
};

export default ReadOnlyQuestion;
