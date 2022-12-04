import axios from "axios";

const API_URL = "http://localhost:1234/api";

class procedureService {
  getProcedures(animalId, visitId) {
    var config = {
      method: "get",
      url:
        API_URL + "/animals/" + animalId + "/visits/" + visitId + "/procedures",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    };
    return axios(config)
      .then(function (response) {
        return response.data;
      })
      .catch(function (error) {
        return error;
      });
  }
  getProcedure(animalId, visitId, procedureId) {
    var config = {
      method: "get",
      url:
        API_URL +
        "/animals/" +
        animalId +
        "/visits/" +
        visitId +
        "/procedures/" +
        procedureId,
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    };
    return axios(config)
      .then(function (response) {
        return response.data;
      })
      .catch(function (error) {
        return error;
      });
  }
  updateProcedure(
    procedure,
    description,
    price,
    animalId,
    visitId,
    procedureId
  ) {
    var data = JSON.stringify({
      name: procedure,
      description: description,
      cost: parseInt(price),
    });
    var config = {
      method: "put",
      url:
        API_URL +
        "/animals/" +
        animalId +
        "/visits/" +
        visitId +
        "/procedures/" +
        procedureId,
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      data: data,
    };
    return axios(config)
      .then(function (response) {
        return response.data;
      })
      .catch(function (error) {
        return error;
      });
  }
  deleteProcedure(animalId, visitId, procedureId) {
    var config = {
      method: "delete",
      url:
        API_URL +
        "/animals/" +
        animalId +
        "/visits/" +
        visitId +
        "/procedures/" +
        procedureId,
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
    };
    return axios(config)
      .then(function (response) {
        return response.data;
      })
      .catch(function (error) {
        return error;
      });
  }
  createProcedure(procedure, description, price, animalId, visitId) {
    var data = JSON.stringify({
      name: procedure,
      description: description,
      cost: parseInt(price),
    });
    var config = {
      method: "post",
      url:
        API_URL + "/animals/" + animalId + "/visits/" + visitId + "/procedures",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      data: data,
    };
    return axios(config)
      .then(function (response) {
        return response.data;
      })
      .catch(function (error) {
        return error;
      });
  }
}

export default new procedureService();
