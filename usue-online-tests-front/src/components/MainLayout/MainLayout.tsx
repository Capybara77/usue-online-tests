import { ReactNode } from 'react';
import { NavBar } from '../Navbar/';

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
