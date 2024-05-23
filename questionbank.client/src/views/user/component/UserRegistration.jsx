import { DialogBody } from "./DialogBody";
import { useState, useEffect } from "react";
import { Dialog } from "primereact/dialog";
import { DialogHeader } from "../../../components/DialogHeader";
import { DialogFooter } from "../../../components/DialogFooter";
import {
  registerUser,
  getAllRoles,
  getTags,
  getUserByUserId,
  updateUser,
} from "../userApi";

const UserRegistration = ({ modalVisible, setModalVisible, userId = 0 }) => {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    cellNo: "",
    roleId: null,
    tagIds: [],
    person: {
      firstName: "",
      middleName: "",
      lastName: "",
      address: "",
      dob: "",
    },
  });

  const [tagOptions, setTagOptions] = useState([]);

  const [roleOptions, setRoleOptions] = useState([]);

  useEffect(() => {
    const getRoles = async () => {
      const result = await getAllRoles();

      setRoleOptions(
        result.data.map((item) => ({
          value: item.id,
          label: item.name,
        }))
      );
    };

    const getSkillsTag = async () => {
      const result = await getTags();
      setTagOptions(
        result.data.map((item) => ({
          value: item.id,
          label: item.name,
        }))
      );
    };

    function formatDate(dateStr) {
      const dateObj = new Date(dateStr);
      const year = dateObj.getFullYear();
      const month = String(dateObj.getMonth() + 1).padStart(2, "0"); // Months are zero-based
      const day = String(dateObj.getDate()).padStart(2, "0");

      return `${year}-${month}-${day}`;
    }

    const getUserInformations = async () => {
      let result = await getUserByUserId(userId);
      setFormData({
        ...result.data,
        person: {
          ...result.data.person,
          dob: formatDate(result.data.person.dob),
        },
      });
    };

    getRoles();
    getSkillsTag();
    if (userId > 0) {
      getUserInformations();
    }
  }, []);

  const handleChange = (e, field, subField = null) => {
    const { value } = e.target;
    setFormData((prevData) => {
      if (subField !== null) {
        return {
          ...prevData,
          person: {
            ...prevData.person,
            [subField]: value,
          },
        };
      }
      return {
        ...prevData,
        [field]: value,
      };
    });
    //console.log("adasdasda:  ", formData);
  };

  const handleDialogSubmit = async () => {
    try {
      if (userId > 0) {
        updateUser(formData);
      } else {
        await registerUser(formData);
      }
      setModalVisible(false);
    } catch (error) {}
  };

  const handleDialogCancel = () => {
    setModalVisible(false);
  };

  return (
    <>
      <div>
        {modalVisible && (
          <Dialog
            header={<DialogHeader title={"User Registration"} />}
            blockScroll={true}
            visible={modalVisible}
            position="center"
            style={{ width: "50vw", height: "100%" }}
            footer={
              <DialogFooter
                onModalCancel={handleDialogCancel}
                onModalSubmit={handleDialogSubmit}
              />
            }
            draggable={false}
            resizable={false}
            maximizable
            onHide={() => setModalVisible(false)}
          >
            <DialogBody
              formData={formData}
              handleChange={handleChange}
              roleOptions={roleOptions}
              tagOptions={tagOptions}
            />
          </Dialog>
        )}
      </div>
    </>
  );
};

export default UserRegistration;
