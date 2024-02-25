import React, { ReactNode } from 'react';

type PageHeaderProps = {
  children: ReactNode;
  className?: string;
};

export const PageHeader: React.FC<PageHeaderProps> = ({
  children,
  className,
}) => {
  return (
    <div className={`prose mb-8 ${className}`}>
      <h1>{children}</h1>
    </div>
  );
};
