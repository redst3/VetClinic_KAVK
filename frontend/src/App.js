import "./App.css";
import Navbar from "./components/Navbar";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Outlet,
} from "react-router-dom";
import ProtectedRoutes from "./components/services/protectedRoutes";
import {
  HomePage,
  AdminPage,
  EmployeePage,
  UserPage,
  SignUpPage,
  LoginPage,
  RegisteredAnimalsPage,
} from "./components/pages/index";
import Footer from "./components/Footer";

function App() {
  return (
    <>
      <Router>
        <Navbar />
        <Routes>
          <Route path="/" element={<HomePage />} />
          // User protected routes
          <Route>
            <Route
              path="/user"
              element={
                <ProtectedRoutes requiredRole={"User"} page={<UserPage />} />
              }
            />
            <Route
              path="/user/useranimals"
              element={
                <ProtectedRoutes
                  requiredRole={"User"}
                  page={<RegisteredAnimalsPage />}
                />
              }
            />
          </Route>
          // Employee protected routes
          <Route>
            <Route
              path="/employee"
              element={
                <ProtectedRoutes
                  requiredRole={"Employee"}
                  page={<EmployeePage />}
                />
              }
            />
            <Route
              path="/employee/"
              element={<ProtectedRoutes requiredRole={"Employee"} page="" />}
            />
          </Route>
          // Admin protected routes
          <Route>
            <Route
              path="/admin"
              element={
                <ProtectedRoutes requiredRole={"Admin"} page={<AdminPage />} />
              }
            />
            <Route
              path="/admin/"
              element={<ProtectedRoutes requiredRole={"Admin"} page="" />}
            />
          </Route>
          <Route path="/" element={<Outlet />}>
            <Route path="/sign-up" element={<SignUpPage />} />
            <Route path="/login" element={<LoginPage />} />
          </Route>
        </Routes>
        <Footer />
      </Router>
    </>
  );
}

export default App;
