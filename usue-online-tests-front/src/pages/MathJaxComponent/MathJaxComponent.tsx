// import createDOMPurify from 'dompurify'
// import { JSDOM } from 'jsdom'
import React, { useEffect } from 'react';
// const window = (new JSDOM('')).window
// const DOMPurify = createDOMPurify(window)

export const MathJaxComponent = () => {
  useEffect(() => {
    // Load MathJax script
    const script = document.createElement('script');
    script.src = 'https://cdn.jsdelivr.net/npm/mathjax@2/MathJax.js?config=TeX-AMS-MML_HTMLorMML';
    script.async = true;
    document.head.appendChild(script);

    // Configure MathJax
    script.onload = () => {
      console.log("Configure MathJax")
      window.MathJax.Hub.Config({
        extensions: ['tex2jax.js', "/src/libs/forminput.js"],
        jax: ['input/TeX', 'output/HTML-CSS'],
        tex2jax: {
          inlineMath: [['$', '$'], ['\\(', '\\)']],
          TeX: { extensions: ['AMSmath.js', 'AMSsymbols.js'] },
        },
        showMathMenu: false,
        messageStyle: 'none',
      });

      MathJax.Callback.Queue(MathJax.Hub.Register.StartupHook("TeX Jax Ready",function(){var t=MathJax.InputJax.TeX,a=t.Definitions,e=MathJax.ElementJax.mml,n=MathJax.HTML;a.macros.FormInput="FormInput",t.Parse.Augment({FormInput:function(t){var a=this.GetBrackets(t),i=this.GetBrackets(t),m=this.GetBrackets(t),s=this.GetArgument(t);(null==a||""===a)&&(a="2"),null==m&&(m=""),i=("MathJax_Input "+(i||"")).replace(/ +$/,"");var l=n.Element("input",{type:"text",name:s,id:s,size:a,className:i,value:m});l.setAttribute("xmlns","http://www.w3.org/1999/xhtml");var r=e["annotation-xml"](e.xml(l)).With({encoding:"application/xhtml+xml",isToken:!0});this.Push(e.semantics(r))}})})),MathJax.Ajax.loadComplete("/libs/forminput.js");

      window.MathJax.Hub.Typeset();
      // Typeset equations after a delay
      setTimeout(() => {
      }, 0);
    };
  }, []);

  return (
    <div>
    </div>
  );
};
