import "./App.css";
import Navbar from "./components/Navbar";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import {
  HomePage,
  AdminPage,
  EmployeePage,
  UserPage,
  SignUpPage,
  LoginPage,
} from "./components/pages/index";
import Footer from "./components/Footer";

function App() {
  return (
    <>
      <Router>
        <Navbar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/user" element={<UserPage />} />
          <Route path="/employee" element={<EmployeePage />} />
          <Route path="/admin" element={<AdminPage />} />
          <Route path="/sign-up" element={<SignUpPage />} />
          <Route path="/login" element={<LoginPage />} />
        </Routes>
        <Footer />
      </Router>
    </>
  );
}

export default App;
