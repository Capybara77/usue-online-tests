// import createDOMPurify from 'dompurify'
// import { JSDOM } from 'jsdom'

// const window = (new JSDOM('')).window
// const DOMPurify = createDOMPurify(window)

export const MathJaxComponent = () => {

  console.log('MathJaxComponent');
  const rawHTML = `
  <script src="https://cdn.jsdelivr.net/npm/mathjax@2/MathJax.js?config=TeX-AMS-MML_HTMLorMML"></script>
  <script type="text/x-mathjax-config">
          MathJax.Hub.Config({
              extensions: ["tex2jax.js", "/src/libs/forminput.js"],
              jax: ["input/TeX", "output/HTML-CSS"],
              tex2jax: {
                  inlineMath: [
                  ["$", "$"],
                  ["\\(", "\\)"]
                  ],
                  TeX: { extensions: ["AMSmath.js", "AMSsymbols.js"] },
              },
              showMathMenu: false,
              messageStyle: "none"
          });
          setTimeout(() => {
            MathJax.Hub.Typeset()
    console.log('12312')

          }, 3000)
  </script>
  `
  
  return (

    <div>
    { <div dangerouslySetInnerHTML={{ __html: rawHTML }} /> }
  </div>
  );
};
