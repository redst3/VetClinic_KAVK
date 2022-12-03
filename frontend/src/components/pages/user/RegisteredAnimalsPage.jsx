import React, { useEffect, useState } from "react";
import "../registeredPages.scss";
import animalService from "../../services/animalServices";
import Table from "react-bootstrap/Table";

export default function RegisteredAnimalsPage() {
  const [animals, setAnimals] = useState([]);
  const [user, setUser] = useState();
  useEffect(() => {
    getAnimals();
  }, []);
  async function getAnimals() {
    var animals = await animalService.getAnimals();
    const localUser = JSON.parse(localStorage.getItem("user"));
    setUser(localUser);
    animals = animals.filter((animal) => {
      return animal.userId === localUser.sub;
    });
    setAnimals(animals);
    console.log(animals);
  }

  return (
    <>
      <div className="pages-container">
        <div className="pages-container-info">
          <h2>Here you can find all of your registered animals</h2>
          <ul className="responsive-table">
            <li className="table-header">
              <div className="col col-1">Pet`s name</div>
              <div className="col col-2">Type</div>
              <div className="col col-3">Breed</div>
              <div className="col col-4">Options</div>
            </li>
            {animals.map((animal) => {
              console.log(animals);
              return (
                <li className="table-row">
                  <div className="col col-1" data-label="name">
                    {animal.name}
                  </div>
                  <div className="col col-2" data-label="type">
                    {animal.type}
                  </div>
                  <div className="col col-3" data-label="breed">
                    {animal.breed}
                  </div>
                  <div className="col col-4" data-label="options">
                    {animal.id}
                  </div>
                </li>
              );
            })}
          </ul>
        </div>
      </div>
    </>
  );
}
