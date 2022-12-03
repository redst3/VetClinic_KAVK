import React, { useEffect, useState } from "react";
import "../registeredPages.scss";
import animalService from "../../services/animalServices";
import { useLocation, useNavigate } from "react-router-dom";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";

export default function RegisteredAnimalsEditPage() {
  const location = useLocation();
  const navigate = useNavigate();
  const [error, setError] = useState("");
  const [age, setAge] = useState("");
  const [breed, setBreed] = useState("");
  const [name, setName] = useState("");
  const [type, setType] = useState("");
  useEffect(() => {
    getAnimal();
  }, []);
  async function getAnimal() {
    var animal = await animalService.getAnimal(location.state.animalId);
    setAge(animal.age);
    setBreed(animal.breed);
    setName(animal.name);
    setType(animal.type);
  }
  const handleNavigate = () => {
    navigate("/user/useranimals");
  };
  const handleEdit = async (event) => {
    event.preventDefault();
    if (!age.toString().match(/^[1-9]*$/)) {
      setError("Age input is invalid!");
    }
    if (age >= 99) {
      setError("Animal is too old!");
    } else {
      if (breed.length >= 3 && name.length >= 3 && type.length >= 3) {
        animalService
          .updateAnimal(age, breed, name, type, location.state.animalId)
          .then((response) => {
            setError("");
            navigate("/user/useranimals");
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
            <h2>Edit animal</h2>
          </div>
          <div className="pages-container-info-form">
            <Form>
              <Form.Group size="lg" controlId="age">
                <Form.Label>Age</Form.Label>
                <Form.Control
                  type="text"
                  value={age}
                  onChange={(e) => setAge(e.target.value)}
                />
              </Form.Group>
              <Form.Group size="lg" controlId="breed">
                <Form.Label>Breed</Form.Label>
                <Form.Control
                  type="text"
                  value={breed}
                  onChange={(e) => setBreed(e.target.value)}
                />
              </Form.Group>
              <Form.Group size="lg" controlId="name">
                <Form.Label>Name</Form.Label>
                <Form.Control
                  type="text"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
              </Form.Group>
              <Form.Group size="lg" controlId="type">
                <Form.Label>Type</Form.Label>
                <Form.Control
                  type="text"
                  value={type}
                  onChange={(e) => setType(e.target.value)}
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
