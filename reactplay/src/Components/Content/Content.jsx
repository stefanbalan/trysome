import React, { useState } from "react";

import "./Content.css";
import { DogBreedList } from "./DogBreedList/DogBreedList";

export const Content = () => {
  const [dogViewUrl, setDogViewUrl] = useState(
    "https://images.dog.ceo/breeds/ovcharka-caucasian/IMG_20190826_095310.jpg"
  );

  return (
    <main>
      <DogBreedList setDog={setDogViewUrl}></DogBreedList>
      
    </main>
  );
};
