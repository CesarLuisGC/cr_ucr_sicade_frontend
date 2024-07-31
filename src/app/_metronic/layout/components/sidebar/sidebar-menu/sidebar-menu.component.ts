import { Component } from '@angular/core';
import { NavMenu } from 'src/app/core/models/base/navMenu.model';

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.scss']
})
export class SidebarMenuComponent {
  menus: NavMenu[] = [
    {
      titulo: "Seguridad",
      hijos: [
        {
          nombre: 'Usuarios',
          icono: 'element-plus',
          ruta: '',
          hijos: [
            {
              nombre: 'Menu 1',
              icono: 'element-plus',
              ruta: '',
              hijos: [
                {
                  nombre: 'Sub-menu 1',
                  icono: 'element-plus',
                  ruta: 'crafted/pages/profile/overview',
                  hijos: []
                },
                {
                  nombre: 'Sub-menu 2',
                  icono: 'element-plus',
                  ruta: '/crafted/pages/profile/projects',
                  hijos: []
                },
              ]
            },
            {
              nombre: 'Menu 2',
              icono: 'element-plus',
              ruta: '/crafted/pages/profile/campaigns',
              hijos: []
            },
          ]
        },
        {
          nombre: 'Roles',
          icono: 'element-plus',
          ruta: 'builder',
          hijos: []
        },
      ]
    },
  ];
}
