// страшное зрелище, но по другому подключить MathJax не придумали, да прости нас господь

/* eslint-disable prefer-const */
// eslint-disable-next-line @typescript-eslint/ban-ts-comment
// @ts-nocheck

import { useEffect } from 'react';

export const MathJaxComponent = () => {
  useEffect(() => {
    // Load MathJax script

    const script = document.createElement('script');
    script.src =
      'https://cdn.jsdelivr.net/npm/mathjax@2/MathJax.js?config=TeX-AMS-MML_HTMLorMML';
    script.async = true;
    document.head.appendChild(script);

    // Configure MathJax
    script.onload = () => {
      console.log('Configure MathJax');
      window.MathJax.Hub.Config({
        extensions: ['tex2jax.js', '/src/libs/forminput.js'],
        jax: ['input/TeX', 'output/HTML-CSS'],
        tex2jax: {
          inlineMath: [
            ['$', '$'],
            ['\\(', '\\)'],
          ],
          TeX: { extensions: ['AMSmath.js', 'AMSsymbols.js'] },
        },
        showMathMenu: false,
        messageStyle: 'none',
      });

      MathJax.Callback.Queue(
        MathJax.Hub.Register.StartupHook('TeX Jax Ready', function () {
          const t = MathJax.InputJax.TeX,
            a = t.Definitions,
            e = MathJax.ElementJax.mml,
            n = MathJax.HTML;
          (a.macros.FormInput = 'FormInput'),
            t.Parse.Augment({
              FormInput: function (t) {
                let a = this.GetBrackets(t),
                  i = this.GetBrackets(t),
                  m = this.GetBrackets(t),
                  s = this.GetArgument(t);
                (null == a || '' === a) && (a = '2'),
                  null == m && (m = ''),
                  (i = ('MathJax_Input ' + (i || '')).replace(/ +$/, ''));
                const l = n.Element('input', {
                  type: 'text',
                  name: s,
                  id: s,
                  size: a,
                  className: i,
                  value: m,
                });
                l.setAttribute('xmlns', 'http://www.w3.org/1999/xhtml');
                const r = e['annotation-xml'](e.xml(l)).With({
                  encoding: 'application/xhtml+xml',
                  isToken: !0,
                });
                this.Push(e.semantics(r));
              },
            });
        })
      ),
        MathJax.Ajax.loadComplete('/libs/forminput.js');

      window.MathJax.Hub.Typeset();
    };
  }, []);

  return <div></div>;
};
