import axios from "axios";

const API_URL = "http://localhost:1234/api";

class visitService {
  createVisit(text, animalId) {
    var data = JSON.stringify({
      description: text,
    });

    var config = {
      method: "post",
      url: API_URL + "/animals/" + animalId + "/visits",
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
  getVisits(animalId) {
    var config = {
      method: "get",
      url: API_URL + "/animals/" + animalId + "/visits",
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
  getVisit(animalId, visitId) {
    var config = {
      method: "get",
      url: API_URL + "/animals/" + animalId + "/visits/" + visitId,
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
  updateVisit(text, animalId, visitId) {
    var data = JSON.stringify({
      description: text,
    });
    var config = {
      method: "put",
      url: API_URL + "/animals/" + animalId + "/visits/" + visitId,
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
  deleteVisit(animalId, visitId) {
    var config = {
      method: "delete",
      url: API_URL + "/animals/" + animalId + "/visits/" + visitId,
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
}

export default new visitService();
