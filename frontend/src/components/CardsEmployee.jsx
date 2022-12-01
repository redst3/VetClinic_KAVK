import React from "react";
import CardItem from "./CardItem";
import "./Cards.css";
import photo from "../images/temp.png";

function Cards() {
  return (
    <div className="cards">
      <h1>Select action</h1>
      <div className="cards__container">
        <div className="cards__wrapper">
          <ul className="cards__items">
            <CardItem
              src={photo}
              text="Testavimas"
              label="Testavimas"
              path="/"
            />
            <CardItem
              src={photo}
              text="Testavimas"
              label="Testavimas"
              path="/"
            />
            <CardItem
              src={photo}
              text="Testavimas"
              label="Testavimas"
              path="/"
            />
          </ul>
          <ul className="cards__items">
            <CardItem
              src={photo}
              text="Testavimasaaaaa"
              label="Testavimas"
              path="/"
            />
            <CardItem
              src={photo}
              text="Testavimas"
              label="Testavimas"
              path="/"
            />
            <CardItem
              src={photo}
              text="Testavimasaaaa"
              label="Testavimas"
              path="/"
            />
          </ul>
        </div>
      </div>
    </div>
  );
}

export default Cards;
