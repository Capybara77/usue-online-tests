import { MainLayout } from '@/components/MainLayout';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { MathJaxComponent } from '../MathJaxComponent/MathJaxComponent';
import React from 'react';

export const TestPage = () => {
  const { testid } = useParams();
  const [testText, setTestText] = useState('');

  useEffect(() => {
    const createTest = async () => {
      const userResponse = await fetch('/api/create-test?testid=' + testid);
      const testJson = await userResponse.json();
      console.log('set text');

      setTestText(testJson.text);
    };
    createTest();
  }, [testid]);

  function decodeHtmlEntities(encodedString:string) {
    return encodedString.replace(/&#(\d+);/g, function(match, dec) {
        return String.fromCharCode(dec);
    });
}

  const renderInputs = (testText: string) => {
    let tempTestText = testText;
    const matches = testText.match(/<(.*?)>/g);
    if (!matches) return tempTestText;

    for (let i = 0; i < matches.length; i++) {
      const inputName = matches[i].replace(/[<>]/g, '');

      // Replace input tags
      tempTestText = tempTestText.replace(
        matches[i],
        `\\FormInput[1][input input-sm mx-2 my-1][]{${inputName}}`
      );
    }

    console.log(decodeHtmlEntities(tempTestText));
    return decodeHtmlEntities(tempTestText);
  };

  if (!testText) {
    console.log('no render ' + testText);
    return null;
  }
  console.log('render ' + testText);
  return (
    <MainLayout>
      <div>
        <p className="overflow-x-auto overflow-y-hidden">
          {renderInputs(testText)
            .split('\n')
            .map((line, index) => (
              <React.Fragment key={index}>
                {line}
                <br />
              </React.Fragment>
            ))}
        </p>

        <MathJaxComponent></MathJaxComponent>
      </div>
    </MainLayout>
  );
};
