import React from "react";
import TopMenuBar from "../../components/TopMenuBar";
import { Badge } from "primereact/badge";
import { Avatar } from "primereact/avatar";
import { Outlet, Navigate, useNavigate } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import { logout } from "../auth/authSlice";
import { appRoutes } from "../../constants/appRoutes";
import { useRef } from "react";
import { Menu } from "primereact/menu";
import ResetPassword from "../user/component/ResetPassword";
import { useState, useEffect } from "react";
import { classNames } from "primereact/utils";
import ImageUploadModal from "../../components/ImageUploadModal ";
import { getUserImage, uploadImage } from "./layoutApi";
import userRoleEnum from "../../enum/userRoleEnum";

const Layout = () => {
  const [resetPasswordModalVisible, setResetPasswordModalVisible] =
    useState(false);
  const dispatch = useDispatch();
  const profileMenuRef = useRef();
  const { isLoggedIn, user } = useSelector((state) => state.auth);
  const navigate = useNavigate();
  const [modalVisible, setModalVisible] = useState(false);
  const [preview, setPreview] = useState();

  const handleAvatarClick = () => {
    setModalVisible(true);
  };
  //https://www.pikpng.com/pngl/b/183-1837422_icon-person-transparent-background-clipart.png
  useEffect(() => {
    const getImage = async () => {
      let response = await getUserImage();
      if (response.data) setPreview(response.data);
      else
        setPreview(
          "https://www.pikpng.com/pngl/b/183-1837422_icon-person-transparent-background-clipart.png"
        );
    };
    getImage();
  }, []);

  const handleUpload = (file) => {
    const formData = new FormData();
    formData.append("image", file);

    console.log("File to upload:", formData);
    uploadImage(formData);
  };

  if (!isLoggedIn) {
    navigate("/");
  }

  const onProfileClicked = (e) => {
    profileMenuRef.current.toggle(e);
  };

  const notificationCount = 99;
  const startElement = <h3>Dsi Question Bank</h3>;
  const endElement = (
    <div className="flex align-items-center gap-4">
      {/* <i
        className="pi pi-bell p-overlay-badge"
        onClick={() => alert("Show Notification dropdown")}
        style={{ fontSize: "1.6rem", cursor: "pointer" }}
      >
        {notificationCount ? (
          <Badge
            value={notificationCount > 9 ? "9+" : notificationCount}
          ></Badge>
        ) : null}
      </i> */}
      {preview && (
        <Avatar
          shape="circle"
          //label={user.email[0].toUpperCase()}
          image={preview}
          onClick={onProfileClicked}
          style={{ width: "2.5rem", height: "2.5rem" }}
        />
      )}{" "}
      {!preview && (
        <Avatar
          shape="circle"
          label={user.email[0].toUpperCase()}
          onClick={onProfileClicked}
          style={{ width: "2.5rem", height: "2.5rem" }}
        />
      )}
    </div>
  );
  const menuButtonClickHandler = (e) => {
    navigate(e.item.path);
  };
  const profileMenuItems = [
    {
      items: [
        {
          command: handleAvatarClick,
          template: (item, options) => {
            return (
              <button
                onClick={(e) => options.onClick(e)}
                className={classNames(
                  options.className,
                  "w-full p-link flex align-items-center p-2 pl-4 text-color hover:surface-200 border-noround"
                )}
              >
                <Avatar
                  image={preview} //"https://primefaces.org/cdn/primereact/images/avatar/amyelsner.png"
                  className="mr-2"
                  shape="circle"
                />
                <div className="flex flex-column align">
                  <span className="font-bold">{user.fullName}</span>
                </div>
              </button>
            );
          },
        },
        {
          label: "Reset Password",
          icon: "pi pi-key",
          command: () => {
            setResetPasswordModalVisible(true);
          },
        },
        {
          label: "Logout",
          icon: "pi pi-sign-out",
          command: () => {
            dispatch(logout());
            navigate("/");
          },
        },
      ],
    },
  ];
  const items = [
    {
      label: "Question Bank",
      icon: "pi pi-home",
      path: appRoutes.allQuestion,
      roles: ["SysAdmin", "Reviewer", "Approver", "QuestionCreator"],
      command: menuButtonClickHandler,
    },
    {
      label: "My Question",
      icon: "pi pi-question",
      path: appRoutes.myQuestion,
      roles: ["QuestionCreator"],
      command: menuButtonClickHandler,
    },
    {
      label: "Category",
      icon: "pi pi-cog",
      path: appRoutes.category,
      roles: ["SysAdmin"],
      command: menuButtonClickHandler,
    },
    {
      label: "Review",
      icon: "pi pi-question",
      path: appRoutes.review,
      roles: ["Reviewer"],
      command: menuButtonClickHandler,
    },
    {
      label: "Approval",
      icon: "pi pi-check",
      path: appRoutes.approve,
      roles: ["Approver"],
      command: menuButtonClickHandler,
    },
    {
      label: "User",
      icon: "pi pi-user-plus",
      path: appRoutes.user,
      roles: ["SysAdmin"],
      command: menuButtonClickHandler,
    },
  ];

  return (
    <>
      <TopMenuBar
        startElement={startElement}
        items={items.filter((_) => _.roles.includes(user.role))}
        endElement={endElement}
      />
      <Menu
        model={profileMenuItems}
        popup
        ref={profileMenuRef}
        id="popup_menu_right"
        popupAlignment="right"
      />
      <ResetPassword
        modalVisible={resetPasswordModalVisible}
        setModalVisible={setResetPasswordModalVisible}
      />
      {modalVisible && (
        <ImageUploadModal
          visible={modalVisible}
          onHide={() => setModalVisible(false)}
          onUpload={handleUpload}
          setPreview={setPreview}
          preview={preview}
        />
      )}
      <Outlet />
    </>
  );
};

export default Layout;
