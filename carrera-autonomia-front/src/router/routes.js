const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        name: 'Inicio',
        component: () => import('pages/IndexPage.vue')
      },
      {
        path: 'admin',
        name: 'Admin',
        component: () => import('pages/VistaRegistros.vue')
      }
    ]
  },

  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue')
  }
]

export default routes
