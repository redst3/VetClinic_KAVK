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
  RegisteredAnimalsEditPage,
  RegisteredAnimalsCreatePage,
  RegisteredAllAnimalsPage,
  NewVisitPage,
  AnimalVisitListEmployee,
  AnimalVisitEdit,
  AnimalVisitList,
  VisitProcedureListEmployee,
  VisitProcedureEdit,
  VisitProcedureCreate,
  VisitProcedureList,
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
            <Route
              path="/user/useranimals/edit"
              element={
                <ProtectedRoutes
                  requiredRole={"User"}
                  page={<RegisteredAnimalsEditPage />}
                />
              }
            />
            <Route
              path="/user/useranimals/create"
              element={
                <ProtectedRoutes
                  requiredRole={"User"}
                  page={<RegisteredAnimalsCreatePage />}
                />
              }
            />
            <Route
              path="/user/useranimals/visits"
              element={
                <ProtectedRoutes
                  requiredRole={"User"}
                  page={<AnimalVisitList />}
                />
              }
            />
            <Route
              path="/user/useranimals/visits/procedures"
              element={
                <ProtectedRoutes
                  requiredRole={"User"}
                  page={<VisitProcedureList />}
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
              path="/employee/allanimals"
              element={
                <ProtectedRoutes
                  requiredRole={"Employee"}
                  page={<RegisteredAllAnimalsPage />}
                />
              }
            />
            <Route
              path="/employee/allanimals/newvisit"
              element={
                <ProtectedRoutes
                  requiredRole={"Employee"}
                  page={<NewVisitPage />}
                />
              }
            />
            <Route
              path="/employee/allanimals/visits"
              element={
                <ProtectedRoutes
                  requiredRole={"Employee"}
                  page={<AnimalVisitListEmployee />}
                />
              }
            />
            <Route
              path="/employee/allanimals/visits/edit"
              element={
                <ProtectedRoutes
                  requiredRole={"Employee"}
                  page={<AnimalVisitEdit />}
                />
              }
            />
            <Route
              path="/employee/allanimals/visits/procedures"
              element={
                <ProtectedRoutes
                  requiredRole={"Employee"}
                  page={<VisitProcedureListEmployee />}
                />
              }
            />
            <Route
              path="/employee/allanimals/visits/procedures/edit"
              element={
                <ProtectedRoutes
                  requiredRole={"Employee"}
                  page={<VisitProcedureEdit />}
                />
              }
            />
            <Route
              path="/employee/allanimals/visits/procedures/new"
              element={
                <ProtectedRoutes
                  requiredRole={"Employee"}
                  page={<VisitProcedureCreate />}
                />
              }
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
