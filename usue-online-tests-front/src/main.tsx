import App from "./App";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import UserPage from "./UserPage";
import Tests from "./Tests";
import Test from "./Test";
import ReactDOM from "react-dom";

const element = (
  <Router>
    <Routes>
      <Route path="/" element={<App />} />
      <Route path="/user" element={<UserPage />} />
      <Route path="/alltests" element={<Tests />} />
      <Route path="/test/:testid" element={<Test />} />
    </Routes>
  </Router>
);

ReactDOM.render(element, document.getElementById("root"));
