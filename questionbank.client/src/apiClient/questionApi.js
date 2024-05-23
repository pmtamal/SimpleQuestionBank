import HttpInstance from "../configs/axiosClient";
import { apiRoutes } from "../constants/apiRoutes";

export const getAllMergedQuestion = () => {
  return HttpInstance.get(apiRoutes.getAllMergeQuestion);
};

export const getOwnQuestionsByStatus = (status) => {
  return HttpInstance.get(`${apiRoutes.getOwnQuestionByStatus}/${status}`);
};

export const getQuestion = (id) => {
  return HttpInstance.get(`${apiRoutes.getQuestion}/${id}`);
};

export const getOwnQuestionView = (id) => {
  return HttpInstance.get(`${apiRoutes.getOwnQuestionView}/${id}`);
};
export const getOwnQuestionEdit = (id) => {
  return HttpInstance.get(`${apiRoutes.getOwnQuestionEdit}/${id}`);
};

export const addQuestion = (data) => {
  return HttpInstance.post(apiRoutes.addQuestion, data);
};
export const updateOwnQuestion = (data) => {
  return HttpInstance.post(apiRoutes.updateOwnQuestion, data);
};
