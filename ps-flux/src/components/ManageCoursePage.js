import React from "react";
import { Link, Prompt } from "react-router-dom";

const ManageCoursePage = (props) => {
  return (
    <>
      <h2>Manage course</h2>
      <Prompt when={true} message="Are you sure?"></Prompt>
      <p>{props.match.params.slug}</p>
    </>
  );
};

export default ManageCoursePage;
