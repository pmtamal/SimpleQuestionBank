import HttpInstance from "../configs/axiosClient";
import { apiRoutes } from "../constants/apiRoutes";

export const getAllReviewerQuestion = (status) => {
  return HttpInstance.get(`${apiRoutes.getAllReviewerQuestion}/${status}`);
};
export const getAllApproverQuestion = (status) => {
  return HttpInstance.get(`${apiRoutes.getAllApproverQuestion}/${status}`);
};

export const getOwnComments = (questionId) => {
  return HttpInstance.get(`${apiRoutes.getOwnReviewComment}/${questionId}`);
};

export const addComment = (data) => {
  return HttpInstance.post(apiRoutes.getOwnReviewComment, data);
};

export const updateReviewCommentComment = (data) => {
  return HttpInstance.put(apiRoutes.getOwnReviewComment, data);
};

export const resolveComment = (id) => {
  return HttpInstance.put(`${apiRoutes.resolveComment}/${id}`);
};

export const deleteComment = (id) => {
  return HttpInstance.delete(`${apiRoutes.getOwnReviewComment}/${id}`);
};

export const updateReviewAction = (data) => {
  return HttpInstance.put(apiRoutes.updateReviewAction, data);
};
