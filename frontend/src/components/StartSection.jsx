import React from "react";
import { Button } from "./Button";
import "../App.css";
import "./StartSection.css";

function StartSection() {
  return (
    <div className="hero-container">
      <h1>VETERINARY CLINIC</h1>
      <p>
        No matter how little money and how few possessions you own, having an
        animal makes you feel rich !
      </p>
      <div className="hero-btns">
        <Button
          className="btns"
          buttonStyle="btn--outline"
          buttonSize="btn--large"
          link="/sign-up"
        >
          GET STARTED
        </Button>
      </div>
    </div>
  );
}

export default StartSection;
