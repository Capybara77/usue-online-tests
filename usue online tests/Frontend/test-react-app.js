import ReactDOM from "react-dom";

function Test() {
  return <h1>Этот текст вставился через реакт</h1>;
}

if (document.querySelector("#test-react-app")) {
  ReactDOM.render(<Test />, document.querySelector("#test-react-app"));
}
