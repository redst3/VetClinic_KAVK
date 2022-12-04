import React, { useEffect, useState } from "react";
import "../registeredPages.scss";
import { useNavigate } from "react-router-dom";

export default function AllUsersPage() {
  const [users, setUsers] = useState([]);
  const navigate = useNavigate();
  //   useEffect(() => {
  //     getAnimals();
  //   }, []);
  //   async function getAnimals() {
  //     var animals = await animalService.getAnimals();
  //     setAnimals(animals);
  //   }

  return (
    <>
      <div className="pages-container">
        <div className="pages-container-info">
          <div className="pages-container-info-header">
            <h2>Here you can find all registered users</h2>
          </div>
          {users.length !== 0 ? (
            <ul className="responsive-table">
              <li className="table-header">
                <div className="col col-1">Pet`s name</div>
                <div className="col col-2">Type</div>
                <div className="col col-3">Breed</div>
                <div className="col col-4">Visits</div>
                <div className="col col-5">Options</div>
              </li>
              {users.map((animal) => {
                return (
                  <li className="table-row" key={animal.id}>
                    <div
                      className="col col-1"
                      data-label="Name"
                      data-key={animal.name}
                    >
                      {animal.name}
                    </div>
                    <div
                      className="col col-2"
                      data-label="Type"
                      data-key={animal.type}
                    >
                      {animal.type}
                    </div>
                    <div
                      className="col col-3"
                      data-label="Breed"
                      data-key={animal.breed}
                    >
                      {animal.breed}
                    </div>
                    <div className="col col-4" data-label="Visits">
                      <button
                        className="button-visits"
                        onClick={console.log("")}
                        value={animal.id}
                      >
                        Check history
                      </button>
                    </div>
                    <div className="col col-5" data-label="Options">
                      <button
                        className="button-visits"
                        onClick={console.log("")}
                        value={animal.id}
                      >
                        {" "}
                        Add new visit
                      </button>
                    </div>
                  </li>
                );
              })}
            </ul>
          ) : (
            <h1>Sorry, we could not find any registered users </h1>
          )}
        </div>
      </div>
    </>
  );
}
