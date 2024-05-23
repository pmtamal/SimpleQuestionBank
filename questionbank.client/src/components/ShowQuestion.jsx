/* eslint-disable react/jsx-key */
/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import { useState, useEffect } from "react";
import DOMPurify from "dompurify";
import { Panel } from "primereact/panel";
import { Tag } from "primereact/tag";

const ShowQuestion = (props) => {
  const { questionInfo } = props;

  const tagsColor = ["success", "info", "warning", "danger"];

  console.log("question inside showquestion", questionInfo);

  const tagElement = () => {
    return (
      questionInfo.tags[0] && (
        <div className="flex flex-wrap gap-2 px-4 py-1">
          {questionInfo.tags.map((tag, index) => {
            return (
              <div>
                <Tag severity={tagsColor[index % 4]} value={tag}></Tag>
              </div>
            );
          })}
        </div>
      )
    );
  };

  return (
    <>
      <div className="card flex my-4">
        <div className="flex flex-column w-full">
          <Panel header={"Question Title"}>
            <p className="m-0">{questionInfo.title}</p>
          </Panel>
        </div>
      </div>

      <div className="card flex my-4">
        <div className="flex flex-column w-full">
          <Panel header={"Question Description"} footerTemplate={tagElement}>
            <p
              className="m-0"
              dangerouslySetInnerHTML={{
                __html: DOMPurify.sanitize(questionInfo.description),
              }}
            />
          </Panel>
        </div>
      </div>

      <div className="card flex my-4">
        <div className="flex flex-column w-full">
          <Panel header={"Sample Answer"}>
            <p className="m-0">{questionInfo.sampleAnswer}</p>
          </Panel>
        </div>
      </div>

      <div className="card flex my-4">
        <div className="flex flex-column w-full">
          <Panel>
            <div className="flex flex-wrap gap-2">
              <div className="">
                <p className="m-0"> Category: {questionInfo.categoryName} </p>
              </div>
            </div>
          </Panel>
        </div>
      </div>
    </>
  );
};

export default ShowQuestion;
