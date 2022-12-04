import React, { useEffect, useState } from "react";
import "../registeredPages.scss";
import { useLocation, useNavigate } from "react-router-dom";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import procedureServices from "../../services/procedureServices";

export default function VisitProcedureEdit() {
  const location = useLocation();
  const navigate = useNavigate();
  const [error, setError] = useState("");
  const [procedure, setProcedure] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");

  const handleNavigate = () => {
    navigate("/employee/allanimals/visits/procedures", {
      state: {
        animalId: location.state.animalId,
        visitId: location.state.visitId,
      },
    });
  };

  useEffect(() => {
    getProcedure();
  }, []);
  async function getProcedure() {
    var procedure = await procedureServices.getProcedure(
      location.state.animalId,
      location.state.visitId,
      location.state.procedureId
    );
    setProcedure(procedure.name);
    setDescription(procedure.description);
    setPrice(procedure.cost);
  }
  const handleEdit = async (event) => {
    event.preventDefault();
    if (!price.toString().match(/^[1-9]*$/) || price.length === 0) {
      setError("Price input is invalid!");
    } else {
      if (procedure.length >= 3 && description.length >= 3) {
        procedureServices
          .updateProcedure(
            procedure,
            description,
            price,
            location.state.animalId,
            location.state.visitId,
            location.state.procedureId
          )
          .then((response) => {
            setError("");
            navigate("/employee/allanimals/visits/procedures", {
              state: {
                animalId: location.state.animalId,
                visitId: location.state.visitId,
              },
            });
          });
      } else {
        setError("Write atleast 3 letters!");
      }
    }
  };

  return (
    <>
      <div className="pages-container">
        <div className="pages-container-info">
          <div className="pages-container-info-header">
            <Button
              type="submit"
              className="button-back"
              onClick={handleNavigate}
            >
              Back
            </Button>
            <h2>Selected procedure edit</h2>
          </div>
          <div className="pages-container-info-form">
            <Form>
              <Form.Group size="lg" controlId="age">
                <Form.Label>Procedure`s name</Form.Label>
                <Form.Control
                  type="text"
                  value={procedure}
                  onChange={(e) => setProcedure(e.target.value)}
                />
              </Form.Group>
              <Form.Group size="lg" controlId="age">
                <Form.Label>Description</Form.Label>
                <Form.Control
                  type="text"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                />
              </Form.Group>
              <Form.Group size="lg" controlId="age">
                <Form.Label>Price</Form.Label>
                <Form.Control
                  type="text"
                  value={price}
                  onChange={(e) => setPrice(e.target.value)}
                />
              </Form.Group>
              <p className="error-message">{error}</p>
              <br />
              <Button
                type="submit"
                className="button-confirm"
                onClick={handleEdit}
              >
                Confirm
              </Button>
            </Form>
          </div>
        </div>
      </div>
    </>
  );
}
