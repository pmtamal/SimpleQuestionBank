import React, { useState, useEffect } from "react";
import { Dialog } from "primereact/dialog";
import { Button } from "primereact/button";

const ImageUploadModal = ({
  visible,
  onHide,
  onUpload,
  preview,
  setPreview,
}) => {
  const [selectedFile, setSelectedFile] = useState(null);
  const [prevImage, setPrevImage] = useState(preview);

  useEffect(() => {});

  const resizeImage = (file, maxWidth, maxHeight, callback) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (event) => {
      const img = new Image();
      img.src = event.target.result;
      img.onload = () => {
        const canvas = document.createElement("canvas");
        const ctx = canvas.getContext("2d");

        const ratio = Math.min(maxWidth / img.width, maxHeight / img.height);
        canvas.width = img.width * ratio;
        canvas.height = img.height * ratio;

        ctx.drawImage(img, 0, 0, canvas.width, canvas.height);

        canvas.toBlob((blob) => {
          callback(blob);
        }, file.type);
      };
    };
  };

  const handleFileChange = (event) => {
    const file = event.target.files[0];

    if (file) {
      resizeImage(file, 300, 300, (resizedBlob) => {
        setSelectedFile(resizedBlob);

        const reader = new FileReader();
        reader.readAsDataURL(resizedBlob);
        reader.onloadend = () => {
          setPrevImage(reader.result);
        };
      });
    }
  };

  const handleUpload = () => {
    if (selectedFile) {
      onUpload(selectedFile);
      setPreview(prevImage);
      onHide();
    }
  };

  return (
    <Dialog visible={visible} onHide={onHide} header="Upload Image">
      <div className="flex flex-column align-items-center">
        <div className="relative">
          <img
            src={prevImage}
            alt="Avatar"
            className="w-48 h-48 rounded-full object-cover"
            onClick={() => document.getElementById("fileInput").click()}
          />
          <input
            type="file"
            id="fileInput"
            style={{ display: "none" }}
            onChange={handleFileChange}
          />
        </div>
        {selectedFile && <p>{selectedFile.name}</p>}
        <Button label="Upload" icon="pi pi-check" onClick={handleUpload} />
      </div>
    </Dialog>
  );
};

export default ImageUploadModal;
