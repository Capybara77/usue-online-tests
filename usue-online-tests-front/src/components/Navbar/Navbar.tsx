import { links } from '@/navigation/links';
import { useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { themeChange } from 'theme-change';

const ThemeController = () => {
  useEffect(() => {
    themeChange(false);
  }, []);

  const currentTheme = localStorage.getItem('theme');
  if (currentTheme) {
    document.documentElement.setAttribute('data-theme', currentTheme);
  }

  return (
    <label className="swap swap-rotate mr-4" onClick={() => themeChange(false)}>
      <input type="checkbox" className="theme-controller" value="synthwave" />

      {/* sun icon */}
      <svg
        className={`swap-${currentTheme === 'light' ? 'off' : 'on'} fill-current w-6 h-6`}
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
      >
        <path d="M5.64,17l-.71.71a1,1,0,0,0,0,1.41,1,1,0,0,0,1.41,0l.71-.71A1,1,0,0,0,5.64,17ZM5,12a1,1,0,0,0-1-1H3a1,1,0,0,0,0,2H4A1,1,0,0,0,5,12Zm7-7a1,1,0,0,0,1-1V3a1,1,0,0,0-2,0V4A1,1,0,0,0,12,5ZM5.64,7.05a1,1,0,0,0,.7.29,1,1,0,0,0,.71-.29,1,1,0,0,0,0-1.41l-.71-.71A1,1,0,0,0,4.93,6.34Zm12,.29a1,1,0,0,0,.7-.29l.71-.71a1,1,0,1,0-1.41-1.41L17,5.64a1,1,0,0,0,0,1.41A1,1,0,0,0,17.66,7.34ZM21,11H20a1,1,0,0,0,0,2h1a1,1,0,0,0,0-2Zm-9,8a1,1,0,0,0-1,1v1a1,1,0,0,0,2,0V20A1,1,0,0,0,12,19ZM18.36,17A1,1,0,0,0,17,18.36l.71.71a1,1,0,0,0,1.41,0,1,1,0,0,0,0-1.41ZM12,6.5A5.5,5.5,0,1,0,17.5,12,5.51,5.51,0,0,0,12,6.5Zm0,9A3.5,3.5,0,1,1,15.5,12,3.5,3.5,0,0,1,12,15.5Z" />
      </svg>

      {/* moon icon */}
      <svg
        className={`swap-${currentTheme === 'light' ? 'on' : 'off'} fill-current w-6 h-6`}
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 24 24"
      >
        <path d="M21.64,13a1,1,0,0,0-1.05-.14,8.05,8.05,0,0,1-3.37.73A8.15,8.15,0,0,1,9.08,5.49a8.59,8.59,0,0,1,.25-2A1,1,0,0,0,8,2.36,10.14,10.14,0,1,0,22,14.05,1,1,0,0,0,21.64,13Zm-9.5,6.69A8.14,8.14,0,0,1,7.08,5.22v.27A10.15,10.15,0,0,0,17.22,15.63a9.79,9.79,0,0,0,2.1-.22A8.11,8.11,0,0,1,12.14,19.73Z" />
      </svg>
    </label>
  );
};

export const NavBar = () => {
  const navigation = useNavigate();

  const logout = async () => {
    const response = await fetch('/api/logout', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    const json = await response.json();

    if (json) {
      navigation('/', {});
    }
    console.log('🚀 ~ logout ~ json:', json);
  };

  return (
    <div className="navbar max-w-screen-lg mx-auto p-0">
      <div className="navbar-start">
        <div className="dropdown">
          <div tabIndex={0} role="button" className="btn btn-ghost lg:hidden">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-5 w-5"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M4 6h16M4 12h8m-8 6h16"
              />
            </svg>
          </div>
          <ul
            tabIndex={0}
            className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52"
          >
            {links.map((link) => (
              <li key={link.name}>
                {link.sublinks?.length ? (
                  <>
                    <Link to={link.to}>{link.name}</Link>
                    <ul className="p-2">
                      {link.sublinks.map((sublink) => (
                        <li key={sublink.to}>
                          <Link to={sublink.to}>{sublink.name}</Link>
                        </li>
                      ))}
                    </ul>
                  </>
                ) : (
                  <Link key={link.name} to={link.to}>
                    {link.name}
                  </Link>
                )}
              </li>
            ))}
          </ul>
        </div>
        <Link to={'/'} className="text-xl link link-hover">
          Аудиторные тесты
        </Link>
      </div>
      <div className="navbar-center hidden lg:flex">
        <ul className="menu menu-horizontal px-1">
          {links.map((link) => (
            <li key={link.to}>
              {link.sublinks?.length ? (
                <details>
                  <summary>{link.name}</summary>
                  <ul className="p-2 z-[1]">
                    {link.sublinks.map((sublink) => (
                      <li key={sublink.to}>
                        <Link to={sublink.to}>{sublink.name}</Link>
                      </li>
                    ))}
                  </ul>
                </details>
              ) : (
                <Link key={link.name} to={link.to}>
                  {link.name}
                </Link>
              )}
            </li>
          ))}
        </ul>
      </div>
      <div className="navbar-end">
        <button data-toggle-theme="dark,light" data-act-class="ACTIVECLASS">
          <ThemeController />
        </button>
        <div className="dropdown dropdown-end">
          <div className="avatar placeholder cursor-pointer">
            <div
              tabIndex={0}
              role="button"
              className="btn btn-ghost rounded-full bg-neutral "
            >
              <span className="text-xl text-neutral-content">AI</span>
            </div>
          </div>
          <ul
            tabIndex={0}
            className="menu dropdown-content z-[1] p-2 shadow bg-base-100 rounded-box w-52 mt-4"
          >
            <li>
              <a>Item 1</a>
            </li>
            <div className="divider my-0"></div>
            <li onClick={logout}>
              <span>Выйти</span>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};
