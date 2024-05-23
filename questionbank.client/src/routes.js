// import { createBrowserRouter } from 'react-router-dom';
// import  Layout  from './views/layout/Layout';

// export const routers= createBrowserRouter([
//     {
//         path: "/",
//         element: Layout,
//         children:[
//             {
//                 path: 'dashboard',
//                 lazy: async () => {
//                     const  {Dashboard}  = await import('./views/dashboard/index');
//                     return { Component: Dashboard };
//                 },
//             }
//         ]
//     },
//    {
//         path: '/',
//         lazy: async () => {
//         const { Login } = await import('./views/auth/Login');
//         return { Component: Login };
//         },
//     },
//     // {
//     //     path: '/dashboard',
//     //     lazy: async () => {
//     //         const  {Dashboard}  = await import('./views/dashboard/index');
//     //     return { Component: Dashboard };
//     //     },
//     // },
// ]);