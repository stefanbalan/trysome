import React from "react";

import "./DogBreedButton.css";

export const DogBreedButton = (props) => {
  return (
    <button
      onClick={props.clicked}
      className={
        props.selected
          ? "dog-breed-button dog-breed-button-selected"
          : "dog-breed-button"
      }
    >
      {props.children}
    </button>
  );
};
