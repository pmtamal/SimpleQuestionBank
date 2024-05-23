import HttpInstance from "../configs/axiosClient";
import { apiRoutes } from "../constants/apiRoutes";

export const getAllTags = () => {
  return HttpInstance.get(apiRoutes.getTags);
};
