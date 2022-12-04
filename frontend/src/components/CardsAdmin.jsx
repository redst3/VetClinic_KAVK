import React from "react";
import CardItem from "./CardItem";
import "./Cards.css";
import photo from "../images/users.png";

function Cards() {
  return (
    <div className="cards">
      <h1>Select action</h1>
      <div className="cards__container">
        <div className="cards__wrapper">
          <ul className="cards__items">
            <CardItem
              src={photo}
              text="MANAGE USERS"
              label="Users"
              path="/admin/users"
            />
          </ul>
        </div>
      </div>
    </div>
  );
}

export default Cards;
