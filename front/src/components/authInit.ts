import { setAuthToken } from "../api"; // Import the setAuthToken function from your api.ts file

const initAuth = () => {
  const token = localStorage.getItem("token");
  if (token) {
    setAuthToken(token); // Set the token in the Axios headers if available in localStorage
  }
};

export default initAuth;
