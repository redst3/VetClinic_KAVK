import React, { useEffect, useState } from "react";
import "../registeredPages.scss";
import { useLocation, useNavigate } from "react-router-dom";
import visitServices from "../../services/visitServices";

export default function AnimalVisitList() {
  const location = useLocation();
  const [visits, setVisits] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getVisits();
  }, []);
  async function getVisits() {
    var visit = await visitServices.getVisits(location.state.animalId);
    setVisits(visit);
  }
  const handleNavigate = () => {
    navigate("/user/useranimals");
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
            <h2>Here you can find visit history for the selected animal</h2>
          </div>
          {visits.length !== 0 ? (
            <ul className="responsive-table">
              <li className="table-header">
                <div className="col col-1-visit-user">Visit description</div>
                <div className="col col-2-visit-user">Visits</div>
              </li>
              {visits.map((visit) => {
                return (
                  <li className="table-row" key={visit.id}>
                    <div
                      className="col col-1-visit-user"
                      data-label="Name"
                      data-key={visit.description}
                    >
                      {visit.description}
                    </div>
                    <div
                      className="col col-2-visit-user"
                      data-label="Procedures"
                    >
                      <button
                        className="button-visits"
                        value={visit.id}
                        onClick={handleProcedures}
                      >
                        Procedures
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
