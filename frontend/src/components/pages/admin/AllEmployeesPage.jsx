import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import authServices from "../../services/authServices";
import { confirmAlert } from "react-confirm-alert";
import "../registeredPages.scss";

export default function AllEmployeesPage() {
  const [users, setUsers] = useState([]);
  const [update, setUpdate] = useState();
  useEffect(() => {
    getUsers();
    return () => {
      getUsers();
      setUpdate(false);
    };
  }, [update]);
  async function getUsers() {
    var users = await authServices.getEmployees();
    setUsers(users);
  }
  const confirmWindow = (event) => {
    confirmAlert({
      title: "Confirm deletion",
      message: "This action can not be undone!",
      buttons: [
        {
          label: "Yes",
          onClick: () => handleDelete(event),
        },
        {
          label: "No",
        },
      ],
    });
  };
  const handleDelete = async (event) => {
    event.preventDefault();
    await authServices.remove(event.target.value);
    setUpdate(true);
  };
  const confirmWindowRole = (event) => {
    confirmAlert({
      title: "Confirm role transfer?",
      buttons: [
        {
          label: "Yes",
          onClick: () => handleRoleTransfer(event),
        },
        {
          label: "No",
        },
      ],
    });
  };
  const handleRoleTransfer = async (event) => {
    event.preventDefault();
    await authServices.updateRole(event.target.value);
    setUpdate(true);
  };

  return (
    <>
      <div className="pages-container">
        <div className="pages-container-info">
          <div className="pages-container-info-header">
            <h2>Here you can find all registered employees</h2>
          </div>
          {users.length !== 0 ? (
            <ul className="responsive-table">
              <li className="table-header">
                <div className="col col-1-users">Username</div>
                <div className="col col-2-users">Email</div>
                <div className="col col-3-users">Role Options</div>
                <div className="col col-4-users">Options</div>
              </li>
              {users.map((user) => {
                return (
                  <li className="table-row" key={user.id}>
                    <div
                      className="col col-1-users"
                      data-label="Name"
                      data-key={user.userName}
                    >
                      {user.userName}
                    </div>
                    <div
                      className="col col-2-users"
                      data-label="Email"
                      data-key={user.email}
                    >
                      {user.email}
                    </div>
                    <div className="col col-3-users" data-label="Role">
                      <button
                        className="button-visits"
                        onClick={confirmWindowRole}
                        value={user.id}
                      >
                        Make User
                      </button>
                    </div>
                    <div className="col col-4-users" data-label="Options">
                      <button
                        className="button-delete"
                        onClick={confirmWindow}
                        value={user.id}
                      >
                        {" "}
                        Delete
                      </button>
                    </div>
                  </li>
                );
              })}
            </ul>
          ) : (
            <h1>Sorry, we could not find any registered users </h1>
          )}
        </div>
      </div>
    </>
  );
}
