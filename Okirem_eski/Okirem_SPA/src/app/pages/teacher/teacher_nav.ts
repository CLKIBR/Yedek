import { INavData } from '@coreui/angular';

export const teacherNavItems: INavData[] = [
    {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },
];