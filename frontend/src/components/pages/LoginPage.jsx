import React, { useState, useEffect } from "react";
import "../../App.css";
import "./authPages.css";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { Link, useNavigate } from "react-router-dom";
import authServices from "../services/authServices";

export default function UserPage() {
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const handleSubmit = (event) => {
    event.preventDefault();
    authServices.login(username, password).then(
      (response) => {
        setSuccess("Login was succesful!");
        localStorage.setItem("navbarUpdate", true);
        navigate("/");
      },
      (error) => {
        setError("Wrong username or password!");
      }
    );
  };

  return (
    <>
      <div className="container">
        <div className="form">
          <h1>Login form</h1>
          <Form onSubmit={handleSubmit}>
            <Form.Group size="lg" controlId="username">
              <Form.Label>Username</Form.Label>
              <Form.Control
                type="text"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </Form.Group>
            <Form.Group size="lg" controlId="password">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="text"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </Form.Group>
            <p className="error-message">{error}</p>
            <p className="success-message">{success}</p>
            <Button type="submit">Login</Button>
            <Link className="form-help" to="/sign-up">
              If you are a new user, use this instead!
            </Link>
          </Form>
        </div>
      </div>
    </>
  );
}
