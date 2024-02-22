import { MainLayout } from '@/components/MainLayout';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';

export const TestPage = () => {
  const { testid } = useParams();
  const [testText, setTestText] = useState('');

  useEffect(() => {
    const createTest = async () => {
      const userResponse = await fetch('/api/create-test?testid=' + testid);
      const testJson = await userResponse.json();
      setTestText(testJson.text);
    };
    createTest();
  }, []);

  useEffect(() => {
    // Create a script element
    const script = document.createElement('script');
    script.src = 'https://cdn.jsdelivr.net/npm/mathjax@2/MathJax.js?config=TeX-AMS-MML_HTMLorMML';
    script.type = 'text/javascript';

    // Configure MathJax
    script.innerHTML = `
      MathJax.Hub.Config({
        extensions: ["tex2jax.js"],
        jax: ["input/TeX", "output/HTML-CSS"],
        tex2jax: {
          inlineMath: [["$", "$"], ["\\(", "\\)"]],
          TeX: { extensions: ["AMSmath.js", "AMSsymbols.js"] },
        },
        showMathMenu: false,
        messageStyle: "none"
      });
    `;

    // Append the script to the document body
    document.body.appendChild(script);
  }, []); // Empty dependency array ensures this runs only once on component mount

  return (
    <MainLayout>
      <div>
        <h1>TEST</h1>
        <p>Test ID: {testid}</p>
        <p>{testText}</p>
      </div>
    </MainLayout>
  );
};
