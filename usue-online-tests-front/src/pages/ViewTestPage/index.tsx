import { MathJaxComponent } from '../MathJaxComponent/MathJaxComponent';
import React from 'react';

const ViewTest = ({
  text,
  checkBoxes,
}: {
  text: string;
  checkBoxes: string[];
}) => {
  function decodeHtmlEntities(encodedString: string) {
    return encodedString.replace(/&#(\d+);/g, function (match, dec) {
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

    return decodeHtmlEntities(tempTestText);
  };

  return (
    <div>
      <p className="overflow-x-auto overflow-y-hidden">
        {renderInputs(text)
          .split('\n')
          .map((line, index) => (
            <React.Fragment key={index}>
              {line}
              <br />
            </React.Fragment>
          ))}
      </p>

      {checkBoxes.map((item, index) => (
        <div key={index}>
          <input
            type="checkbox"
            id={`chk-${index}`}
            name={`checkbox-${index}`}
          />
          <label htmlFor={`chk-${index}`}>{item}</label>
        </div>
      ))}

      <MathJaxComponent></MathJaxComponent>
    </div>
  );
};

export default ViewTest;
