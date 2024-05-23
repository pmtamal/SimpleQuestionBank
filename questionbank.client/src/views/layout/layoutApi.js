import HttpInstance from "../../configs/axiosClient";
import { apiRoutes } from "../../constants/apiRoutes";

export const uploadImage = (data) => {
  return HttpInstance.post(apiRoutes.uploadImage, data);
};

export const getUserImage = () => {
  return HttpInstance.get(apiRoutes.getImage);
};
