import { createSlice } from '@reduxjs/toolkit';

const initialToken = {
    accessToken : null,
    refreshToken : null
}

const initialUser = {
    role: '',
    cellNo: '',
    email: '',
    fullName: '',
  };

const initialState = {
    token: initialToken,
    user: initialUser,
    isLoggedIn: false
};

const authSlice=createSlice({
    name: "authSlice",
    initialState,
    reducers:{
        setToken(state, action){
            state.token = action.payload;
        },
        logout(state) {
            state.token = initialToken;
            state.user = initialUser;
            state.isLoggedIn = false;
        },
        successfulLogin(state, action){
            //const { user, token } = action.payload;
            state.user = action.payload;
            state.isLoggedIn = true;
        }
    }
});
  
export const { successfulLogin, setToken, logout } = authSlice.actions;

export default authSlice.reducer;