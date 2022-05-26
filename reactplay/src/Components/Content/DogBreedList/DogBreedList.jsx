import React, { useEffect, useState } from "react";
import { DogBreedButton } from "./DogBreedButton/DogBreedButton";

import "./DogBreedList.css";

export const DogBreedList = (props) => {
  const [dogbreeeds, setDogBreeds] = useState(["akita", "beagle", "chelutu'"]);
  const [selectedDogIndex, setBreedIndex] = useState(-1);

  const getRandomDogForBreed = (breed) => {
    fetch(`https://dog.ceo/api/breed/${breed}/images/random`)
      .then((r) => r.json())
      .then((data) => console.log(data.message));
  };

  useEffect(() => {
    console.log("did update");
  });

  useEffect(() => {
    console.log("did mount");
    fetch("https://dog.ceo/api/breeds/list/all")
      .then((r) => r.json())
      .then((data) => {
        const breeds = Object.keys(data.message);
        setDogBreeds(breeds);
        console.log(data);
      });
  }, []);

  useEffect(() => {
    console.log("did update with deps");
  }, [selectedDogIndex]);

  return (
    <div className="content__dog-list__container">
      <h2 className="dog-list-title">Choose a dog breed</h2>
      <div className="content__dog-list">
        {dogbreeeds.map((breed, index) => {
          return (
            <DogBreedButton
              key={index}
              selected={index === selectedDogIndex}
              clicked={() => {
                getRandomDogForBreed(breed);
                setBreedIndex(index);
              }}
            >
              {breed}
            </DogBreedButton>
          );
        })}
      </div>
    </div>
  );
};
