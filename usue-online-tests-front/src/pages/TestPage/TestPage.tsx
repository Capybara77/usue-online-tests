import { MainLayout } from '@/components/MainLayout';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { MathJaxComponent } from '../MathJaxComponent/MathJaxComponent';
// import { MathJax as BetterReactMathJax } from 'better-react-mathjax';

export const TestPage = () => {
  const { testid } = useParams();
  const [testText, setTestText] = useState('');

  useEffect(() => {
    const createTest = async () => {
      const userResponse = await fetch('/api/create-test?testid=' + testid);
      const testJson = await userResponse.json();
      console.log("set text");

      setTestText(testJson.text);
    };
    createTest();
  }, [testid]);

  const [count, setCount] = useState(0);

  // useEffect(() => {
  //   // Create a script element
  //   const script = document.createElement('script');
  //   script.src = 'https://cdn.jsdelivr.net/npm/mathjax@2/MathJax.js?config=TeX-AMS-MML_HTMLorMML';
  //   script.type = 'text/javascript';

  //   // Configure MathJax
  //   script.innerHTML = `
  //     MathJax.Hub.Config({
  //       extensions: ["tex2jax.js"],
  //       jax: ["input/TeX", "output/HTML-CSS"],
  //       tex2jax: {
  //         inlineMath: [["$", "$"], ["\\(", "\\)"]],
  //         TeX: { extensions: ["AMSmath.js", "AMSsymbols.js"] },
  //       },
  //       showMathMenu: false,
  //       messageStyle: "none"
  //     });
  //   `;

  //   // Append the script to the document body
  //   document.body.appendChild(script);
  // }, []); // Empty dependency array ensures this runs only once on component mount


  if (!testText) {
    console.log("no render " + testText);
    return null;
  }
  console.log("render " + testText);
  return (
    <MainLayout>
      <div>
        <button onClick={() => setCount((prev) => prev + 1)}>
          count {count}
        </button>
        <h1>TEST</h1>
        <p>Test ID: {testid}</p>
        <p>{testText}</p>
        <div></div>
        <p>Math JAX</p>
        {/* <BetterReactMathJax>{testText}</BetterReactMathJax> */}
        {/* <h2>Basic MathJax example with Latex</h2>
        <MathJax>{'\\(\\frac{10}{4x} \\approx 2^{12}\\)'}</MathJax> */}
        <MathJaxComponent></MathJaxComponent>
      </div>
    </MainLayout>
  );
};
