import axios from "axios";

const API_URL = "http://localhost:1234/api";

class AuthService {
  login(username, password) {
    var data = JSON.stringify({
      userName: username,
      password: password,
    });
    var config = {
      method: "post",
      url: API_URL + "/login",
      headers: {
        "Content-Type": "application/json",
      },
      data: data,
    };
    axios(config)
      .then(function (response) {
        console.log(JSON.stringify(response.data));
        localStorage.setItem("user", JSON.stringify(response.data));
        return true;
      })
      .catch(function (error) {
        console.log(error);
        return false;
      });

    // return axios
    //   .post(API_URL + { username: username, password: password })
    //   .then((response) => {
    //     if (response.data.token) {
    //       localStorage.setItem("user", JSON.stringify(response.data));
    //     }
    //     return response.data;
    //   });
  }
}
export default new AuthService();
