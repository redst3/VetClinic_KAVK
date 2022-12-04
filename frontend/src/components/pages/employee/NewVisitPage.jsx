import React, { useState } from "react";
import "../registeredPages.scss";
import { useLocation, useNavigate } from "react-router-dom";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import visitServices from "../../services/visitServices";

export default function NewVisitPage() {
  const location = useLocation();
  const navigate = useNavigate();
  const [error, setError] = useState("");
  const [text, setText] = useState("");

  const handleNavigate = () => {
    navigate("/employee/allanimals");
  };
  const handleEdit = async (event) => {
    event.preventDefault();
    if (text.length >= 3) {
      visitServices
        .createVisit(text, location.state.animalId)
        .then((response) => {
          setError("");
          navigate("/employee/allanimals");
        });
    } else {
      setError("Write atleast 3 letters!");
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
            <h2>Add new visit to animal - id: {location.state.animalId} </h2>
          </div>
          <div className="pages-container-info-form">
            <Form>
              <Form.Group size="lg" controlId="age">
                <Form.Label>Visit Name</Form.Label>
                <Form.Control
                  type="text"
                  value={text}
                  onChange={(e) => setText(e.target.value)}
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
