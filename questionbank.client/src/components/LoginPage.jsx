import React from "react";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { Card } from "primereact/card";
import { motion } from "framer-motion";
import { useNavigate } from "react-router-dom";
import logo from "../assets/logo.svg";

const LoginForm = ({ setPassword, setUsername, handleLogin }) => {
  const handleKeyPress = (e) => {
    if (e.key === "Enter" && e.target.id === "password") {
      handleSubmit(e);
    }
  };
  const handleSubmit = (e) => {
    e.preventDefault();
    handleLogin();
    // Implement your login logic here
    //console.log("Submitted:", { username, password });
  };
  return (
    <form onSubmit={handleSubmit}>
      <div
        className="p-d-flex p-jc-center mx-auto"
        style={{ maxWidth: "400px" }}
      >
        <Card
          className="p-fluid mt-8"
          style={{
            width: "100%",
            position: "relative",
            overflow: "hidden",
            boxShadow: "0 4px 8px 0 rgba(0,0,0,0.2)",
          }}
        >
          <div
            style={{
              position: "absolute",
              width: "50px",
              height: "50px",
              background: "#fffdfd",
              top: "0",
              right: "0",
              borderTop: "2px solid #364F6B",
              borderRight: "2px solid #364F6B",
            }}
          ></div>
          <div
            style={{
              position: "absolute",
              width: "50px",
              height: "50px",
              background: "#fffdfd",
              bottom: "0",
              left: "0",
              borderBottom: "2px solid #364F6B",
              borderLeft: "2px solid #364F6B",
            }}
          ></div>
          <div
            className="flex flex-column gap-0 align-items-center"
            style={{ minHeight: "200px" }}
          >
            <img
              src={logo}
              alt="Logo"
              style={{
                width: "250px",
                height: "100px",
                margin: "0",
                padding: "0",
              }}
            />

            <h4 style={{ textAlign: "center" }}>
              Please Login to your account
            </h4>
          </div>

          {/* <form onSubmit={handleLogin}> */}
          <div>
            <span className="p-float-label mb-5">
              <InputText
                id="username"
                type="username"
                required
                onChange={(e) => setUsername(e.target.value)}
              />
              <label htmlFor="password" className="block">
                Enter your Name
              </label>
            </span>
            <span className="p-float-label mb-5">
              <InputText
                id="password"
                type="password"
                onChange={(e) => setPassword(e.target.value)}
                onKeyUp={handleKeyPress}
                required
              />
              <label htmlFor="password" className="block">
                Enter your password
              </label>
            </span>
            {/* <a style={{ float: "right" }} className="pink-text text-sm" href="#!">
            <b>Forgot Password?</b>
          </a> */}
          </div>
          <div className="flex justify-content-center mt-5">
            <motion.button
              whileHover={{ scale: 1.2 }}
              style={{
                border: "none",
                background: "none",
                padding: 0,
                cursor: "pointer",
                outline: "none",
              }}
            >
              <Button label="Login" className="p-button-indigo" type="submit" />
            </motion.button>
          </div>
          {/* </form> */}
        </Card>
      </div>
    </form>
  );
};

export default LoginForm;
