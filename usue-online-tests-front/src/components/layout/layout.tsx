import { ReactNode } from 'react';
import { Link } from 'react-router-dom';

export const Layout = ({ children }: { children: ReactNode }) => {
  return (
    <div className='flex min-h-screen flex-col bg-base-200 px-4'>
      <nav className='max-w-screen-lg mx-auto navbar p-0'>
        <div className='flex-1'>
          <Link to={'/'} className='text-xl'>Аудиторные тесты</Link>
        </div>
      </nav>
      <div className='grow max-w-screen-lg mx-auto w-full'>{children}</div>
      <footer className='footer footer-center p-4 text-base-content'>
        <div className='footer-center p-4 text-base-content'>
          © 2024 - ФГАОУ ВО «УрФУ имени первого Президента России Б.Н. Ельцина»
        </div>
      </footer>
    </div>
  );
};
