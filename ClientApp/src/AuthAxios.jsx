import axios from "axios";

const getAxios = () => {
  const headers = {};
  const token = localStorage.getItem("auth-token");
  if (token) headers["Authorization"] = `Bearer ${token}`;
  return axios.create({
    baseURL: "/",
    headers,
  });
};

export default getAxios;