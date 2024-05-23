/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState, useEffect } from "react";
import { InputText } from "primereact/inputtext";
import { Editor } from "primereact/editor";
import { InputTextarea } from "primereact/inputtextarea";
import { Dropdown } from "primereact/dropdown";
import { MultiSelect } from "primereact/multiselect";

import { getAllCategory } from "../../apiClient/categoryApi";
import { getAllTags } from "../../apiClient/tagApi";
const QuestionCreationPage = (props) => {
  const { questionId, questionInfo, setQuestionInfo } = props;

  const [quesCategories, setquesCategories] = useState(null);

  const [quesTags, setquesTags] = useState(null);

  useEffect(() => {
    const getTagAndCategoryList = async () => {
      let result = await getAllCategory();
      setquesCategories(result.data);
      result = await getAllTags();
      setquesTags(result.data);
    };
    getTagAndCategoryList();

    // need to call service here
  }, []);

  const formValueChangeHandler = (e) => {
    setQuestionInfo({ ...questionInfo, [e.target.name]: e.target.value });
  };

  return (
    <div>
      <div className="card flex my-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="title">Question Title</label>
          <InputText
            id="ques-title"
            value={questionInfo.title}
            onChange={formValueChangeHandler}
            name="title"
          />
        </div>
      </div>

      <div className="card flex my-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="description">Description</label>
          <Editor
            style={{ height: "200px" }}
            value={questionInfo.description}
            onTextChange={(e) => {
              setQuestionInfo({
                ...questionInfo,
                ["description"]: e.htmlValue,
              });
            }}
            name="description"
          />
        </div>
      </div>

      <div className="card flex my-4">
        <div className="flex flex-column gap-2 w-full">
          <label htmlFor="sampleAnswer">Sample Answer</label>
          <InputTextarea
            rows={5}
            id="sample-ans"
            value={questionInfo.sampleAnswer}
            onChange={formValueChangeHandler}
            name="sampleAnswer"
          />
        </div>
      </div>

      <div className="grid">
        <div className="sm:col-12 md:col-12 lg:col-6">
          <div className="card flex my-4">
            <div className="flex flex-column gap-2 sm:w-full md: w-full lg:w-9">
              <label htmlFor="category">Category</label>
              <Dropdown
                value={questionInfo.category}
                onChange={formValueChangeHandler}
                options={quesCategories}
                optionLabel="title"
                optionValue="id"
                checkmark={true}
                placeholder="Select Category"
                highlightOnSelect={false}
                name="category"
              />
            </div>
          </div>
        </div>
        <div className="sm:col-12 md:col-12 lg:col-6">
          <div className="card flex my-4">
            <div className="flex flex-column gap-2 sm:w-full md: w-full lg:w-9">
              <label htmlFor="title">Tags</label>
              <div className="card p-fluid">
                <MultiSelect
                  value={questionInfo.tags}
                  onChange={formValueChangeHandler}
                  options={quesTags}
                  optionLabel="name"
                  optionValue="id"
                  placeholder="Select Tags"
                  maxSelectedLabels={10}
                  name="tags"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default QuestionCreationPage;
