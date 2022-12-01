import React from "react";
import "./Footer.css";

function Footer() {
  return (
    <div className="footer-container">
      <section className="footer-info">
        <p className="footer-info-heading">Redas Steckys IFF/9-1 - 2022</p>
        <br />
        <a
          target="_blank"
          href="https://github.com/redst3/VetClinic_KAVK"
          className="footer-info-link"
          rel="noreferrer"
        >
          Github <i className="fab fa-github"></i>
        </a>
      </section>
    </div>
  );
}

export default Footer;
