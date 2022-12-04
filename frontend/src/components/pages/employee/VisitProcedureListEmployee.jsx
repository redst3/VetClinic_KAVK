import React, { useEffect, useState } from "react";
import "../registeredPages.scss";
import { useLocation, useNavigate } from "react-router-dom";
import procedureServices from "../../services/procedureServices";
import { confirmAlert } from "react-confirm-alert";

export default function VisitProcedureListEmployee() {
  const location = useLocation();
  const [procedures, setProcedures] = useState([]);
  const [update, setUpdate] = useState();
  const navigate = useNavigate();

  useEffect(() => {
    getProcedures();
    return () => {
      getProcedures();
      setUpdate(false);
    };
  }, [update]);

  async function getProcedures() {
    var procedures = await procedureServices.getProcedures(
      location.state.animalId,
      location.state.visitId
    );
    setProcedures(procedures);
  }
  const handleNavigate = () => {
    navigate("/employee/allanimals/visits", {
      state: { animalId: location.state.animalId },
    });
  };

  const handleEdit = async (event) => {
    event.preventDefault();
    navigate("edit", {
      state: {
        animalId: location.state.animalId,
        visitId: location.state.visitId,
        procedureId: event.target.value,
      },
    });
  };
  const confirmWindow = (event) => {
    confirmAlert({
      title: "Confirm deletion",
      message: "This action can not be undone!",
      buttons: [
        {
          label: "Yes",
          onClick: () => handleDelete(event),
        },
        {
          label: "No",
        },
      ],
    });
  };
  const handleDelete = async (event) => {
    event.preventDefault();
    await procedureServices.deleteProcedure(
      location.state.animalId,
      location.state.visitId,
      event.target.value
    );
    setUpdate(true);
  };
  const handleCreate = async (event) => {
    event.preventDefault();
    navigate("new", {
      state: {
        animalId: location.state.animalId,
        visitId: location.state.visitId,
      },
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
            <h2>Here you can find procedure history for the selected animal</h2>
            <button className="button-visits" onClick={handleCreate}>
              {" "}
              Add a new procedure to the visit
            </button>
          </div>
          {procedures.length !== 0 ? (
            <ul className="responsive-table">
              <li className="table-header">
                <div className="col col-1-visit">Procedure</div>
                <div className="col col-2-visit">Description</div>
                <div className="col col-3-visit">Options</div>
              </li>
              {procedures.map((procedure) => {
                return (
                  <li className="table-row" key={procedure.id}>
                    <div
                      className="col col-1-visit"
                      data-label="Name"
                      data-key={procedure.name}
                    >
                      {procedure.name}
                    </div>
                    <div
                      className="col col-2-visit"
                      data-label="Description"
                      data-key={procedure.description}
                    >
                      {procedure.description}
                    </div>
                    <div className="col col-3-visit" data-label="Options">
                      <button
                        className="button-edit"
                        onClick={handleEdit}
                        value={procedure.id}
                      >
                        {" "}
                        Edit
                      </button>
                      <button
                        className="button-delete"
                        onClick={confirmWindow}
                        value={procedure.id}
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
            <h1>Sorry, we could not find any procedure history </h1>
          )}
        </div>
      </div>
    </>
  );
}
