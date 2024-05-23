export const signInPostBody = (username, password) => {
    return {
        username,
        password
    };
};

export const refreshTokenPostBody = (accessToken, refreshToken) => {
    return {
        accessToken,
        refreshToken
    };
};

export const resetPasswordPatchBody = (currentPassword, newPassword) => {
    return {
        currentPassword,
        newPassword
    };
};
  