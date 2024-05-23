import HttpInstance from "../../configs/axiosClient";
import { apiRoutes } from "../../constants/apiRoutes";

export const signIn=(data)=>{
    return HttpInstance.post(apiRoutes.signIn, data);
}

export const getUserInfo=()=>{
    return HttpInstance.get(apiRoutes.getUserInfo);
}