import axios from "axios";
import jwt from "jwt-decode";

const API_URL = "http://localhost:1234/api";

class animalService {
  getAnimals() {
    var config = {
      method: "get",
      url: API_URL + "/animals?pageNumber=1&pageSize=25",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    };
    return axios(config)
      .then(function (response) {
        return response.data;
      })
      .catch(function (error) {
        console.log(error);
      });
  }
  getAnimal(animalId) {
    var config = {
      method: "get",
      url: "http://localhost:1234/api/animals/" + animalId,
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    };

    return axios(config)
      .then(function (response) {
        return response.data;
      })
      .catch(function (error) {
        console.log(error);
      });
  }
  updateAnimal(age, breed, name, type, animalId) {
    var user = JSON.parse(localStorage.getItem("user"));
    let userName =
      user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    var data = JSON.stringify({
      age: age,
      breed: breed,
      name: name,
      type: type,
      owner: userName,
      UserId: user["sub"],
    });
    var config = {
      method: "put",
      url: "http://localhost:1234/api/animals/" + animalId,
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      data: data,
    };
    return axios(config)
      .then(function (response) {
        return response;
      })
      .catch(function (error) {
        return error;
      });
  }
  createAnimal(age, breed, name, type) {
    var user = JSON.parse(localStorage.getItem("user"));
    let userName =
      user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    var data = JSON.stringify({
      age: age,
      breed: breed,
      name: name,
      type: type,
      owner: userName,
      UserId: user["sub"],
    });
    var config = {
      method: "post",
      url: "http://localhost:1234/api/animals/",
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      data: data,
    };
    return axios(config)
      .then(function (response) {
        return response;
      })
      .catch(function (error) {
        return error;
      });
  }
  deleteAnimal(animalId) {
    var config = {
      method: "delete",
      url: "http://localhost:1234/api/animals/" + animalId,
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    };
    return axios(config)
      .then(function (response) {
        return response;
      })
      .catch(function (error) {
        return error;
      });
  }
}

export default new animalService();
