import { useParams } from "react-router-dom";

function Test() {
  const { testid } = useParams();
  console.log(testid)

  return (
    <div>
      <h1>TEST</h1>
      <p>Test ID: {testid}</p>
    </div>
  );
}

export default Test;
