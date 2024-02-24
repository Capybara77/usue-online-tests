type TLink = {
  name: string;
  to: string;
  sublinks?: TLink[];
};

export const links: TLink[] = [
  {
    name: 'Профиль',
    to: '/profile',
  },
  {
    name: 'Тесты',
    to: '/alltests',
    sublinks: [
      {
        name: 'Alltests',
        to: '/alltests',
      },
      {
        name: 'Доступные тесты',
        to: '/tests',
      },
      {
        name: 'Тесты для прохождений',
        to: '/availabletests',
      },
    ],
  },

  {
    name: 'Поменять пароль',
    to: '/changepassword',
  },
];
