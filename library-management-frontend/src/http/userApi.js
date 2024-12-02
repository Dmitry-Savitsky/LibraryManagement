import jwtDecode from "jwt-decode"; import { $host } from ".";

export const register = async (name, email, password) => {
    try {
        console.log("Registration request data:");
        console.log(name, email, password);

        const { data } = await $host.post("api/user/register", {
            Name: name,
            Email: email,
            Password: password,
        });

        localStorage.setItem("token", data.token);
        console.log("Token: " + data.token);

        return data.token;
    } catch (error) {
        console.error("Registration error:", error);
        throw error;
    }
};


export const login = async (email, password) => {
    try {
        console.log("Login request data: " + email + " " + password);

        const { data } = await $host.post("api/user/login", {
            Email: email,
            Password: password,
        });

        localStorage.setItem("token", data.token);
        console.log("Token: " + data.token);

        return data.token;
    } catch (error) {
        console.error("Login error:", error);
        throw error;
    }
};

export const logout = () => {
    localStorage.removeItem("token"); console.log("User logged out.");
};
