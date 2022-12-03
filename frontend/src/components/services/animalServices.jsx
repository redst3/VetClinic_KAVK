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
}

export default new animalService();
