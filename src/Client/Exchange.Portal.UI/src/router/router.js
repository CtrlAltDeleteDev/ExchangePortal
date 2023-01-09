import Main from '@/pages/Main';
import Reviews from '@/pages/Reviews';
import Rules from '@/pages/Rules';
import Contacts from '@/pages/Contacts';
import Login from '@/pages/Login';
import PreviewOrder from '@/pages/PreviewOrder';
import CreateOrder from '@/pages/CreateOrder';
import ReadyOrder from '@/pages/ReadyOrder';
import CanceledOrder from '@/pages/CanceledOrder';
import PaidOrder from '@/pages/PaidOrder';
import { createRouter, createWebHistory } from 'vue-router';

const routes = [
  {
    path: '/preview',
    component: PreviewOrder,
  },
  {
    path: '/preview/create-order',
    component: CreateOrder,
  },
  {
    path: '/preview/create-order/ready-order',
    component: ReadyOrder,
  },
  {
    path: '/preview/create-order/ready-order/cancel',
    component: CanceledOrder,
  },
  {
    path: '/preview/create-order/ready-order/paid',
    component: PaidOrder,
  },
  {
    path: '/contacts',
    component: Contacts,
  },
  {
    path: '/rules',
    component: Rules,
  },
  {
    path: '/reviews',
    component: Reviews,
  },
  {
    path: '/adminka',
    component: Login,
  },
  {
    path: '/',
    component: Main,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
