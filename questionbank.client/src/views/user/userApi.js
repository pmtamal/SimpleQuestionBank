import HttpInstance from "../../configs/axiosClient";
import { apiRoutes } from "../../constants/apiRoutes";

export const registerUser =(data)=>{
    return HttpInstance.post(apiRoutes.registerUser, data);
}
export const getAllRoles =()=>{
    return HttpInstance.get(apiRoutes.getAllRoles);
}
export const getUsers =()=>{
    return HttpInstance.get(apiRoutes.getUsers);
}
export const getTags =()=>{
    return HttpInstance.get(apiRoutes.getTags);
}
export const getUserByUserId =(userId)=>{
    return HttpInstance.get(apiRoutes.getUserByUserId(userId));
}

export const updateUser = (data) => {
    return HttpInstance.put(apiRoutes.registerUser, data);
  };

export const resetUserPassword = (data) => {
    return HttpInstance.patch(apiRoutes.resetPassword, data);
  };

