import React, { useEffect, useState } from "react";
import "../registeredPages.scss";
import { useLocation, useNavigate } from "react-router-dom";
import visitServices from "../../services/visitServices";

export default function AnimalVisitListEmployee() {
  const location = useLocation();
  const [visits, setVisits] = useState([]);
  const [update, setUpdate] = useState();
  const navigate = useNavigate();

  useEffect(() => {
    getVisits();
    return () => {
      getVisits();
      setUpdate(false);
    };
  }, [update]);
  async function getVisits() {
    var visit = await visitServices.getVisits(location.state.animalId);
    setVisits(visit);
  }
  const handleNavigate = () => {
    navigate("/employee/allanimals");
  };
  const handleEdit = async (event) => {
    event.preventDefault();
    navigate("edit", {
      state: { animalId: location.state.animalId, visitId: event.target.value },
    });
  };
  const handleDelete = async (event) => {
    event.preventDefault();
    await visitServices.deleteVisit(
      location.state.animalId,
      event.target.value
    );
    setUpdate(true);
  };
  const handleProcedures = async (event) => {
    event.preventDefault();
    navigate("procedures", {
      state: { visitId: event.target.value, animalId: location.state.animalId },
    });
  };

  return (
    <>
      <div className="pages-container">
        <div className="pages-container-info">
          <div className="pages-container-info-header">
            <button className="button-new" onClick={handleNavigate}>
              {" "}
              Back
            </button>
            <h2>
              Here you can find visit history for the selected animal with id:{" "}
              {location.state.animalId}
            </h2>
          </div>
          {visits.length !== 0 ? (
            <ul className="responsive-table">
              <li className="table-header">
                <div className="col col-1-visit">Visit description</div>
                <div className="col col-2-visit">Visits</div>
                <div className="col col-3-visit">Options</div>
              </li>
              {visits.map((visit) => {
                return (
                  <li className="table-row" key={visit.id}>
                    <div
                      className="col col-1-visit"
                      data-label="Name"
                      data-key={visit.description}
                    >
                      {visit.description}
                    </div>
                    <div className="col col-2-visit" data-label="Procedures">
                      <button
                        className="button-visits"
                        value={visit.id}
                        onClick={handleProcedures}
                      >
                        Procedures
                      </button>
                    </div>
                    <div className="col col-3-visit" data-label="Options">
                      <button
                        className="button-edit"
                        onClick={handleEdit}
                        value={visit.id}
                      >
                        {" "}
                        Edit
                      </button>
                      <button
                        className="button-delete"
                        onClick={handleDelete}
                        value={visit.id}
                      >
                        {" "}
                        Delete
                      </button>
                    </div>
                  </li>
                );
              })}
            </ul>
          ) : (
            <h1>Sorry, we could not find any visit history </h1>
          )}
        </div>
      </div>
    </>
  );
}
