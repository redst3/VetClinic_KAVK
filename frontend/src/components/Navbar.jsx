import React, { useState, useEffect } from "react";
import "./Navbar.css";
import { Link } from "react-router-dom";
import { Button } from "./Button";
import authServices from "./services/authServices";

function Navbar() {
  const [click, setClick] = useState(false);
  const [button, setButton] = useState(true);
  const [user, setUser] = useState();
  // const [navbar, setNavbar] = useState();
  const [log, setLog] = useState();
  const handleClick = () => setClick(!click);
  const closeMobileMenu = () => setClick(setClick(false));
  const handleLogout = () => authServices.logout();
  const handleLogoutMobile = () => {
    closeMobileMenu();
    authServices.logout();
  };

  useEffect(() => {
    setUser(localStorage.getItem("role"));
    // setNavbar(localStorage.getItem("navbarUpdate"));
  }, []);
  useEffect(() => {
    showLoginButton();
  }, [user]);

  // useEffect(() => {
  //   updateNavbar();
  // }, [navbar]);

  // const updateNavbar = () => {
  //   if (localStorage.getItem("navbarUpdate")) {
  //     window.location.reload();
  //     localStorage.removeItem("navbarUpdate");
  //   }
  // };
  const showLoginButton = () => {
    if (user === undefined || user === null) {
      setLog(
        <Button buttonStyle="btn--outline" link="/login">
          Login
        </Button>
      );
    } else {
      setLog(
        <Button buttonStyle="btn--outline" link="/login" onClick={handleLogout}>
          Logout
        </Button>
      );
    }
  };
  const showButton = () => {
    if (window.innerWidth <= 960) {
      setButton(false);
    } else {
      setButton(true);
    }
  };

  window.addEventListener("resize", showButton);
  return (
    <>
      <nav className="navbar">
        <div className="navbar-container">
          <Link to="/" className="navbar-logo" onClick={closeMobileMenu}>
            <i className="fa-solid fa-stethoscope" /> KAVK
          </Link>
          <div className="menu-icon" onClick={handleClick}>
            <i className={click ? "fas fa-times" : "fas fa-bars"} />
          </div>
          <ul className={click ? "nav-menu active" : "nav-menu"}>
            {user === "User" || user === "Admin" ? (
              <li className="nav-item">
                <Link
                  to="/user"
                  className="nav-links"
                  onClick={closeMobileMenu}
                >
                  User Page
                </Link>
              </li>
            ) : null}
            {user === "Employee" || user === "Admin" ? (
              <li className="nav-item">
                <Link
                  to="/employee"
                  className="nav-links"
                  onClick={closeMobileMenu}
                >
                  Employee Page
                </Link>
              </li>
            ) : null}
            {user === "Admin" ? (
              <li className="nav-item">
                <Link
                  to="/admin"
                  className="nav-links"
                  onClick={closeMobileMenu}
                >
                  Admin Page
                </Link>
              </li>
            ) : null}
            {user !== null ? (
              <li className="nav-item">
                <Link
                  to="/login"
                  className="nav-links-mobile"
                  onClick={handleLogoutMobile}
                >
                  Logout
                </Link>
              </li>
            ) : (
              <li className="nav-item">
                <Link
                  to="/login"
                  className="nav-links-mobile"
                  onClick={closeMobileMenu}
                >
                  Login
                </Link>
              </li>
            )}
          </ul>
          {button && log}
        </div>
      </nav>
    </>
  );
}

export default Navbar;
