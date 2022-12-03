import axios from "axios";
import jwt from "jwt-decode";

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
    return axios(config).then(function (response) {
      if (response.data.accessToken) {
        const user = jwt(response.data.accessToken);
        const token = response.data.accessToken;
        localStorage.setItem("user", JSON.stringify(user));
        localStorage.setItem("token", token);
        let roles =
          user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        console.log(roles);
        if (roles.length === 3) {
          localStorage.setItem("role", "Admin");
        } else {
          localStorage.setItem("role", roles);
        }
      }
      return response.data;
    });
  }
  logout() {
    localStorage.clear();
  }
  register(username, password, email) {
    var data = JSON.stringify({
      userName: username,
      email: email,
      password: password,
    });
    var config = {
      method: "post",
      url: API_URL + "/register",
      headers: {
        "Content-Type": "application/json",
      },
      data: data,
    };
    return axios(config).then(function (response) {
      return response.data;
    });
  }
}
export default new AuthService();
