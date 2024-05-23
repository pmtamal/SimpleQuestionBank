import HttpInstance from "../configs/axiosClient";
import { apiRoutes } from "../constants/apiRoutes";

export const getAllCategory = () => {
  return HttpInstance.get(apiRoutes.getAllCategory);
};

export const getCategory = (id) => {
  return HttpInstance.get(`${apiRoutes.getCategory}/${id}`);
};
export const addCategory = (data) => {
  return HttpInstance.post(apiRoutes.addCategory, data);
};
export const updateCategory = (data) => {
  return HttpInstance.put(apiRoutes.addCategory, data);
};

export const getAllReviewer = () => {
  return HttpInstance.get(apiRoutes.getAllReviewer);
};

export const getAllApprover = () => {
  return HttpInstance.get(apiRoutes.getAllApprover);
};
