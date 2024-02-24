import { links } from '@/navigation/links';
import { ReactNode } from 'react';
import { Link } from 'react-router-dom';

const NavBar = () => {
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
              <li>
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
            {/* <li>
              <a>Item 1</a>
            </li>
            <li>
              <a>Parent</a>
              <ul className="p-2">
                <li>
                  <a>Submenu 1</a>
                </li>
                <li>
                  <a>Submenu 2</a>
                </li>
              </ul>
            </li>
            <li>
              <a>Item 3</a>
            </li> */}
          </ul>
        </div>
        <Link to={'/'} className="text-xl link link-hover">
          Аудиторные тесты
        </Link>
      </div>
      <div className="navbar-center hidden lg:flex">
        <ul className="menu menu-horizontal px-1">
          {links.map((link) => (
            <li>
              {link.sublinks?.length ? (
                <details>
                  <summary>{link.name}</summary>
                  <ul className="p-2">
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
          {/* <li>
            <a>Item 1</a>
          </li>
          <li>
            <details>
              <summary>Parent</summary>
              <ul className="p-2">
                <li>
                  <a>Submenu 1</a>
                </li>
                <li>
                  <a>Submenu 2</a>
                </li>
              </ul>
            </details>
          </li>
          <li>
            <a>Item 3</a>
          </li> */}
        </ul>
      </div>
      <div className="navbar-end">
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
            <li>
              <a>Item 2</a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export const MainLayout = ({ children }: { children: ReactNode }) => {
  return (
    <div className="flex min-h-screen flex-col bg-base-200 px-4 gap-8">
      <NavBar />
      <div className="grow max-w-screen-lg mx-auto w-full px-4 lg:px-0">
        {children}
      </div>
      <footer className="footer footer-center p-4 text-base-content">
        <div className="footer-center p-4 text-base-content">
          © 2024 - ФГАОУ ВО «УрФУ имени первого Президента России Б.Н. Ельцина»
        </div>
      </footer>
    </div>
  );
};
